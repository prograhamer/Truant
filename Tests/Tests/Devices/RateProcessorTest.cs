using System;
using NUnit.Framework;
using Truant.Processors;

namespace Tests
{
	[TestFixture]
	public class RateProcessorTest
	{
		[Test]
		public void ProcessRateEvent ()
		{
			var processor = new RateProcessor (65536, 65536);

			Assert.IsNull (processor.EventTime);
			Assert.IsNull (processor.EventCount);
			Assert.IsNull (processor.Rate);

			processor.ProcessRateEvent(2048, 3);

			Assert.AreEqual (2048, processor.EventTime);
			Assert.AreEqual (3, processor.EventCount);
			Assert.IsNull (processor.Rate);

			processor.ProcessRateEvent(4148, 6);

			Assert.AreEqual (4148, processor.EventTime);
			Assert.AreEqual (6, processor.EventCount);
			Assert.AreEqual (1.46, Math.Round((double) processor.Rate, 2));
		}

		[Test]
		public void ProcessRateEventOverflow ()
		{
			var processor = new RateProcessor (65536, 65536);

			Assert.IsNull (processor.EventTime);
			Assert.IsNull (processor.EventCount);
			Assert.IsNull (processor.Rate);

			processor.ProcessRateEvent(65530, 65534);

			Assert.AreEqual (65530, processor.EventTime);
			Assert.AreEqual (65534, processor.EventCount);
			Assert.IsNull (processor.Rate);

			processor.ProcessRateEvent(2831, 2);

			Assert.AreEqual (2831, processor.EventTime);
			Assert.AreEqual (2, processor.EventCount);
			Assert.AreEqual (1.44, Math.Round((double) processor.Rate, 2));
		}

		[Test]
		public void ProcessRateEvent65536_256 ()
		{
			var processor = new RateProcessor (65536, 256);

			Assert.IsNull (processor.EventTime);
			Assert.IsNull (processor.EventCount);
			Assert.IsNull (processor.Rate);

			processor.ProcessRateEvent(65530, 254);

			Assert.AreEqual (65530, processor.EventTime);
			Assert.AreEqual (254, processor.EventCount);
			Assert.IsNull (processor.Rate);

			processor.ProcessRateEvent(2831, 2);

			Assert.AreEqual (2831, processor.EventTime);
			Assert.AreEqual (2, processor.EventCount);
			Assert.AreEqual (1.44, Math.Round((double) processor.Rate, 2));
		}

		[Test]
		public void ProcessRateEvent256_65536 ()
		{
			var processor = new RateProcessor (256, 65536);

			Assert.IsNull (processor.EventTime);
			Assert.IsNull (processor.EventCount);
			Assert.IsNull (processor.Rate);

			processor.ProcessRateEvent(250, 65534);

			Assert.AreEqual (250, processor.EventTime);
			Assert.AreEqual (65534, processor.EventCount);
			Assert.IsNull (processor.Rate);

			processor.ProcessRateEvent(57, 2);

			Assert.AreEqual (57, processor.EventTime);
			Assert.AreEqual (2, processor.EventCount);
			Assert.AreEqual (65.02, Math.Round((double) processor.Rate, 2));
		}
	}
}

