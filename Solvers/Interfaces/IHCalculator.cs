using Solvers.Algorithms.Astar;

namespace Solvers.Interfaces
{
    /// <summary>
    /// Heuristic calculator
    /// </summary>
    public interface IHCalculator<TNode>
    {
        /// <summary>
        /// Estimates largest possible cummulative reward from start to goal
        /// </summary>
        /// <param name="start">start node</param>
        /// <param name="goal">goal node</param>
        /// <returns></returns>
        int Calculate(TNode start, TNode goal);
    }
}
