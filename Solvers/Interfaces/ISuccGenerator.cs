using System.Collections.Generic;

namespace Solvers.Interfaces
{
    public interface ISuccGenerator<TNode>
    {

		TNode Generate(TNode n, IOperator op);

		IEnumerable<IOperator> ApplicableOperators(TNode n);

        IEnumerable<TNode> Generate(TNode n);

        IEnvironment getEnvironment();

    }
}
