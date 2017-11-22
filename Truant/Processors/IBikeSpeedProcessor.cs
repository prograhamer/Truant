namespace Truant.Processors
{
	public interface IBikeSpeedProcessor
	{
		double? Speed { get; }
		int WheelSize { get; set; }

		void ProcessSpeedEvent(int eventTime, int revolutionCount);
	}
}