using System;
using System.Runtime.InteropServices;

namespace Truant
{
	public class AntInternal
	{
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate bool AssignResponseDelegate(byte channel, Truant.MessageType messageID);
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate bool ChannelEventDelegate(byte channel, Truant.ResponseStatus channelEvent);

		[DllImport ("libANT")]
		public static extern bool ANT_GetDeviceUSBInfo(byte ucUSBDeviceNum, byte [] pucProductString, byte [] pucSerialString);
		[DllImport ("libANT")]
		public static extern bool ANT_GetDeviceUSBPID(ushort [] pusPID_);
		[DllImport ("libANT")]
		public static extern bool ANT_GetDeviceUSBVID(ushort [] pusVID_);
		[DllImport ("libANT")]
		public static extern uint ANT_GetDeviceSerialNumber();

		[DllImport ("libANT")]
		public static extern bool ANT_Init(byte ucUSBDeviceNum_, uint ulBaudrate_);  //Initializes and opens USB connection to the module
		[DllImport ("libANT")]
		public static extern bool ANT_Init_Special(byte ucUSBDeviceNum_, uint ulBaudrate_, byte ucPortType_, byte ucSerialFramerType_);
		[DllImport ("libANT")]
		public static extern void ANT_Close();   //Closes the USB connection to the module
		[DllImport ("libANT")]
		public static extern byte [] ANT_LibVersion(); // Obtains the version number of the dynamic library

		[DllImport ("libANT")]
		public static extern void ANT_AssignResponseFunction(AssignResponseDelegate pfResponse, byte[] pucResponseBuffer);
		[DllImport ("libANT")]
		public static extern void ANT_AssignChannelEventFunction(byte ucLink, ChannelEventDelegate pfLinkEvent, byte[] pucRxBuffer);
		[DllImport ("libANT")]
		public static extern void ANT_UnassignAllResponseFunctions();	//Unassigns all response functions


		////////////////////////////////////////////////////////////////////////////////////////
		// Config Messages
		////////////////////////////////////////////////////////////////////////////////////////
		[DllImport ("libANT")]
		public static extern bool ANT_UnAssignChannel(byte ucANTChannel); // Unassign a Channel

		[DllImport ("libANT")]
		public static extern bool ANT_AssignChannel(byte ucANTChannel, ChannelType ucChanType, byte ucNetNumber);

		[DllImport ("libANT")]
		public static extern bool ANT_AssignChannelExt(byte ucANTChannel, ChannelType ucChannelType_, byte ucNetNumber, byte ucExtFlags_);

		[DllImport ("libANT")]
		public static extern bool ANT_SetChannelId(byte ucANTChannel, ushort usDeviceNumber, byte ucDeviceType, byte ucTransmissionType_);

		[DllImport ("libANT")]
		public static extern bool ANT_SetChannelPeriod(byte ucANTChannel, ushort usMesgPeriod);

		[DllImport ("libANT")]
		public static extern bool ANT_SetChannelSearchTimeout(byte ucANTChannel, byte ucSearchTimeout);   // Sets the search timeout for a give receive channel on the module

		[DllImport ("libANT")]
		public static extern bool ANT_SetChannelRFFreq(byte ucANTChannel, byte ucRFFreq);

		[DllImport ("libANT")]
		public static extern bool ANT_SetNetworkKey(byte ucNetNumber, byte [] pucKey);

		[DllImport ("libANT")]
		public static extern bool ANT_SetTransmitPower(byte ucTransmitPower);


		////////////////////////////////////////////////////////////////////////////////////////
		// Test Mode
		////////////////////////////////////////////////////////////////////////////////////////
		[DllImport ("libANT")]
		public static extern bool ANT_InitCWTestMode();
		[DllImport ("libANT")]
		public static extern bool ANT_SetCWTestMode(byte ucTransmitPower, byte ucRFChannel);

		////////////////////////////////////////////////////////////////////////////////////////
		// ANT Control messages
		////////////////////////////////////////////////////////////////////////////////////////
		[DllImport ("libANT")]
		public static extern bool ANT_ResetSystem();

		[DllImport ("libANT")]
		public static extern bool ANT_OpenChannel(byte ucANTChannel); // Opens a Channel

		[DllImport ("libANT")]
		public static extern bool ANT_CloseChannel(byte ucANTChannel); // Close a channel

		[DllImport ("libANT")]
		public static extern bool ANT_RequestMessage(byte ucANTChannel, byte ucMessageID);
		[DllImport ("libANT")]
		public static extern bool ANT_WriteMessage(byte ucMessageID, byte [] aucData, ushort usMessageSize);

