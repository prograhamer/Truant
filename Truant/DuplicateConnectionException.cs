using System;
namespace Truant
{
	public class DuplicateConnectionException : Exception
	{
		public DuplicateConnectionException(string message) : base(message)
		{
		}
	}
}

