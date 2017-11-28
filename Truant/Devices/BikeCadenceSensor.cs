using System.Collections.Generic;
using Truant.Processors;

namespace Truant.Devices
{
	public class BikeCadenceSensor : PlusDevice
	{
		private IBikeCadenceProcessor Processor;

		public struct BikeCadenceData
		{
			public double? Cadence;
		}

		private BikeCadenceData _Data;

		public BikeCadenceData Data {
			get {
				return _Data;
			}
		}

		public delegate void NewDataCallback(ushort id, BikeCadenceData data);
		private List<NewDataCallback> NewDataCallbacks = new List<NewDataCallback>();

		public BikeCadenceSensor()
		{
			// ID and period as described in device profile
			DeviceType = 0x7A;
			ChannelPeriod = 8102;

			Processor = new BikeCadenceProcessor();
		}

		public void AddNewDataCallback(NewDataCallback callback)
		{
			NewDataCallbacks.Add(callback);
		}

		// Data Pages
		// N.B. First byte of rxData is the corresponding channel no.
		//
		// Page 0
		// ------
		// Byte:
		// 1   : MSB (1 bit) Page change toggle / 7 lower bits Data page number
		// 2-4 : Reserved
		// 5-6 : Cadence Event Time (1/1024s)
		// 7-8 : Cadence Revolution Count
		protected override void InterpretReceivedData(byte[] rxData)
		{
			Processor.ProcessCadenceEvent(
				rxData[5] + (rxData[6] << 8), // Event time
				rxData[7] + (rxData[8] << 8)  // Revolution count
			);

			_Data.Cadence = Processor.Cadence;
		}

		protected override void TriggerNewDataCallbacks()
		{
			foreach (NewDataCallback callback in NewDataCallbacks) {
				callback(Config.DeviceID, _Data);
			}
		}
	}
}