namespace Truant.Processors
{
	public class BikeCadenceProcessor : RateProcessor, IBikeCadenceProcessor
	{
		public double? Cadence {
			get { return Rate * CadenceFactor; }
		}

		private const int CadenceFactor = 60; // Revolutions/second -> RPM

		public BikeCadenceProcessor() : base(65536, 65536) // EventTimeOverflow, EventCountOverflow
		{
		}

		public void ProcessCadenceEvent(int eventTime, int revolutionCount)
		{
			ProcessRateEvent(eventTime, revolutionCount);
		}
	}
}