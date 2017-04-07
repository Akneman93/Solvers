using Solvers.Interfaces;
using System.Collections.Generic;



namespace Solvers.Algorithms.Astar
{
    class ANodeSucGen : ISuccGenerator<ANode>
    {
		IEnvironment env;      
        
       

        public ANodeSucGen(IEnvironment Env)
        {
			env = Env;            
        }

        public IEnumerable<ANode> Generate(ANode node)
        {
			List<ANode> successors = new List<ANode>();

			foreach (IOperator op in env.ApplicableOperators(node.State))
			{
				IOutcome outcome = env.act(node.State, op);
				ANode succ = new ANode();
                succ.State = outcome.State;
                succ.UsedOperator = op;
                succ.Parent = node;
                succ.G = (int)(node.G - outcome.Reward);
				successors.Add(succ);
			}

			return successors;
        }


		public IEnumerable<IOperator> ApplicableOperators(ANode node)
		{
			return env.ApplicableOperators(node.State);
		}

		public ANode Generate(ANode node, IOperator op)
		{
			IOutcome outcome = env.act(node.State, op);
			ANode succ = new ANode();
            succ.State = outcome.State;
            succ.UsedOperator = op;
            succ.Parent = node;
            succ.G = (int)(node.G - outcome.Reward);
			return succ;
		}

        public IEnvironment getEnvironment()
        {
            return env;
        }



    }
}
