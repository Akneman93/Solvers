using Solvers.Interfaces;
namespace Solvers.Algorithms
{
    public class Outcome:IOutcome
	{
		public Outcome(IState state, double qValue)
		{
			State = state;
			QValue = qValue;
		}

		public IState State { get; set; }
		public double QValue { get; set; }
	}
}
