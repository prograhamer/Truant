using Truant.Processors;

namespace Truant.Devices
{
	public class BikeSpeedCadenceSensor : PlusDevice
	{
		private IBikeSpeedProcessor SpeedProcessor;
		private IBikeCadenceProcessor CadenceProcessor;

		public double? Speed {
			get{ return SpeedProcessor.Speed; }
		}

		public double? Cadence {
			get{ return CadenceProcessor.Cadence; }
		}

		public int WheelSize {
			get { return SpeedProcessor.WheelSize; }
			set { SpeedProcessor.WheelSize = value; }
		}

		public BikeSpeedCadenceSensor (int wheelSize)
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
		protected override void InterpretReceivedData(byte [] rxData)
		{
			CadenceProcessor.ProcessCadenceEvent(
				rxData[1] + (rxData[2] << 8), // Event time
				rxData[3] + (rxData[4] << 8)  // Revolution count
			);

			SpeedProcessor.ProcessSpeedEvent(
				rxData[5] + (rxData[6] << 8), // Event time
				rxData[7] + (rxData[8] << 8)  // Revolution count
			);
		}
	}
}

