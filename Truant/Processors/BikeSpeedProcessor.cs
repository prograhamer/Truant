namespace Truant.Processors
{
	public class BikeSpeedProcessor : RateProcessor, IBikeSpeedProcessor
	{
		public double? Speed {
			get { return Rate * SpeedFactor; }
		}
		public int WheelSize { get; set; }

		private double SpeedFactor {
			// To convert speed in mm per second to km/h
			get { return (0.0036d * WheelSize); }
		}

		public BikeSpeedProcessor(int wheelSize) : base(65536, 65536) // EventTimeOverflow, EventCountOverflow
		{
			WheelSize = wheelSize;
		}

		public void ProcessSpeedEvent(int eventTime, int revolutionCount)
		{
			ProcessRateEvent(eventTime, revolutionCount);
		}
	}
}