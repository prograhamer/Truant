using System;

namespace Truant.Devices
{
	public class HeartRateMonitor : PlusDevice
	{
		// Device-specific data attributes ------------------------
		public int? HeartBeatEventTime { get; private set; }
		public int? HeartBeatCount { get; private set; }
		public int? ComputedHeartRate { get; private set; }

		public int? CumulativeOperatingTime{ get; private set; }

		public int? ManufacturerID{ get; private set; }
		public int? SerialNumber{ get; private set; }

		public int? HardwareVersion{ get; private set; }
		public int? SoftwareVersion{ get; private set; }
		public int? ModelNumber{ get; private set; }

		public int? PreviousHeartBeatEventTime{ get; private set; }
		// ---------------------------------------------------------

		private bool pageChangeOn = false;
		private bool pageChangeOff = false;

		public HeartRateMonitor()
		{
			// ID and period as described in HR monitor device profile
			DeviceType = 0x78;
			ChannelPeriod = 8070;
		}

		public override void interpretReceivedData(byte [] rxData)
		{
			int page = (byte) (rxData [1] & 0x7F);
			bool pageChange = ((rxData [1] & 0x80) == 0x80);

			if (pageChange) {
				pageChangeOn = true;
			} else {
				pageChangeOff = true;
			}

			if (pageChangeOn && pageChangeOff) {
				if (page == 1) {
					CumulativeOperatingTime = rxData[2] + (rxData[3] << 8) + (rxData[4] << 16);
				} else if (page == 2) {
					ManufacturerID = rxData[2];
					SerialNumber = rxData[3] + (rxData[4] << 8);
				} else if (page == 3) {
					HardwareVersion = rxData[2];
					SoftwareVersion = rxData[3];
					ModelNumber = rxData[4];
				} else if (page == 4) {
					PreviousHeartBeatEventTime = rxData[3] + (rxData[4] << 8);
				}
			}

			HeartBeatEventTime = rxData[5] + (rxData[6] << 8);
			HeartBeatCount = rxData [7];
			ComputedHeartRate = rxData [8];
		}
	}
}
