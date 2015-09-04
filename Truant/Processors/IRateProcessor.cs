namespace Truant.Processors
{
	public interface IRateProcessor
	{
		double? Rate{ get; }
		void ProcessRateEvent(int eventTime, int eventCount);
	}
}

