using System;
namespace Truant
{
	public class ResponseStatus
	{
		public const byte NO_ERROR                                   = 0x00;
		public const byte NO_EVENT                                   = 0x00;
		
		public const byte EVENT_RX_SEARCH_TIMEOUT                    = 0x01;
		public const byte EVENT_RX_FAIL                              = 0x02;
		public const byte EVENT_TX                                   = 0x03;
		public const byte EVENT_TRANSFER_RX_FAILED                   = 0x04;
		public const byte EVENT_TRANSFER_TX_COMPLETED                = 0x05;
		public const byte EVENT_TRANSFER_TX_FAILED                   = 0x06;
		public const byte EVENT_CHANNEL_CLOSED                       = 0x07;
		public const byte EVENT_RX_FAIL_GO_TO_SEARCH                 = 0x08;
		public const byte EVENT_CHANNEL_COLLISION                    = 0x09;
		public const byte EVENT_TRANSFER_TX_START                    = 0x0A;           // a pending transmit transfer has begun
		
		public const byte EVENT_CHANNEL_ACTIVE                       = 0x0F;
		
		public const byte EVENT_TRANSFER_TX_NEXT_MESSAGE             = 0x11;           // only enabled in FIT1
		
		public const byte CHANNEL_IN_WRONG_STATE                     = 0x15;           // returned on attempt to perform an action from the wrong channel state
		public const byte CHANNEL_NOT_OPENED                         = 0x16;           // returned on attempt to communicate on a channel that is not open
		public const byte CHANNEL_ID_NOT_SET                         = 0x18;           // returned on attempt to open a channel without setting the channel ID
		public const byte CLOSE_ALL_CHANNELS                         = 0x19;           // returned when attempting to start scanning mode, when channels are still open
		
		public const byte TRANSFER_IN_PROGRESS                       = 0x1F;           // returned on attempt to communicate on a channel with a TX transfer in progress
		public const byte TRANSFER_SEQUENCE_NUMBER_ERROR             = 0x20;           // returned when sequence number is out of order on a Burst transfer
		public const byte TRANSFER_IN_ERROR                          = 0x21;
		public const byte TRANSFER_BUSY                              = 0x22;
		
		public const byte INVALID_MESSAGE_CRC                        = 0x26;           // returned if there is a framing error on an incomming message
		public const byte MESSAGE_SIZE_EXCEEDS_LIMIT                 = 0x27;           // returned if a data message is provided that is too large
		public const byte INVALID_MESSAGE                            = 0x28;           // returned when the message has an invalid parameter
		public const byte INVALID_NETWORK_NUMBER                     = 0x29;           // returned when an invalid network number is provided
		public const byte INVALID_LIST_ID                            = 0x30;           // returned when the provided list ID or size exceeds the limit
		public const byte INVALID_SCAN_TX_CHANNEL                    = 0x31;           // returned when attempting to transmit on channel 0 when in scan mode
		public const byte INVALID_PARAMETER_PROVIDED                 = 0x33;           // returned when an invalid parameter is specified in a configuration message
		
		public const byte EVENT_SERIAL_QUE_OVERFLOW                  = 0x34;
		public const byte EVENT_QUE_OVERFLOW                         = 0x35;           // ANT event que has overflowed and lost 1 or more events
		
		public const byte EVENT_CLK_ERROR                            = 0x36;           // debug event for XOSC16M on LE1
		public const byte EVENT_STATE_OVERRUN                        = 0x37;
		
		
		public const byte SCRIPT_FULL_ERROR                          = 0x40;           // error writing to script, memory is full
		public const byte SCRIPT_WRITE_ERROR                         = 0x41;           // error writing to script, bytes not written correctly
		public const byte SCRIPT_INVALID_PAGE_ERROR                  = 0x42;           // error accessing script page
		public const byte SCRIPT_LOCKED_ERROR                        = 0x43;           // the scripts are locked and can't be dumped
		
		public const byte NO_RESPONSE_MESSAGE                        = 0x50;           // returned to the Command_SerialMessageProcess function, so no reply message is generated
		public const byte RETURN_TO_MFG                              = 0x51;           // default return to any mesg when the module determines that the mfg procedure has not been fully completed
		
		public const byte FIT_ACTIVE_SEARCH_TIMEOUT                  = 0x60;           // Fit1 only event added for timeout of the pairing state after the Fit module becomes active
		public const byte FIT_WATCH_PAIR                             = 0x61;           // Fit1 only
		public const byte FIT_WATCH_UNPAIR                           = 0x62;           // Fit1 only
		
		public const byte USB_STRING_WRITE_FAIL                      = 0x70;
		
		// Internal only events below this point
		public const byte INTERNAL_ONLY_EVENTS                       = 0x80;
		public const byte EVENT_RX                                   = 0x80;           // INTERNAL: Event for a receive message
		public const byte EVENT_NEW_CHANNEL                          = 0x81;           // INTERNAL: EVENT for a new active channel
		public const byte EVENT_PASS_THRU                            = 0x82;           // INTERNAL: Event to allow an upper stack events to pass through lower stacks
		
		public const byte EVENT_BLOCKED                              = 0xFF;           // INTERNAL: Event to replace any event we do not wish to go out, will also zero the size of the Tx message
	}
}

