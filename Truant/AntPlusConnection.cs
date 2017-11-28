using System;

namespace Truant
{
	public class AntPlusConnection : AntConnection
	{
		// ANT+ Managed network key
        private static byte[] networkKey = { 0xB9, 0xA5, 0x21, 0xFB, 0xBD, 0x72, 0xC3, 0x45 };

		private AntPlusConnection(byte device, byte network, byte[] networkKey) : base(device, network, networkKey)
		{
		}

		public static AntConnection GetConnection()
		{
			return GetConnection(0 /* device */, 0 /* network */, networkKey);
		}

		public static AntConnection GetConnection(byte device, byte network)
		{
			return GetConnection(device, network, networkKey);
		}
	}
}