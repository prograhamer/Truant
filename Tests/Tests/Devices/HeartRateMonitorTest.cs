using System;
using NUnit.Framework;
using Truant.Devices;

namespace Tests
{
	[TestFixture]
	public class HeartRateMonitorTest
	{
		[Test]
		public void Page0Data()
		{
			var sensor = new HeartRateMonitor();
			byte [] rxData;

			rxData = new byte[] {
				0,
				0,
				0, 0, 0,    // Reserved bytes
				0x10, 0xF0, // Event time
				0x7F,       // Heart beat count
				0x4A,       // Heart rate
			};

			sensor.ReceiveData(rxData);

			Assert.IsNull(sensor.Data.CumulativeOperatingTime);
			Assert.IsNull(sensor.Data.ManufacturerID);
			Assert.IsNull(sensor.Data.SerialNumber);
			Assert.IsNull(sensor.Data.HardwareVersion);
			Assert.IsNull(sensor.Data.SoftwareVersion);
			Assert.IsNull(sensor.Data.ModelNumber);
			Assert.IsNull(sensor.Data.PreviousHeartBeatEventTime);

			Assert.AreEqual(61456, sensor.Data.HeartBeatEventTime);
			Assert.AreEqual(127, sensor.Data.HeartBeatCount);
			Assert.AreEqual(74, sensor.Data.ComputedHeartRate);
		}
	}
}

