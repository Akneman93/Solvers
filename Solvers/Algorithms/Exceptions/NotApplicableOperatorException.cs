using System;
namespace Solvers.Algorithms.Exceptions
{
    public class NotApplicableOperatorException: Exception
	{
		public NotApplicableOperatorException()
		{
		}
		public NotApplicableOperatorException(string message) : base(message) { }
	}
}
