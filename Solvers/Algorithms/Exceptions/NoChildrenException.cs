using System;
namespace Solvers.Algorithms.Exceptions
{
    public class NoChildrenException : Exception
	{
		public NoChildrenException()
		{
		}
		public NoChildrenException(string message) : base(message) { }
	}
}
