namespace Truant.Processors
{
	public interface IHeartRateProcessor
	{
		double? HeartRate { get; }
        double? RRPeriod { get; }
		bool NewEvent { get; }

		void ProcessHeartRateEvent(int eventTime, int eventCount);
	}
}