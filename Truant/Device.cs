using System;

namespace Truant
{
	public abstract class Device
	{
		public byte DeviceType { get; protected set; }
		public ushort DeviceID { get; protected set; }
		public byte TransmissionType { get; protected set; }
		public byte RadioFrequency { get; protected set; }
		public ushort ChannelPeriod { get; protected set; }

		public abstract void interpretReceivedData(byte [] data);
	}
}

