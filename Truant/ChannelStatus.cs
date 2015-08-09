namespace Truant
{
	public enum ChannelStatus : byte
	{
		CHANNEL_STATE_MASK                  = 0x03,
		UNASSIGNED_CHANNEL                  = 0x00,
		ASSIGNED_CHANNEL                    = 0x01,
		SEARCHING_CHANNEL                   = 0x02,
		TRACKING_CHANNEL                    = 0x03,
	}
}

