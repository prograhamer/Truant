namespace Truant.Processors
{
	public interface IHeartRateProcessor
	{
		double? HeartRate { get; }
        double? RRPeriod { get; }

		void ProcessHeartRateEvent(int eventTime, int eventCount);
	}
}