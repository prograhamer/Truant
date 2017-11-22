namespace Truant.Processors
{
	public interface IHeartRateProcessor
	{
		double? HeartRate { get; }

		void ProcessHeartRateEvent(int eventTime, int eventCount);
	}
}