		////////////////////////////////////////////////////////////////////////////////////////
		// The following are the synchronous RF event functions used to update the synchronous data sent over a channel
		////////////////////////////////////////////////////////////////////////////////////////
		[DllImport ("libANT")]
		public static extern bool ANT_SendBroadcastData(byte ucANTChannel, byte [] pucData);   // Sends broadcast data to be sent on the channel's next synchronous message period
		[DllImport ("libANT")]
		public static extern bool ANT_SendAcknowledgedData(byte ucANTChannel, byte [] pucData);  // Sends acknowledged data to be sent on the channel's next synchronous message period

		[DllImport ("libANT")]
		public static extern bool ANT_SendBurstTransferPacket(byte ucANTChannelSeq, byte [] pucData);  // Sends acknowledged data to be sent on the channel's next synchronous message period
		[DllImport ("libANT")]
		public static extern bool ANT_SendBurstTransfer(byte ucANTChannel, byte [] pucData, ushort usNumDataPackets);


		////////////////////////////////////////////////////////////////////////////////////////
		// The following functions are used with version 2 modules
		////////////////////////////////////////////////////////////////////////////////////////
		[DllImport ("libANT")]
		public static extern bool ANT_AddChannelID(byte ucANTChannel, ushort usDeviceNumber, byte ucDeviceType, byte ucTranmissionType_, byte ucIndex);
		[DllImport ("libANT")]
		public static extern bool ANT_ConfigList(byte ucANTChannel, byte ucListSize, byte ucExclude);
		[DllImport ("libANT")]
		public static extern bool ANT_OpenRxScanMode();

		////////////////////////////////////////////////////////////////////////////////////////
		// The following functions are used with AP2 modules (not AP1 or AT3)
		////////////////////////////////////////////////////////////////////////////////////////
		[DllImport ("libANT")]
		public static extern bool ANT_ConfigFrequencyAgility(byte ucANTChannel_, byte ucFreq1_, byte ucFreq2_, byte ucFreq3_);
		[DllImport ("libANT")]
		public static extern bool ANT_SetProximitySearch(byte ucANTChannel_, byte ucSearchThreshold_);
		[DllImport ("libANT")]
		public static extern bool ANT_SleepMessage();
		[DllImport ("libANT")]
		public static extern bool ANT_CrystalEnable();

		////////////////////////////////////////////////////////////////////////////////////////
		// The following are NVM specific functions
		////////////////////////////////////////////////////////////////////////////////////////
		[DllImport ("libANT")]
		public static extern bool ANT_NVM_Write(byte ucSize, byte [] pucData);
		[DllImport ("libANT")]
		public static extern bool ANT_NVM_Clear(byte ucSectNumber);
		[DllImport ("libANT")]
		public static extern bool ANT_NVM_Dump();
		[DllImport ("libANT")]
		public static extern bool ANT_NVM_SetDefaultSector(byte ucSectNumber);
		[DllImport ("libANT")]
		public static extern bool ANT_NVM_EndSector();
		[DllImport ("libANT")]
		public static extern bool ANT_NVM_Lock();

		////////////////////////////////////////////////////////////////////////////////////////
		// Rx Scan Additional Data Transmit Functions
		////////////////////////////////////////////////////////////////////////////////////////
		[DllImport ("libANT")]
		public static extern bool ANT_SendExtBroadcastData(byte ucANTChannel, byte [] pucData);     // Sends broadcast data to be sent on the channel's next synchronous message period
		[DllImport ("libANT")]
		public static extern bool ANT_SendExtAcknowledgedData(byte ucANTChannel, byte [] pucData);  // Sends acknowledged data to be sent on the channel's next synchronous message period

		[DllImport ("libANT")]
		public static extern bool ANT_SendExtBurstTransferPacket(byte ucANTChannelSeq, byte [] pucData);  // Sends acknowledged data to be sent on the channel's next synchronous message period
		[DllImport ("libANT")]
		public static extern ushort ANT_SendExtBurstTransfer(byte ucANTChannel, byte [] pucData, ushort usNumDataPackets);

		[DllImport ("libANT")]
		public static extern bool ANT_RxExtMesgsEnable(byte ucEnable);
		[DllImport ("libANT")]
		public static extern bool ANT_SetLowPriorityChannelSearchTimeout(byte ucANTChannel, byte ucSearchTimeout);

		[DllImport ("libANT")]
		public static extern bool ANT_SetSerialNumChannelId(byte ucANTChannel, byte ucDeviceType, byte ucTransmissionType);
		[DllImport ("libANT")]
		public static extern bool ANT_EnableLED(byte ucEnable);
		[DllImport ("libANT")]
		public static extern bool ANT_SetChannelTxPower(byte ucANTChannel, byte ucTransmitPower);

		[DllImport ("libANT")]
		public static extern bool ANT_RSSI_SetSearchThreshold(byte ucANTChannel_, byte ucThreshold_);
	}
}

