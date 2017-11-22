namespace Truant.Processors
{
	public class HeartRateProcessor : RateProcessor, IHeartRateProcessor
	{
		public double? HeartRate {
			get { return Rate * HeartRateFactor; }
		}

		private const int HeartRateFactor = 60; // Beats/second -> BPM

		public HeartRateProcessor() : base(65536, 256) // EventTimeOverflow, EventCountOverflow
		{
		}

		public void ProcessHeartRateEvent(int eventTime, int eventCount)
		{
			ProcessRateEvent(eventTime, eventCount);
		}
	}
}