using System;
namespace Truant
{
	public class InitializationException : Exception
	{
		public InitializationException(string message) : base(message)
		{
		}
	}
}