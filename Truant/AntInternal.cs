using System;
using System.Runtime.InteropServices;

namespace Truant
{
	public class AntInternal
	{		
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
	}
}

