using System;
using System.Threading;
using Truant.Plus;
using Truant.Plus.Devices;

namespace TruantDemo
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			var connection = AntPlusConnection.GetConnection(0 /* USB device */, 0 /* network no */);

			connection.Connect();
			var hrMonitor = new HeartRateMonitor();
			connection.AddDevice(hrMonitor);

			for(int i = 0; i < 45; i++)
			{
				Thread.Sleep(1000);
				Console.WriteLine("Heart rate: " + hrMonitor.ComputedHeartRate);
			}

			connection.Disconnect();
		}
	}
}
