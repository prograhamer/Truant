using System.Collections.Generic;
using Truant.Processors;

namespace Truant.Devices
{
	public class BikeSpeedSensor : PlusDevice
	{
		private IBikeSpeedProcessor Processor;

		public struct BikeSpeedData
		{
			public double? Speed;
		}

		private BikeSpeedData _Data;

		public BikeSpeedData Data {
			get {
				return _Data;
			}
		}

		public BikeSpeedSensor(int wheelSize)
		{
			DeviceType = 0x7B;
			ChannelPeriod = 8118;

			Processor = new BikeSpeedProcessor(wheelSize);
		}

		// Data Pages
		//
		// Page 0
		// ------
		// Byte:
		// 1   : MSB (1 bit) Page change toggle / 7 lower bits Data page number
		// 2-4 : Reserved
		// 5-6 : Speed Event Time (1/1024s)
		// 7-8 : Speeed Revolution Count
		protected override void InterpretReceivedData(byte[] rxData)
		{
			Processor.ProcessSpeedEvent(
				rxData[5] + (rxData[6] << 8), // Event time
				rxData[7] + (rxData[8] << 8)  // Revolution count
			);

			_Data.Speed = Processor.Speed;
		}

		protected override void TriggerNewDataCallbacks()
		{
			foreach (NewDataCallback callback in NewDataCallbacks) {
				callback(Config.DeviceID, _Data);
			}
		}
	}
}