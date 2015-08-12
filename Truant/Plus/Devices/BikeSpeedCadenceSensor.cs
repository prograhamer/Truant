using System;

namespace Truant.Plus.Devices
{
	public class BikeSpeedCadenceSensor : PlusDevice
	{
		// Device-specific data attributes ------------------------
		public int CadenceEventTime { get; private set; }
		public int CadenceRevolutionCount { get; private set; }
		public int SpeedEventTime { get; private set; }
		public int SpeedRevolutionCount { get; private set; }

		public double Cadence { get; private set; }
		public double Speed { get; private set; }
		// ---------------------------------------------------------

		private int WheelSize;

		public BikeSpeedCadenceSensor (int wheelSize)
		{
			// ID and period as described in device profile
			DeviceType = 0x79;
			ChannelPeriod = 8086;

			this.WheelSize = wheelSize;
		}

		public override void interpretReceivedData(byte [] rxData)
		{
			int oldEventTime, oldRevolutionCount;
			int newEventTime, newRevolutionCount;

			// Cadence update and calculation
			oldEventTime = CadenceEventTime;
			oldRevolutionCount = CadenceRevolutionCount;

			CadenceEventTime = rxData[1] + (rxData[2] << 8);
			CadenceRevolutionCount = rxData[3] + (rxData[4] << 8);

			newEventTime = CadenceEventTime;
			if(newEventTime < oldEventTime) newEventTime += 65535;
			newRevolutionCount = CadenceRevolutionCount;
			if(newRevolutionCount < oldRevolutionCount) newRevolutionCount += 65535;

			Cadence = ((newRevolutionCount - oldRevolutionCount)*60.0*1024.0) / (newEventTime - oldEventTime);

			// Speed update and calculation
			oldEventTime = SpeedEventTime;
			oldRevolutionCount = SpeedRevolutionCount;

			SpeedEventTime = rxData[5] + (rxData[6] << 8);
			SpeedRevolutionCount = rxData[7] + (rxData[8] << 8);

			newEventTime = SpeedEventTime;
			if(newEventTime < oldEventTime) newEventTime += 65535;
			newRevolutionCount = SpeedRevolutionCount;
			if(newRevolutionCount < oldRevolutionCount) newRevolutionCount += 65535;

			Speed = ((newRevolutionCount - oldRevolutionCount)*1024.0*WheelSize) / (newEventTime - oldEventTime);
		}
	}
}
