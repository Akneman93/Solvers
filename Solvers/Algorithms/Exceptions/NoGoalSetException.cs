using System;
namespace Solvers.Algorithms.Exceptions
{
    public class NoGoalSetException:Exception
	{
		public NoGoalSetException()
		{
		}
		public NoGoalSetException(string message) : base(message) { }
	}
}
