using System;
using System.Threading;
using Truant;

namespace TruantDemo
{
	class MainClass
	{
		private const byte NETWORK_NO = 0;
		private const byte CHANNEL_NO = 0;
		private const uint BAUD = 57600;

		public static void Main (string[] args)
		{
			bool result;

			result = AntInternal.ANT_Init (0 /* Device number */, BAUD);
			Console.WriteLine ("ANT_Init(): " + result);

			Console.WriteLine ("Resetting module!");
			result = AntInternal.ANT_ResetSystem ();
			Console.WriteLine ("ANT_ResetSystem(): " + result);
			Thread.Sleep (1000);

			Truant.Plus.HeartRateMonitor hrMonitor = new Truant.Plus.HeartRateMonitor (CHANNEL_NO, NETWORK_NO);
			hrMonitor.Connect ();

			hrMonitor.RequestChannelID ();

			Thread.Sleep (30000);

			hrMonitor.Disconnect ();
			Thread.Sleep (1000);

			AntInternal.ANT_Close ();
		}
	}
}
