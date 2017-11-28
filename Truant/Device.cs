using System;
using System.Collections.Generic;

namespace Truant
{
	public abstract class Device
	{
		public byte DeviceType { get; protected set; }
		public byte RadioFrequency { get; protected set; }
		public ushort ChannelPeriod { get; protected set; }
		long? LastReceivedTicks { get; set; }
		public TimeSpan? DataReceiptTimeSpan {
			get {
				if (LastReceivedTicks != null) {
					return new TimeSpan(DateTime.UtcNow.Ticks - (long)LastReceivedTicks);
				} else {
					return null;
				}
			}
		}

		internal AntConnection Connection { get; set; }

		public DeviceConfig Config { get; set; }
		public DeviceStatus Status { get; set; }

		public delegate void NewDataCallback(ushort id, object data);
		protected List<NewDataCallback> NewDataCallbacks = new List<NewDataCallback>();

		public Device() : this(new DeviceConfig())
		{
		}

		public Device(DeviceConfig config)
		{
			this.Config = config;
			if (this.Config.DeviceID != 0) {
				this.Status = DeviceStatus.PAIRED;
			}
		}

		public void ReceiveData(byte[] data)
		{
			LastReceivedTicks = DateTime.UtcNow.Ticks;

			lock (this) {
				InterpretReceivedData(data);

				TriggerNewDataCallbacks();
			}
		}

		public void AddNewDataCallback(NewDataCallback callback)
		{
			NewDataCallbacks.Add(callback);
		}

		protected void SendBroadcastData(byte[] data)
		{
			if (Connection != null) {
				Connection.SendBroadcastData(this, data);
			}
		}

		protected void SendAcknowledgedData(byte[] data)
		{
			if (Connection != null) {
				Connection.SendAcknowledgedData(this, data);
			}
		}

		protected abstract void InterpretReceivedData(byte[] data);
		protected abstract void TriggerNewDataCallbacks();
	}
}