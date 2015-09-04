namespace Truant.Processors
{
	public class BikeCadenceProcessor : RateProcessor, IBikeCadenceProcessor
	{
		public double? Cadence{
			get { return Rate * CadenceFactor; }
		}

		private const int CadenceFactor = 61440;

		public BikeCadenceProcessor() : base (65536)
		{
		}

		public void ProcessCadenceEvent(int eventTime, int revolutionCount)
		{
			ProcessRateEvent(eventTime, revolutionCount);
		}
	}
}

