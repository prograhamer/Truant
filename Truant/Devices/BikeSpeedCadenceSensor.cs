using System.Collections.Generic;
using Truant.Processors;

namespace Truant.Devices
{
	public class BikeSpeedCadenceSensor : PlusDevice
	{
		private IBikeSpeedProcessor SpeedProcessor;
		private IBikeCadenceProcessor CadenceProcessor;

		public struct BikeSpeedCadenceData
		{
			public double? Speed;
			public double? Cadence;
		}

		public int WheelSize {
			get { return SpeedProcessor.WheelSize; }
			set { SpeedProcessor.WheelSize = value; }
		}

		private BikeSpeedCadenceData _Data;

		public BikeSpeedCadenceData Data {
			get {
				return _Data;
			}
		}

		public BikeSpeedCadenceSensor(int wheelSize)
		{
			// ID and period as described in device profile
			DeviceType = 0x79;
			ChannelPeriod = 8086;

			SpeedProcessor = new BikeSpeedProcessor(wheelSize);
			CadenceProcessor = new BikeCadenceProcessor();
		}

		// Data Pages
		// N.B. First byte of rxData is the corresponding channel no.
		//
		// Page 0
		// ------
		// Byte:
		// 1-2 : Cadence Event Time (little-endian) 1/1024s
		// 3-4 : Cadence Revolution Count (little-endian)
		// 5-6 : Speed Event Time (little-endian) 1/1024s
		// 7-8 : Speed Revolution Count (little-endian)
		protected override bool InterpretReceivedData(byte[] rxData)
		{
			CadenceProcessor.ProcessCadenceEvent(
				rxData[1] + (rxData[2] << 8), // Event time
				rxData[3] + (rxData[4] << 8)  // Revolution count
			);

			SpeedProcessor.ProcessSpeedEvent(
				rxData[5] + (rxData[6] << 8), // Event time
				rxData[7] + (rxData[8] << 8)  // Revolution count
			);

			_Data.Cadence = CadenceProcessor.Cadence;
			_Data.Speed = SpeedProcessor.Speed;

			return CadenceProcessor.NewEvent || SpeedProcessor.NewEvent;
		}

		protected override void TriggerNewDataCallbacks()
		{
			foreach (NewDataCallback callback in NewDataCallbacks) {
				callback(Config.DeviceID, _Data);
			}
		}
	}
}