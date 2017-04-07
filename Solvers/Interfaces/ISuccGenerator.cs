using System.Collections.Generic;

namespace Solvers.Interfaces
{
    /// <summary>
    /// Interface for class that generates nodes
    /// </summary>
    /// <typeparam name="TNode">type of generated nodes</typeparam>
    public interface ISuccGenerator<TNode>
    {

		TNode Generate(TNode n, IOperator op);

		IEnumerable<IOperator> ApplicableOperators(TNode n);

        IEnumerable<TNode> Generate(TNode n);

        IEnvironment getEnvironment();

    }
}
