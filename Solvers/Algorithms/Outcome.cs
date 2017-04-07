using Solvers.Interfaces;
namespace Solvers.Algorithms
{
    /// <summary>
    /// Contains result of applying operator to state in environment
    /// </summary>
    public class Outcome:IOutcome
	{
		public Outcome(IState state, double qValue)
		{
			State = state;
			Reward = qValue;
		}

		public IState State { get; set; }
		public double Reward { get; set; }
	}
}
