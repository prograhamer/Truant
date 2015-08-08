using System;
using System.Runtime.InteropServices;
using System.Threading;
using Truant;

namespace TruantDemo
{
	class MainClass
	{
		private const byte NETWORK_NO = 0;
		private const byte CHANNEL_NO = 0;

		private static byte [] NETWORK_KEY = {0xB9, 0xA5, 0x21, 0xFB, 0xBD, 0x72, 0xC3, 0x45};

		private static byte [] responseBuffer = new byte[255];
		private static byte [] channelEventBuffer = new byte[255];

		public static bool assignResponseCallback(byte channel, byte messageID)
		{
			Console.WriteLine ("Got AR callback: " + channel + " / msgId: " + messageID);

			if (messageID == MessageType.RESPONSE_EVENT_ID) {
				if(responseBuffer[1] == MessageType.NETWORK_KEY_ID &&
				   responseBuffer[2] == ResponseStatus.NO_ERROR)
				{
					Console.WriteLine ("Set network key went OK, assigning channel!");
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

			result = AntInternal.ANT_Init (0, 57600);
			Console.WriteLine("ANT_Init(): " + result);

			AntInternal.ANT_AssignResponseFunction (assignResponseCallback, responseBuffer);
			AntInternal.ANT_AssignChannelEventFunction (channelEventCallback, channelEventBuffer);

			Console.WriteLine ("Resetting module!");
			result = AntInternal.ANT_ResetSystem ();
			Console.WriteLine ("ANT_ResetSystem(): " + result);
			Thread.Sleep (1000);

			Console.WriteLine ("Setting network key!");
			result = AntInternal.ANT_SetNetworkKey (NETWORK_NO, NETWORK_KEY);
			Console.WriteLine ("ANT_SetNetworkKey(): " + result);

			Thread.Sleep (10000);

			AntInternal.ANT_Close ();
		}
	}
}
