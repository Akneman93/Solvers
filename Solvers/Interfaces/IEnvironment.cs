using System.Collections.Generic;
namespace Solvers.Interfaces
{
    /// <summary>
    /// represents environment in which agent work
    /// </summary>
    public interface IEnvironment
	{
        /// <summary>
        /// performs action
        /// </summary>
        /// <param name="currentState">current state of the environment</param>
        /// <param name="op">operator to use in current state</param>
        /// <returns>result of usage of operator in current state</returns>
		IOutcome act(IState currentState, IOperator op);




        /// <summary>
        /// returns list of all applicable operators in current state
        /// </summary>
        /// <param name="s"> current state </param>
        /// <returns>list of all applicable operators in current state</returns>
		IEnumerable<IOperator> ApplicableOperators(IState s);
	}
}
