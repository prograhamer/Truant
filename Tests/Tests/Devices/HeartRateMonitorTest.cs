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

			sensor.interpretReceivedData(rxData);

			Assert.IsNull(sensor.CumulativeOperatingTime);
			Assert.IsNull(sensor.ManufacturerID);
			Assert.IsNull(sensor.SerialNumber);
			Assert.IsNull(sensor.HardwareVersion);
			Assert.IsNull(sensor.SoftwareVersion);
			Assert.IsNull(sensor.ModelNumber);
			Assert.IsNull(sensor.PreviousHeartBeatEventTime);

			Assert.AreEqual(61456, sensor.HeartBeatEventTime);
			Assert.AreEqual(127, sensor.HeartBeatCount);
			Assert.AreEqual(74, sensor.ComputedHeartRate);
		}
	}
}

