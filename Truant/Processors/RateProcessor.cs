namespace Truant.Processors
{
	public class RateProcessor : IRateProcessor
	{
		public double? Rate { get; protected set; }
		public double? Period { get; protected set; }
		public int? EventTime { get; protected set; }
		public int? EventCount { get; protected set; }

		private int EventTimeOverflow;
		private int EventCountOverflow;

		public RateProcessor(int eventTimeOverflow, int eventCountOverflow)
		{
			EventTimeOverflow = eventTimeOverflow;
			EventCountOverflow = eventCountOverflow;
		}

		public void ProcessRateEvent(int eventTime, int eventCount)
		{
			int? oldEventTime, oldEventCount, newEventTime, newEventCount;

			// Speed update and calculation
			oldEventTime = EventTime;
			oldEventCount = EventCount;

			EventTime = eventTime;
			EventCount = eventCount;

			if (oldEventTime != null && EventTime != oldEventTime) {
				newEventTime = EventTime;
				if (newEventTime < oldEventTime) newEventTime += EventTimeOverflow;
				newEventCount = EventCount;
				if (newEventCount < oldEventCount) newEventCount += EventCountOverflow;

				if (newEventCount - oldEventCount == 1)
				{
					Period = 1.024 * (newEventTime - oldEventTime) / (newEventCount - oldEventCount);
				} else {
					Period = null;
				}
				// Calculate rate in events/second
				Rate = 1024 * ((double)newEventCount - oldEventCount) / (newEventTime - oldEventTime);
			}
		}
	}
}