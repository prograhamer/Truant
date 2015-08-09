using System;
using System.Threading;

namespace Truant.Plus
{
	public class HeartRateMonitor
	{
		private const byte ANT_FREQ = 57;
		private static byte [] NETWORK_KEY = {0xB9, 0xA5, 0x21, 0xFB, 0xBD, 0x72, 0xC3, 0x45};
		private const byte HR_MONITOR_ID = 0x78;

		private static byte [] responseBuffer = new byte[32];
		private static byte [] channelEventBuffer = new byte[32];

		private byte channel;
		private byte network;

		private static bool channelOpen = false;

		private static Interpreter.HeartRateInterpreter hrInterpreter = new Interpreter.HeartRateInterpreter();

		public HeartRateMonitor (byte channel, byte network)
		{
			this.channel = channel;
			this.network = network;
		}

		public void Connect()
		{
			bool result;

			AntInternal.ANT_AssignResponseFunction (assignResponseCallback, responseBuffer);
			AntInternal.ANT_AssignChannelEventFunction (channel, channelEventCallback, channelEventBuffer);

			Console.WriteLine ("Setting network key!");
			result = AntInternal.ANT_SetNetworkKey (network, NETWORK_KEY);
			Console.WriteLine ("ANT_SetNetworkKey(): " + result);

			while (!channelOpen) {
				Thread.Sleep (10);
			}
		}

		public void Disconnect()
		{
			AntInternal.ANT_CloseChannel (channel);
		}

		public void RequestChannelID()
		{
			AntInternal.ANT_RequestMessage (channel, MessageType.CHANNEL_ID_ID);
		}

		public bool assignResponseCallback(byte channel, Truant.MessageType messageID)
		{
			Console.WriteLine ("Got AR callback: " + channel + " / msgId: " + messageID);
			
			if (messageID == MessageType.RESPONSE_EVENT_ID) {
				Truant.MessageType arMessageID = (Truant.MessageType)responseBuffer [1];
				Truant.ResponseStatus arStatus = (Truant.ResponseStatus)responseBuffer [2];
				
				Console.WriteLine ("arMessageId: " + arMessageID + ", arStatus: " + arStatus);
				
				if (arMessageID == MessageType.NETWORK_KEY_ID &&
					arStatus == ResponseStatus.NO_ERROR) {
					Console.WriteLine ("Set network key went OK, assigning channel!");
					
					AntInternal.ANT_AssignChannel (channel, ChannelType.SLAVE, network);
				} else if (arMessageID == MessageType.ASSIGN_CHANNEL_ID &&
					arStatus == ResponseStatus.NO_ERROR) {
					Console.WriteLine ("Assign Channel went OK, setting channel ID");
					
					AntInternal.ANT_SetChannelId (channel, 0, HR_MONITOR_ID, 0);
				} else if (arMessageID == MessageType.CHANNEL_ID_ID &&
					arStatus == ResponseStatus.NO_ERROR) {
					Console.WriteLine ("Set Channel ID went OK, setting RF frequency");
					
					AntInternal.ANT_SetChannelRFFreq (channel, ANT_FREQ);
				} else if (arMessageID == MessageType.CHANNEL_RADIO_FREQ_ID &&
					arStatus == ResponseStatus.NO_ERROR) {
					Console.WriteLine ("Set RF frequency went OK, setting channel period");
					AntInternal.ANT_SetChannelPeriod (channel, 8070);
				} else if (arMessageID == MessageType.CHANNEL_PERIOD_ID &&
					arStatus == ResponseStatus.NO_ERROR) {
					Console.WriteLine ("Channel period set, opening channel!");
					AntInternal.ANT_OpenChannel (channel);
				} else if (arMessageID == MessageType.OPEN_CHANNEL_ID &&
					arStatus == ResponseStatus.NO_ERROR) {
					Console.WriteLine ("Channel open! Enabling extended messages");
					
					AntInternal.ANT_RxExtMesgsEnable (1);
				} else if (arMessageID == MessageType.RX_EXT_MESGS_ENABLE_ID) {
					Console.WriteLine("Extended messages enabled, setting flag!");

					channelOpen = true;
				} else if (arMessageID == MessageType.CLOSE_CHANNEL_ID &&
				           arStatus == ResponseStatus.NO_ERROR) {
					Console.WriteLine ("Channel close successful");
				} else if (arMessageID == MessageType.UNASSIGN_CHANNEL_ID &&
				           arStatus == ResponseStatus.NO_ERROR) {
					Console.WriteLine ("Channel unassigned!");
				}
			} else if (messageID == MessageType.CHANNEL_ID_ID) {
				Console.WriteLine ("CHANNEL_ID: " + BitConverter.ToString (responseBuffer));
			}
			
			return true;
		}

		public bool channelEventCallback (byte channel, ResponseStatus channelEvent)
		{
			Console.WriteLine ("Got CE callback: " + channel + " / channelEvent: " + channelEvent);
			
			if (channelEvent == ResponseStatus.EVENT_CHANNEL_CLOSED) {
				Console.WriteLine ("CHANNEL CLOSED! Unassigning");
				AntInternal.ANT_UnAssignChannel (channel);
			} else if (channelEvent == ResponseStatus.EVENT_RX_FLAG_BROADCAST) {
				Data.HeartRateData data = hrInterpreter.interpretReceivedData(channelEventBuffer);
				Console.WriteLine ("HEART RATE IS: " + data);
			}
			return true;
		}
	}
}

