using System;
using System.Threading;
using Truant;
using Truant.Devices;

namespace TruantDemo
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			var connection = AntPlusConnection.GetConnection(0 /* USB device */, 0 /* network no */);

			connection.Connect();

			var hrMonitor = new HeartRateMonitor();
			var speedCadence = new BikeSpeedCadenceSensor(2096);

			connection.AddDevice(hrMonitor);
			connection.AddDevice(speedCadence);

			for(int i = 0; i < 45; i++)
			{
				Thread.Sleep(1000);
				Console.WriteLine("Heart rate: " + hrMonitor.Data.ComputedHeartRate);
				Console.WriteLine("HR device config: " + hrMonitor.Config);
				Console.WriteLine("Instantaneous speed: " + speedCadence.Data.Speed);
				Console.WriteLine ("Instantaneous cadence: " + speedCadence.Data.Cadence);
			}

			connection.Disconnect();
		}
	}
}
