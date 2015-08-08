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
		private const uint BAUD = 57600;
		private const byte ANT_FREQ = 57;

		private static byte [] NETWORK_KEY = {0xB9, 0xA5, 0x21, 0xFB, 0xBD, 0x72, 0xC3, 0x45};

		private static byte [] responseBuffer = new byte[32];
		private static byte [] channelEventBuffer = new byte[32];

		public static bool assignResponseCallback(byte channel, byte messageID)
		{
			Console.WriteLine ("Got AR callback: " + channel + " / msgId: " + messageID);

			if (messageID == MessageType.RESPONSE_EVENT_ID) {
				byte arMessageID = responseBuffer[1];
				byte arStatus = responseBuffer[2];

				Console.WriteLine ("arMessageId: " + arMessageID + ", arStatus: " + arStatus);

				if(arMessageID == MessageType.NETWORK_KEY_ID &&
				   arStatus == ResponseStatus.NO_ERROR)
				{
					Console.WriteLine ("Set network key went OK, assigning channel!");

					AntInternal.ANT_AssignChannel(CHANNEL_NO, ChannelType.SLAVE, NETWORK_NO);
				}
				else if(arMessageID == MessageType.ASSIGN_CHANNEL_ID &&
				        arStatus == ResponseStatus.NO_ERROR)
				{
					Console.WriteLine("Assign Channel went OK, setting channel ID");

					AntInternal.ANT_SetChannelId(CHANNEL_NO, 0, 0x78, 0);
				}
				else if(arMessageID == MessageType.CHANNEL_ID_ID &&
				        arStatus == ResponseStatus.NO_ERROR)
				{
					Console.WriteLine("Set Channel ID went OK, setting RF frequency");

					AntInternal.ANT_SetChannelRFFreq(CHANNEL_NO, ANT_FREQ);
				}
				else if(arMessageID == MessageType.CHANNEL_RADIO_FREQ_ID &&
				        arStatus == ResponseStatus.NO_ERROR)
				{
					Console.WriteLine("Set RF frequency went OK, opening channel!");

					AntInternal.ANT_OpenChannel(CHANNEL_NO);
				}
				else if(arMessageID == MessageType.OPEN_CHANNEL_ID &&
				        arStatus == ResponseStatus.NO_ERROR)
				{
					Console.WriteLine("Channel open! Enabling extended messages");

					AntInternal.ANT_RxExtMesgsEnable(1);
				}
				else if(arMessageID == MessageType.CLOSE_CHANNEL_ID &&
				        arStatus == ResponseStatus.NO_ERROR)
				{
					Console.WriteLine("Channel closed! Unassigning channel");

					AntInternal.ANT_UnAssignChannel(CHANNEL_NO);
				}
				else if(arMessageID == MessageType.UNASSIGN_CHANNEL_ID &&
				        arStatus == ResponseStatus.NO_ERROR)
				{
					Console.WriteLine("Channel unassigned!");
				}
			}


			return true;
		}

		public static bool channelEventCallback(byte channel, byte channelEvent)
		{
			Console.WriteLine ("Got CE callback: " + channel + " / channelEvent: " + channelEvent);
			Console.WriteLine ("HEART RATE IS: " + channelEventBuffer [8] + "!!!");
			return true;
		}

		public static void Main (string[] args)
		{
			bool result;

			result = AntInternal.ANT_Init (0, BAUD);
			Console.WriteLine("ANT_Init(): " + result);

			AntInternal.ANT_AssignResponseFunction (assignResponseCallback, responseBuffer);
			AntInternal.ANT_AssignChannelEventFunction (CHANNEL_NO, channelEventCallback, channelEventBuffer);

			Console.WriteLine ("Resetting module!");
			result = AntInternal.ANT_ResetSystem ();
			Console.WriteLine ("ANT_ResetSystem(): " + result);
			Thread.Sleep (1000);

			Console.WriteLine ("Setting network key!");
			result = AntInternal.ANT_SetNetworkKey (NETWORK_NO, NETWORK_KEY);
			Console.WriteLine ("ANT_SetNetworkKey(): " + result);

			Thread.Sleep (30000);

			AntInternal.ANT_CloseChannel (CHANNEL_NO);
			Thread.Sleep (1000);

			AntInternal.ANT_Close ();
		}
	}
}
