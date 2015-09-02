using System;
using NUnit.Framework;

using Truant.Devices;

namespace Tests
{
	[TestFixture]
	public class BikeSpeedCadenceSensorTest
	{
		[Test]
		public void CadenceCalculation ()
		{
			var sensor = new BikeSpeedCadenceSensor(2096);
			byte [] rxData;

			Assert.IsNull(sensor.Cadence);

			rxData = new byte[] {
				0,
				0x00, 0x08, // Event time
				0x03, 0x00, // Rev count
				0, 0, 0, 0, // Speed data
			};
			sensor.interpretReceivedData(rxData);
			Assert.IsNull(sensor.Cadence);
			Assert.AreEqual(3, sensor.CadenceRevolutionCount);
			Assert.AreEqual(2048, sensor.CadenceEventTime);

			rxData = new byte[] {
				0,
				0x34, 0x10, // Event time
				0x06, 0x00, // Rev count
				0, 0, 0, 0, // Speed data
			};
			sensor.interpretReceivedData(rxData);
			Assert.AreEqual(6, sensor.CadenceRevolutionCount);
			Assert.AreEqual(4148, sensor.CadenceEventTime);
			Assert.AreEqual(87.77, Math.Round((double) sensor.Cadence, 2));
		}

		[Test]
		public void CadenceOverflow()
		{
			var sensor = new BikeSpeedCadenceSensor(2096);
			byte [] rxData;

			Assert.IsNull(sensor.Cadence);

			rxData = new byte[] {
				0,
				0xFA, 0xFF, // Event time
				0xFE, 0xFF, // Rev count
				0, 0, 0, 0, // Speed data
			};
			sensor.interpretReceivedData(rxData);
			Assert.IsNull(sensor.Cadence);
			Assert.AreEqual(65534, sensor.CadenceRevolutionCount);
			Assert.AreEqual(65530, sensor.CadenceEventTime);

			rxData = new byte[] {
				0,
				0x0F, 0x0B, // Event time
				0x02, 0x00, // Rev count
				0, 0, 0, 0, // Speed data
			};

			sensor.interpretReceivedData(rxData);
			Assert.AreEqual(2, sensor.CadenceRevolutionCount);
			Assert.AreEqual(2831, sensor.CadenceEventTime);
			Assert.AreEqual(86.63, Math.Round((double) sensor.Cadence, 2));

		}

		[Test]
		public void SpeedCalculation ()
		{
			var sensor = new BikeSpeedCadenceSensor(2096);
			byte [] rxData;

			Assert.IsNull(sensor.Speed);

			rxData = new byte[] {
				0,
				0, 0, 0, 0, // Cadence data
				0x00, 0x08, // Event time
				0x03, 0x00, // Rev count
			};
			sensor.interpretReceivedData(rxData);
			Assert.IsNull(sensor.Speed);
			Assert.AreEqual(3, sensor.SpeedRevolutionCount);
			Assert.AreEqual(2048, sensor.SpeedEventTime);

			rxData = new byte[] {
				0,
				0, 0, 0, 0, // Cadence data
				0xDB, 0x0B, // Event time
				0x06, 0x00, // Rev count
			};
			sensor.interpretReceivedData(rxData);
			Assert.AreEqual(6, sensor.SpeedRevolutionCount);
			Assert.AreEqual(3035, sensor.SpeedEventTime);
			Assert.AreEqual(23.49, Math.Round((double) sensor.Speed, 2));
		}

		[Test]
		public void SpeedOverflow()
		{
			var sensor = new BikeSpeedCadenceSensor(2096);
			byte [] rxData;

			Assert.IsNull(sensor.Speed);

			rxData = new byte[] {
				0,
				0, 0, 0, 0, // Cadence data
				0xB9, 0xFF, // Event time
				0xFF, 0xFF, // Rev count
			};
			sensor.interpretReceivedData(rxData);
			Assert.IsNull(sensor.Speed);
			Assert.AreEqual(65535, sensor.SpeedRevolutionCount);
			Assert.AreEqual(65465, sensor.SpeedEventTime);

			rxData = new byte[] {
				0,
				0, 0, 0, 0, // Cadence data
				0x8F, 0x04, // Event time
				0x03, 0x00, // Rev count
			};

			sensor.interpretReceivedData(rxData);
			Assert.AreEqual(3, sensor.SpeedRevolutionCount);
			Assert.AreEqual(1167, sensor.SpeedEventTime);
			Assert.AreEqual(24.97, Math.Round((double) sensor.Speed, 2));
		}
	}
}
