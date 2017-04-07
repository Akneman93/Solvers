namespace Solvers.Interfaces
{
    /// <summary>
    /// Interface for a class that provides Q value
    /// </summary>
    public interface IQProvider
	{
        /// <summary>
        /// Returns estimation of future cummulative reward for a state and operator
        /// </summary>
        /// <param name="state"></param>
        /// <param name="op"></param>
        /// <returns></returns>
		double getQ(IState state, IOperator op);
	}
}