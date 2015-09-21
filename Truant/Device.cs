using System;

namespace Truant
{
	public abstract class Device
	{
		public byte DeviceType { get; protected set; }
		public byte RadioFrequency { get; protected set; }
		public ushort ChannelPeriod { get; protected set; }

		internal AntConnection Connection { get; set; }

		public DeviceConfig Config { get; set; }
		public DeviceStatus Status { get; set; }

		public Device()
		{
			this.Status = DeviceStatus.UNPAIRED;
			this.Config = new DeviceConfig();
		}

		public Device(DeviceConfig config)
		{
			this.Config = config;
			if(this.Config.DeviceID != 0)
			{
				this.Status = DeviceStatus.PAIRED;
			}
		}

		protected void SendBroadcastData(byte [] data)
		{
			if(Connection != null)
			{
				Connection.SendBroadcastData(this, data);
			}
		}

		protected void SendAcknowledgedData(byte [] data)
		{
			if(Connection != null)
			{
				Connection.SendAcknowledgedData(this, data);
			}
		}

		public abstract void InterpretReceivedData(byte [] data);
	}
}

