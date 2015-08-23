using System;

namespace Truant
{
	public class DeviceConfig
	{
		public ushort DeviceID { get; set; }
		public byte TransmissionType { get; set; }

		public DeviceConfig()
		{
		}

		public DeviceConfig(ushort deviceID, byte transmissionType)
		{
			this.DeviceID = deviceID;
			this.TransmissionType = transmissionType;
		}

		public override string ToString()
		{
			return "DeviceID: " + DeviceID + ", TransmissionType: " + TransmissionType;
		}
	}
}

