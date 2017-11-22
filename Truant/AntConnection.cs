using System;
using System.Threading;

namespace Truant
{
	public class AntConnection
	{
		private static AntConnection connection;

		private const int baud = 57600;

		private static byte [] _ResponseBuffer = new byte[32];
		private static byte [] _ChannelEventBuffer = new byte[32];

		private static byte _UsbDevice;
		private static byte _Network;
		private static byte [] _NetworkKey;

		private static bool _NetworkReady = false;
		private static bool _NetworkError = false;

        private static DeviceConnection [] _DeviceConnections = new DeviceConnection[8];

		protected AntConnection(byte device, byte network, byte [] networkKey)
		{
			_UsbDevice = device;
			_Network = network;
			_NetworkKey = networkKey;
		}

		public static AntConnection GetConnection(byte [] networkKey)
		{
			return GetConnection(0 /* device */, 0 /* network */, networkKey);
		}

		public static AntConnection GetConnection(byte device, byte network, byte [] networkKey)
		{
			if(connection == null) connection = new AntConnection(device, network, networkKey);
			return connection;
		}

		public void Connect()
		{
			if(_NetworkReady) throw new DuplicateConnectionException("Network connection already open");

			bool result;

			result = AntInternal.ANT_Init(_UsbDevice, baud);

			if(!result) throw new InitializationException("Failed to open ANT device");

			result = AntInternal.ANT_ResetSystem ();

			if(!result) throw new InitializationException("Failed to reset ANT device");

			Thread.Sleep (1000);

			AntInternal.ANT_AssignResponseFunction (assignResponseCallback, _ResponseBuffer);
			// AntInternal.ANT_AssignChannelEventFunction (channel, channelEventCallback, _ChannelEventBuffer);

			Console.WriteLine ("Setting network key!");
			result = AntInternal.ANT_SetNetworkKey (_Network, _NetworkKey);

			if(!result) throw new InitializationException("Failed to set network key");

			while (!_NetworkReady && !_NetworkError) {
				Thread.Sleep (100);
			}

			if(_NetworkError)
			{
				_NetworkReady = false; _NetworkError = false;

				throw new InitializationException("Error response to an initialization command");
			}
		}

		public void AddDevice(Device device)
		{
			for(byte i = 0; i < _DeviceConnections.Length; i++)
			{
				if(_DeviceConnections[i] == null)
				{
                    _DeviceConnections[i] = new DeviceConnection(device);
					device.Connection = connection;
					AntInternal.ANT_AssignChannel(i, ChannelType.SLAVE, _Network);
					break;
				}
			}
		}

		public void RemoveDevice(Device device)
		{
			for(byte i = 0; i < _DeviceConnections.Length; i++)
			{
				if(_DeviceConnections[i].Device == device)
				{
					AntInternal.ANT_CloseChannel(i);
                    _DeviceConnections[i].ChannelCloseRequested = true;
					break;
				}
			}
		}

		// Always an 8 byte array to send
		public void SendBroadcastData(Device device, byte [] data)
		{
			for(byte i = 0; i < _DeviceConnections.Length; i++)
			{
				if(_DeviceConnections[i].Device == device) {
					AntInternal.ANT_SendBroadcastData(i, data);
					break;
				}
			}
		}

		public void SendAcknowledgedData(Device device, byte [] data)
		{
			for(byte i = 0; i < _DeviceConnections.Length; i++)
			{
				if(_DeviceConnections[i].Device == device) {
					AntInternal.ANT_SendAcknowledgedData(i, data);
					break;
				}
			}
		}

		public void Disconnect()
		{
			Console.WriteLine("Disconnecting and resetting");

			AntInternal.ANT_UnassignAllResponseFunctions();

			AntInternal.ANT_ResetSystem();
			AntInternal.ANT_Close();

			connection = null;
		}

		private static bool assignResponseCallback(byte channel, Truant.MessageType messageID)
		{
			Console.WriteLine ("Got AR callback: " + channel + " / msgId: " + messageID);

			if (messageID == MessageType.RESPONSE_EVENT_ID) {
				return processResponseEvent(channel, messageID);
			} else if (messageID == MessageType.CHANNEL_ID_ID) {
				Console.WriteLine ("CHANNEL_ID: " + BitConverter.ToString (_ResponseBuffer));
				return processChannelIDEvent(channel, messageID);
			}

			return true;
		}

