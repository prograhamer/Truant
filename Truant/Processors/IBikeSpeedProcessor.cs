namespace Truant.Processors
{
	public interface IBikeSpeedProcessor
	{
		double? Speed { get; }
		int WheelSize { get; set; }
		bool NewEvent { get; }

		void ProcessSpeedEvent(int eventTime, int revolutionCount);
	}
}