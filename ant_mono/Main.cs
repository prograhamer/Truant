using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace ant_mono
{
	class MainClass
	{
		private const byte NETWORK_NO = 0;
		private const byte CHANNEL_NO = 0;

		private const byte MESG_STATUS_OK = 0;

		private const byte MESG_RESPONSE_EVENT_ID = 0x40;
		private const byte MESG_NETWORK_KEY_ID = 0x46;

		private static byte [] NETWORK_KEY = {0xB9, 0xA5, 0x21, 0xFB, 0xBD, 0x72, 0xC3, 0x45};
		private static byte [] responseBuffer = new byte[255];
		private static byte [] channelEventBuffer = new byte[255];

		[DllImport ("libANT")]
		public static extern bool ANT_Init (byte device_no, uint baud);
		[DllImport ("libANT")]
		public static extern void ANT_Close ();
		[DllImport ("libANT")]
		public static extern bool ANT_ResetSystem();
		[DllImport ("libANT")]
		public static extern bool ANT_SetNetworkKey(byte network_no, byte [] key);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate bool AssignResponseDelegate(byte channel, byte messageID);
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate bool ChannelEventDelegate(byte channel, byte channelEvent);

		[DllImport ("libANT")]
		public static extern void ANT_AssignResponseFunction(AssignResponseDelegate cb, byte[] buffer);

		[DllImport ("libANT")]
		public static extern void ANT_AssignChannelEventFunction (ChannelEventDelegate cb, byte[] buffer);

		public static bool assignResponseCallback(byte channel, byte messageID)
		{
			Console.WriteLine ("Got AR callback: " + channel + " / msgId: " + messageID);

			if (messageID == MESG_RESPONSE_EVENT_ID) {
//				Console.WriteLine(BitConverter.ToString (responseBuffer));
				if(responseBuffer[1] == MESG_NETWORK_KEY_ID && responseBuffer[2] == MESG_STATUS_OK)
				{
					Console.WriteLine ("Set network key went OK, assigning channel!");
					// TODO: Assign the channel
				}
			}

			return true;
		}

		public static bool channelEventCallback(byte channel, byte channelEvent)
		{
			Console.WriteLine ("Got CE callback: " + channel + " / channelEvent: " + channelEvent);
			return true;
		}

		public static void Main (string[] args)
		{
			bool result;

			result = ANT_Init (0, 57600);
			Console.WriteLine("ANT_Init(): " + result);

			ANT_AssignResponseFunction (assignResponseCallback, responseBuffer);
			ANT_AssignChannelEventFunction (channelEventCallback, channelEventBuffer);

			Console.WriteLine ("Resetting module!");
			result = ANT_ResetSystem ();
			Console.WriteLine ("ANT_ResetSystem(): " + result);
			Thread.Sleep (1000);

			Console.WriteLine ("Setting network key!");
			result = ANT_SetNetworkKey (NETWORK_NO, NETWORK_KEY);
			Console.WriteLine ("ANT_SetNetworkKey(): " + result);

//			Console.WriteLine ("

			Thread.Sleep (10000);

			ANT_Close ();
		}
	}
}
