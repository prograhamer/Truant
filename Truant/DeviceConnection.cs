using System;

namespace Truant
{
    public class DeviceConnection
    {
        public Device Device { get; }
        public bool ChannelCloseRequested { get; set; } = false;

        public DeviceConnection(Device device)
        {
            Device = device;
        }
    }
}
