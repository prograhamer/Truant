namespace Truant.Processors
{
	public class BikeSpeedProcessor : RateProcessor, IBikeSpeedProcessor
	{
		public double? Speed{
			get{ return Rate * SpeedFactor; }
		}
		public int WheelSize{ get; set; }

		private double SpeedFactor{
			// To convert speed in mm per 1024th second to km/h
			get{ return (3.6864d * WheelSize); }
		}

		public BikeSpeedProcessor(int wheelSize) : base(65536)
		{
			WheelSize = wheelSize;
		}

		public void ProcessSpeedEvent(int eventTime, int revolutionCount)
		{
			ProcessRateEvent(eventTime, revolutionCount);
		}
	}
}