		// Process Response Event Message
		//
		// Byte:
		// 0   : Channel Number
		// 1   : Message ID (MessageType)
		// 2   : Message Code (ResponseStatus)
		private static bool processResponseEvent(byte channel, MessageType messageID)
		{
			MessageType arMessageID = (MessageType) _ResponseBuffer[1];
			ResponseStatus arStatus = (ResponseStatus) _ResponseBuffer[2];

			Console.WriteLine ("RE: arMessageId=" + arMessageID + ", arStatus=" + arStatus);

			if(arMessageID == MessageType.NETWORK_KEY_ID)
			{
				if(arStatus == ResponseStatus.NO_ERROR)
				{
					Console.WriteLine("Network key set");
					_NetworkReady = true;
				}
				else
				{
					_NetworkError = true;
				}
				return true;
			}

			if(channel >= _DeviceConnections.Length || _DeviceConnections[channel] == null)
			{
				Console.WriteLine("RE message received for unexpected channel!");
				return true;
			}

			switch (arMessageID) {
			case MessageType.ASSIGN_CHANNEL_ID:
				if(arStatus == ResponseStatus.NO_ERROR)
				{
					Console.WriteLine("Channel assigned for #" + channel);
							
					AntInternal.ANT_SetChannelId(
                        channel,
                        _DeviceConnections[channel].Device.Config.DeviceID,
                        _DeviceConnections[channel].Device.DeviceType,
                        _DeviceConnections[channel].Device.Config.TransmissionType
                    );
				}
				break;
			case MessageType.CHANNEL_ID_ID:
				if(arStatus == ResponseStatus.NO_ERROR)
				{
					Console.WriteLine("Channel ID set for #" + channel);
					AntInternal.ANT_SetChannelRFFreq(channel, _DeviceConnections[channel].Device.RadioFrequency);
				}
				break;
			case MessageType.CHANNEL_RADIO_FREQ_ID:
				if(arStatus == ResponseStatus.NO_ERROR)
				{
					Console.WriteLine("RF frequency set for #" + channel);
					AntInternal.ANT_SetChannelPeriod(channel, _DeviceConnections[channel].Device.ChannelPeriod);
				}
				break;
			case MessageType.CHANNEL_PERIOD_ID:
				if(arStatus == ResponseStatus.NO_ERROR)
				{
					Console.WriteLine("Channel period set for #" + channel);
					AntInternal.ANT_OpenChannel(channel);
				}
				break;
			case MessageType.PROX_SEARCH_CONFIG_ID:
				if(arStatus == ResponseStatus.NO_ERROR)
				{
					Console.WriteLine ("Channel search radius set for #" + channel);
					AntInternal.ANT_OpenChannel(channel);
				}
				break;
			case MessageType.OPEN_CHANNEL_ID:
				if(arStatus == ResponseStatus.NO_ERROR)
				{
					Console.WriteLine("Channel #" + channel + " open!");
					AntInternal.ANT_AssignChannelEventFunction(channel, channelEventCallback, _ChannelEventBuffer);
				}
				break;
			case MessageType.RX_EXT_MESGS_ENABLE_ID:
				break;
			case MessageType.CLOSE_CHANNEL_ID:
				break;
			case MessageType.UNASSIGN_CHANNEL_ID:
                    if(arStatus == ResponseStatus.NO_ERROR)
                    {
                        if(_DeviceConnections[channel] == null) {
                            Console.WriteLine("DeviceConnection for channel #{0} should be present, and isn't", channel);
                        }
                        else
                        {
                            _DeviceConnections[channel] = null;
                        }
                    }
				break;
			}

			return true;
		}

		// Process Set Channel ID message
		//
		// Byte:
		// 0   : Channel number
		// 1-2 : Device Number (little-endian)
		// 3   : MSB (1 bit) Pairing Bit / lower 7-bits Device Type
		// 4   : Transmission type
		private static bool processChannelIDEvent(byte channel, MessageType messageID)
		{
			if(_DeviceConnections[channel].Device.Status == DeviceStatus.PAIRING)
			{
				_DeviceConnections[channel].Device.Status = DeviceStatus.PAIRED;
				_DeviceConnections[channel].Device.Config.DeviceID = (ushort) (_ResponseBuffer[1] + (_ResponseBuffer[2] << 8));
				_DeviceConnections[channel].Device.Config.TransmissionType = _ResponseBuffer[4];
			}

			return true;
		}

		private static bool channelEventCallback (byte channel, ResponseStatus channelEvent)
		{
			Console.WriteLine ("Got CE callback: " + channel + " / channelEvent: " + channelEvent);

			if (channelEvent == ResponseStatus.EVENT_CHANNEL_CLOSED)
			{
                if(_DeviceConnections[channel].ChannelCloseRequested)
                {
                    Console.WriteLine ("Channel #" + channel + " closed, unassigning...");
                    AntInternal.ANT_UnAssignChannel (channel);
                }
                else
                {
                    Console.WriteLine ("Channel #" + channel + " closed unexpectedly, re-opening...");
                    AntInternal.ANT_OpenChannel(channel);
                }
			}
			else if(channelEvent == ResponseStatus.EVENT_TRANSFER_TX_COMPLETED)
			{
				Console.WriteLine("Channel #" + channel + " Acknowledged data received");
			}
			else if(channelEvent == ResponseStatus.EVENT_TRANSFER_TX_FAILED)
			{
				Console.WriteLine("Channel #" + channel + " Acknowledged data failed");
			}
			else if (channelEvent == ResponseStatus.EVENT_RX_FLAG_BROADCAST || channelEvent == ResponseStatus.EVENT_RX_BROADCAST)
			{
				_DeviceConnections[channel].Device.ReceiveData(_ChannelEventBuffer);

				if(_DeviceConnections[channel].Device.Status == DeviceStatus.UNPAIRED)
				{
					_DeviceConnections[channel].Device.Status = DeviceStatus.PAIRING;
					AntInternal.ANT_RequestMessage(channel, MessageType.CHANNEL_ID_ID);
				}
			}
			return true;
		}
	}
}

