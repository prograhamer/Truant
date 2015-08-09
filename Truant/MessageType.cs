namespace Truant
{
	public enum MessageType : byte
	{
		INVALID_ID                      = 0x00,
		EVENT_ID                        = 0x01,
		
		VERSION_ID                      = 0x3E,
		RESPONSE_EVENT_ID               = 0x40,
		
		UNASSIGN_CHANNEL_ID             = 0x41,
		ASSIGN_CHANNEL_ID               = 0x42,
		CHANNEL_PERIOD_ID               = 0x43,
		CHANNEL_SEARCH_TIMEOUT_ID       = 0x44,
		CHANNEL_RADIO_FREQ_ID           = 0x45,
		NETWORK_KEY_ID                  = 0x46,
		RADIO_TX_POWER_ID               = 0x47,
		RADIO_CW_MODE_ID                = 0x48,
		SYSTEM_RESET_ID                 = 0x4A,
		OPEN_CHANNEL_ID                 = 0x4B,
		CLOSE_CHANNEL_ID                = 0x4C,
		REQUEST_ID                      = 0x4D,
		
		BROADCAST_DATA_ID               = 0x4E,
		ACKNOWLEDGED_DATA_ID            = 0x4F,
		BURST_DATA_ID                   = 0x50,
		
		CHANNEL_ID_ID                   = 0x51,
		CHANNEL_STATUS_ID               = 0x52,
		RADIO_CW_INIT_ID                = 0x53,
		CAPABILITIES_ID                 = 0x54,
		
		STACKLIMIT_ID                   = 0x55,
		
		SCRIPT_DATA_ID                  = 0x56,
		SCRIPT_CMD_ID                   = 0x57,
		
		ID_LIST_ADD_ID                  = 0x59,
		ID_LIST_CONFIG_ID               = 0x5A,
		OPEN_RX_SCAN_ID                 = 0x5B,
		
		EXT_CHANNEL_RADIO_FREQ_ID       = 0x5C,  // OBSOLETE: = for 905 radio,
		EXT_BROADCAST_DATA_ID           = 0x5D,
		EXT_ACKNOWLEDGED_DATA_ID        = 0x5E,
		EXT_BURST_DATA_ID               = 0x5F,
		
		CHANNEL_RADIO_TX_POWER_ID       = 0x60,
		GET_SERIAL_NUM_ID               = 0x61,
		GET_TEMP_CAL_ID                 = 0x62,
		SET_LP_SEARCH_TIMEOUT_ID        = 0x63,
		SET_TX_SEARCH_ON_NEXT_ID        = 0x64,
		SERIAL_NUM_SET_CHANNEL_ID_ID    = 0x65,
		RX_EXT_MESGS_ENABLE_ID          = 0x66,  
		RADIO_CONFIG_ALWAYS_ID          = 0x67,
		ENABLE_LED_FLASH_ID             = 0x68,
		XTAL_ENABLE_ID                  = 0x6D,
		ANTLIB_CONFIG_ID                = 0x6E,
		STARTUP_ID                      = 0x6F,
		AUTO_FREQ_CONFIG_ID             = 0x70,
		PROX_SEARCH_CONFIG_ID           = 0x71,
		
		SET_SEARCH_CH_PRIORITY_ID       = 0x75,
		
		
		CUBE_CMD_ID                     = 0x80,
		
		GET_PIN_DIODE_CONTROL_ID        = 0x8D,
		PIN_DIODE_CONTROL_ID            = 0x8E,
		FIT1_SET_AGC_ID                 = 0x8F,
		
		FIT1_SET_EQUIP_STATE_ID         = 0x91,  // *** CONFLICT: w/ Sensrcore, Fit1 will never have sensrcore enabled
		
		// Sensrcore Messages
		SET_CHANNEL_INPUT_MASK_ID       = 0x90,
		SET_CHANNEL_DATA_TYPE_ID        = 0x91,
		READ_PINS_FOR_SECT_ID           = 0x92,
		TIMER_SELECT_ID                 = 0x93,
		ATOD_SETTINGS_ID                = 0x94,
		SET_SHARED_ADDRESS_ID           = 0x95,
		ATOD_EXTERNAL_ENABLE_ID         = 0x96,
		ATOD_PIN_SETUP_ID               = 0x97,
		SETUP_ALARM_ID                  = 0x98,
		ALARM_VARIABLE_MODIFY_TEST_ID   = 0x99,
		PARTIAL_RESET_ID                = 0x9A,
		OVERWRITE_TEMP_CAL_ID           = 0x9B,
		SERIAL_PASSTHRU_SETTINGS_ID     = 0x9C,
		
		BIST_ID                         = 0xAA,
		UNLOCK_INTERFACE_ID             = 0xAD,
		SERIAL_ERROR_ID                 = 0xAE,
		SET_ID_STRING_ID                = 0xAF,
		
		PORT_GET_IO_STATE_ID            = 0xB4,
		PORT_SET_IO_STATE_ID            = 0xB5,
		
		RSSI_ID                         = 0xC0,
		RSSI_BROADCAST_DATA_ID          = 0xC1,
		RSSI_ACKNOWLEDGED_DATA_ID       = 0xC2,
		RSSI_BURST_DATA_ID              = 0xC3,
		RSSI_SEARCH_THRESHOLD_ID        = 0xC4,
		SLEEP_ID                        = 0xC5,
		GET_GRMN_ESN_ID                 = 0xC6,
		SET_USB_INFO_ID                 = 0xC7,
		
		HCI_COMMAND_COMPLETE            = 0xC8,
	}
}

