using Truant.Processors;

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

		private IHeartRateProcessor Processor;
		public double? HeartRate {
			get { return Processor.HeartRate; }
		}

		public HeartRateMonitor()
		{
			// ID and period as described in HR monitor device profile
			DeviceType = 0x78;
			ChannelPeriod = 8070;

			Processor = new HeartRateProcessor();
		}

		// Data Pages
		// N.B. First byte is always the channel no.
		//
		// Page 0
		// ------
		// Byte:
		// 1   : 1 bit (MSB) Page change toggle / lower 7 bits Data page number
		// 2-4 : Reserved
		// 5-6 : Heart beat event time (1/1024s)
		// 7   : Heart beat count
		// 8   : Computed heart rate (bpm)
		//
		// Page 1
		// ------
		// Byte:
		// 2-4 : Cumulative operating time
		//
		// Page 2
		// ------
		// Byte:
		// 2   : Manufacturer ID
		// 3-4 : Serial number
		//
		// Page 3
		// ------
		// Byte:
		// 2   : Hardware version
		// 3   : Software version
		// 4   : Model number
		//
		// Page 4
		// ------
		// Byte:
		// 2   : Manufacturer specific (no interpretation)
		// 2-3 : Previous heart beat event time (1/1024s)
		protected override void InterpretReceivedData(byte [] rxData)
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

			int eventTime = rxData[5] + (rxData[6] << 8);
			int eventCount = rxData [7];

			HeartBeatEventTime = eventTime;
			HeartBeatCount = eventCount;
			ComputedHeartRate = rxData [8];

			Processor.ProcessHeartRateEvent(eventTime, eventCount);
		}
	}
}
