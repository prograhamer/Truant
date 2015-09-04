namespace Truant.Processors
{
	public class RateProcessor : IRateProcessor
	{
		public double? Rate{ get; protected set; }
		public int? EventTime{ get; protected set; }
		public int? EventCount{ get; protected set; }

		private int Overflow;

		public RateProcessor (int overflow)
		{
			Overflow = overflow;
		}

		public void ProcessRateEvent (int eventTime, int eventCount)
		{
			int? oldEventTime, oldEventCount, newEventTime, newEventCount;

			// Speed update and calculation
			oldEventTime = EventTime;
			oldEventCount = EventCount;

			EventTime = eventTime;
			EventCount = eventCount;

			if(oldEventTime != null && EventTime != oldEventTime)
			{
				newEventTime = EventTime;
				if(newEventTime < oldEventTime) newEventTime += Overflow;
				newEventCount = EventCount;
				if(newEventCount < oldEventCount) newEventCount += Overflow;

				// Calculate rate in events/second
				Rate = 1024 * ((double) newEventCount - oldEventCount) / (newEventTime - oldEventTime);
			}
		}
	}
}

