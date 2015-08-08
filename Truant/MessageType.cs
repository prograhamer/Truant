using System;

namespace Truant
{
	public class MessageType
	{
		public const byte INVALID_ID                      = 0x00;
		public const byte EVENT_ID                        = 0x01;
		
		public const byte VERSION_ID                      = 0x3E;
		public const byte RESPONSE_EVENT_ID               = 0x40;
		
		public const byte UNASSIGN_CHANNEL_ID             = 0x41;
		public const byte ASSIGN_CHANNEL_ID               = 0x42;
		public const byte CHANNEL_PERIOD_ID               = 0x43;
		public const byte CHANNEL_SEARCH_TIMEOUT_ID       = 0x44;
		public const byte CHANNEL_RADIO_FREQ_ID           = 0x45;
		public const byte NETWORK_KEY_ID                  = 0x46;
		public const byte RADIO_TX_POWER_ID               = 0x47;
		public const byte RADIO_CW_MODE_ID                = 0x48;
		public const byte SYSTEM_RESET_ID                 = 0x4A;
		public const byte OPEN_CHANNEL_ID                 = 0x4B;
		public const byte CLOSE_CHANNEL_ID                = 0x4C;
		public const byte REQUEST_ID                      = 0x4D;
		
		public const byte BROADCAST_DATA_ID               = 0x4E;
		public const byte ACKNOWLEDGED_DATA_ID            = 0x4F;
		public const byte BURST_DATA_ID                   = 0x50;
		
		public const byte CHANNEL_ID_ID                   = 0x51;
		public const byte CHANNEL_STATUS_ID               = 0x52;
		public const byte RADIO_CW_INIT_ID                = 0x53;
		public const byte CAPABILITIES_ID                 = 0x54;
		
		public const byte STACKLIMIT_ID                   = 0x55;
		
		public const byte SCRIPT_DATA_ID                  = 0x56;
		public const byte SCRIPT_CMD_ID                   = 0x57;
		
		public const byte ID_LIST_ADD_ID                  = 0x59;
		public const byte ID_LIST_CONFIG_ID               = 0x5A;
		public const byte OPEN_RX_SCAN_ID                 = 0x5B;
		
		public const byte EXT_CHANNEL_RADIO_FREQ_ID       = 0x5C;  // OBSOLETE: = for 905 radio;
		public const byte EXT_BROADCAST_DATA_ID           = 0x5D;
		public const byte EXT_ACKNOWLEDGED_DATA_ID        = 0x5E;
		public const byte EXT_BURST_DATA_ID               = 0x5F;
		
		public const byte CHANNEL_RADIO_TX_POWER_ID       = 0x60;
		public const byte GET_SERIAL_NUM_ID               = 0x61;
		public const byte GET_TEMP_CAL_ID                 = 0x62;
		public const byte SET_LP_SEARCH_TIMEOUT_ID        = 0x63;
		public const byte SET_TX_SEARCH_ON_NEXT_ID        = 0x64;
		public const byte SERIAL_NUM_SET_CHANNEL_ID_ID    = 0x65;
		public const byte RX_EXT_MESGS_ENABLE_ID          = 0x66;  
		public const byte RADIO_CONFIG_ALWAYS_ID          = 0x67;
		public const byte ENABLE_LED_FLASH_ID             = 0x68;
		public const byte XTAL_ENABLE_ID                  = 0x6D;
		public const byte ANTLIB_CONFIG_ID                = 0x6E;
		public const byte STARTUP_ID                      = 0x6F;
		public const byte AUTO_FREQ_CONFIG_ID             = 0x70;
		public const byte PROX_SEARCH_CONFIG_ID           = 0x71;
		
		public const byte SET_SEARCH_CH_PRIORITY_ID       = 0x75;
		
		
		public const byte CUBE_CMD_ID                     = 0x80;
		
		public const byte GET_PIN_DIODE_CONTROL_ID        = 0x8D;
		public const byte PIN_DIODE_CONTROL_ID            = 0x8E;
		public const byte FIT1_SET_AGC_ID                 = 0x8F;
		
		public const byte FIT1_SET_EQUIP_STATE_ID         = 0x91;  // *** CONFLICT: w/ Sensrcore, Fit1 will never have sensrcore enabled
		
		// Sensrcore Messages
		public const byte SET_CHANNEL_INPUT_MASK_ID       = 0x90;
		public const byte SET_CHANNEL_DATA_TYPE_ID        = 0x91;
		public const byte READ_PINS_FOR_SECT_ID           = 0x92;
		public const byte TIMER_SELECT_ID                 = 0x93;
		public const byte ATOD_SETTINGS_ID                = 0x94;
		public const byte SET_SHARED_ADDRESS_ID           = 0x95;
		public const byte ATOD_EXTERNAL_ENABLE_ID         = 0x96;
		public const byte ATOD_PIN_SETUP_ID               = 0x97;
		public const byte SETUP_ALARM_ID                  = 0x98;
		public const byte ALARM_VARIABLE_MODIFY_TEST_ID   = 0x99;
		public const byte PARTIAL_RESET_ID                = 0x9A;
		public const byte OVERWRITE_TEMP_CAL_ID           = 0x9B;
		public const byte SERIAL_PASSTHRU_SETTINGS_ID     = 0x9C;
		
		public const byte BIST_ID                         = 0xAA;
		public const byte UNLOCK_INTERFACE_ID             = 0xAD;
		public const byte SERIAL_ERROR_ID                 = 0xAE;
		public const byte SET_ID_STRING_ID                = 0xAF;
		
		public const byte PORT_GET_IO_STATE_ID            = 0xB4;
		public const byte PORT_SET_IO_STATE_ID            = 0xB5;
		
		public const byte RSSI_ID                         = 0xC0;
		public const byte RSSI_BROADCAST_DATA_ID          = 0xC1;
		public const byte RSSI_ACKNOWLEDGED_DATA_ID       = 0xC2;
		public const byte RSSI_BURST_DATA_ID              = 0xC3;
		public const byte RSSI_SEARCH_THRESHOLD_ID        = 0xC4;
		public const byte SLEEP_ID                        = 0xC5;
		public const byte GET_GRMN_ESN_ID                 = 0xC6;
		public const byte SET_USB_INFO_ID                 = 0xC7;
		
		public const byte HCI_COMMAND_COMPLETE            = 0xC8;
	}
}

