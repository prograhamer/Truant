namespace Truant.Processors
{
	public interface IBikeCadenceProcessor
	{
		double? Cadence { get; }
		bool NewEvent { get; }

		void ProcessCadenceEvent(int eventTime, int revolutionCount);
	}
}