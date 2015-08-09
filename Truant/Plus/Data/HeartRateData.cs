using System;
using System.ComponentModel;

namespace Truant.Plus.Data
{
	public class HeartRateData
	{
		public int HeartBeatEventTime { get; set; }
		public int HeartBeatCount { get; set; }
		public int ComputedHeartRate { get; set; }

		public int CumulativeOperatingTime{ get; set; }

		public int ManufacturerID{ get; set; }
		public int SerialNumber{ get; set; }

		public int HardwareVersion{ get; set; }
		public int SoftwareVersion{ get; set; }
		public int ModelNumber{ get; set; }

		public int PreviousHeartBeatEventTime{ get; set; }

		public HeartRateData ()
		{
		}

		public override String ToString()
		{
			String representation = "";
			bool first = true;

			foreach(PropertyDescriptor descriptor in TypeDescriptor.GetProperties(this))
			{
				string name=descriptor.Name;
				object value=descriptor.GetValue(this);

				if(!first) {
					representation += ", ";
				}
				representation += name + "=" + value;

				first = false;
			}
			return representation;
		}
	}
}

