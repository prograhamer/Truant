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
			var processor = new RateProcessor (65536);

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
			Assert.AreEqual (0.00143, Math.Round((double) processor.Rate, 5));
		}

		[Test]
		public void ProcessEventRateOverflow ()
		{
			var processor = new RateProcessor (65536);

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
			Assert.AreEqual (0.00141, Math.Round((double) processor.Rate, 5));
		}
	}
}

