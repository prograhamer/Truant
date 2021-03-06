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

			Assert.IsNull(sensor.Data.Cadence);

			rxData = new byte[] {
				0,
				0x00, 0x08, // Event time
				0x03, 0x00, // Rev count
				0, 0, 0, 0, // Speed data
			};
			sensor.ReceiveData(rxData);
			Assert.IsNull(sensor.Data.Cadence);

			rxData = new byte[] {
				0,
				0x34, 0x10, // Event time
				0x06, 0x00, // Rev count
				0, 0, 0, 0, // Speed data
			};
			sensor.ReceiveData(rxData);
			Assert.AreEqual(87.77, Math.Round((double) sensor.Data.Cadence, 2));
		}

		[Test]
		public void CadenceOverflow()
		{
			var sensor = new BikeSpeedCadenceSensor(2096);
			byte [] rxData;

			Assert.IsNull(sensor.Data.Cadence);

			rxData = new byte[] {
				0,
				0xFA, 0xFF, // Event time
				0xFE, 0xFF, // Rev count
				0, 0, 0, 0, // Speed data
			};
			sensor.ReceiveData(rxData);
			Assert.IsNull(sensor.Data.Cadence);

			rxData = new byte[] {
				0,
				0x0F, 0x0B, // Event time
				0x02, 0x00, // Rev count
				0, 0, 0, 0, // Speed data
			};

			sensor.ReceiveData(rxData);
			Assert.AreEqual(86.63, Math.Round((double) sensor.Data.Cadence, 2));

		}

		[Test]
		public void SpeedCalculation ()
		{
			var sensor = new BikeSpeedCadenceSensor(2096);
			byte [] rxData;

			Assert.AreEqual(2096, sensor.WheelSize);
			Assert.IsNull(sensor.Data.Speed);

			rxData = new byte[] {
				0,
				0, 0, 0, 0, // Cadence data
				0x00, 0x08, // Event time
				0x03, 0x00, // Rev count
			};
			sensor.ReceiveData(rxData);
			Assert.IsNull(sensor.Data.Speed);

			rxData = new byte[] {
				0,
				0, 0, 0, 0, // Cadence data
				0xDB, 0x0B, // Event time
				0x06, 0x00, // Rev count
			};
			sensor.ReceiveData(rxData);
			Assert.AreEqual(23.49, Math.Round((double) sensor.Data.Speed, 2));
		}

		[Test]
		public void SpeedOverflow()
		{
			var sensor = new BikeSpeedCadenceSensor(2096);
			byte [] rxData;

			Assert.IsNull(sensor.Data.Speed);

			rxData = new byte[] {
				0,
				0, 0, 0, 0, // Cadence data
				0xB9, 0xFF, // Event time
				0xFF, 0xFF, // Rev count
			};
			sensor.ReceiveData(rxData);
			Assert.IsNull(sensor.Data.Speed);

			rxData = new byte[] {
				0,
				0, 0, 0, 0, // Cadence data
				0x8F, 0x04, // Event time
				0x03, 0x00, // Rev count
			};

			sensor.ReceiveData(rxData);
			Assert.AreEqual(24.97, Math.Round((double) sensor.Data.Speed, 2));
		}
	}
}
