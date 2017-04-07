namespace Solvers.Interfaces
{
    public interface IPolicy
	{
        /// <summary>
        ///Returns operator to use in state
        /// </summary>
        /// <param name="s">State of the environment</param>
        /// <returns></returns>
		IOperator action(IState s);



        /// <summary>
        ///Returns probability of using operator in state
        /// </summary>
        /// <param name="s">state of the environment</param>
        /// <returns></returns>
		double actionProb(IState s, IOperator a);



        /// <summary>
        /// Determines if this policy is defined for state s
        /// </summary>
        /// <param name="s">State of the environment</param>
        /// <returns></returns>
		bool definedFor(IState s);



        /// <summary>
        /// Determines if this state is goal state
        /// </summary>
        /// <param name="s">State of the environment</param>
        /// <returns></returns>
        bool isGoal(IState s);

	}

}
