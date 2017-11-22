namespace Truant.Processors
{
	public interface IBikeCadenceProcessor
	{
		double? Cadence { get; }
		void ProcessCadenceEvent(int eventTime, int revolutionCount);
	}
}