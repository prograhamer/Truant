using System;
using Truant.Plus.Data;

namespace Truant.Plus.Interpreter
{
	public class HeartRateInterpreter
	{
		private HeartRateData data;
		private bool pageChangeOn = false;
		private bool pageChangeOff = false;

		public HeartRateInterpreter()
		{
		}

		public HeartRateData interpretReceivedData(byte [] rxData)
		{
			if (data == null) {
				data = new HeartRateData();
			}

			Console.WriteLine ("HeartRateInterpreter: " + BitConverter.ToString(rxData));

			int page = (byte) (rxData [1] & 0x7F);
			bool pageChange = ((rxData [1] & 0x80) == 0x80);

			if (pageChange) {
				pageChangeOn = true;
			} else {
				pageChangeOff = true;
			}

			if (pageChangeOn && pageChangeOff) {
				if (page == 1) {
					data.CumulativeOperatingTime = rxData[1] + (rxData[2] << 8) + (rxData[3] << 16);
				} else if (page == 2) {
					data.ManufacturerID = rxData[2];
					data.SerialNumber = rxData[3] + (rxData[4] << 8);
				} else if (page == 3) {
					data.HardwareVersion = rxData[2];
					data.SoftwareVersion = rxData[3];
					data.ModelNumber = rxData[4];
				} else if (page == 4) {
					data.PreviousHeartBeatEventTime = rxData[3] + (rxData[4] << 8);
				}
			}

			data.HeartBeatEventTime = rxData[5] + (rxData[6] << 8);
			data.HeartBeatCount = rxData [7];
			data.ComputedHeartRate = rxData [8];

			return data;
		}
	}
}

