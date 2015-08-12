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

		public int Cadence { get; private set; }
		public int Speed { get; private set; }
		// ---------------------------------------------------------

		public BikeSpeedCadenceSensor ()
		{
			// ID and period as described in device profile
			DeviceType = 0x79;
			ChannelPeriod = 8086;
		}

		public override void interpretReceivedData(byte [] rxData)
		{
			int oldEventTime, oldRevolutionCount;
			int newEventTime, newRevolutionCount;

			oldEventTime = CadenceEventTime;
			oldRevolutionCount = CadenceRevolutionCount;

			CadenceEventTime = rxData[1] + (rxData[2] << 8);
			CadenceRevolutionCount = rxData[3] + (rxData[4] << 8);

			newEventTime = CadenceEventTime;
			if(CadenceEventTime < oldEventTime) newEventTime += 65535;
			newRevolutionCount = CadenceRevolutionCount;
			if(CadenceRevolutionCount < oldRevolutionCount) newRevolutionCount += 65535;

			Cadence = ((newRevolutionCount - oldRevolutionCount)*60*1024) / (newEventTime - oldEventTime);

			SpeedEventTime = rxData[5] + (rxData[6] << 8);
			SpeedRevolutionCount = rxData[7] + (rxData[8] << 8);
		}
	}
}

