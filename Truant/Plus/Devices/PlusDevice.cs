using System;

namespace Truant.Plus.Devices
{
	public abstract class PlusDevice : Device
	{
		public PlusDevice()
		{
			RadioFrequency = 57; // 2.457 GHz, ANT+ standard
		}
	}
}

