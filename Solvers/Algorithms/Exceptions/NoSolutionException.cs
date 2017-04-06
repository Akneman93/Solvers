using System;
namespace Solvers.Algorithms.Exceptions
{
    public class NoSolutionException:Exception
	{
		public NoSolutionException()
		{
		}
		public NoSolutionException(string message) : base(message) { }
	}
}
