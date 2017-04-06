using Solvers.Interfaces;
using System.Collections.Generic;


namespace Solvers.Algorithms.MCTS
{
    class UCTNodeSucGen : ISuccGenerator<UCTNode>
    {
        IEnvironment env;

        public UCTNodeSucGen(IEnvironment Env)
        {
            env = Env;
        }

        public IEnumerable<UCTNode> Generate(UCTNode node)
        {
            List<UCTNode> successors = new List<UCTNode>();

            foreach (IOperator op in env.ApplicableOperators(node.State))
            {
                IOutcome outcome = env.act(node.State, op);
                UCTNode succ = new UCTNode();
                succ.State = outcome.State;
                succ.UsedOperator = op;
                succ.Parent = node;
                succ.Q = double.MaxValue;
                succ.N = 0;
                successors.Add(succ);
            }

            return successors;
        }


        public IEnumerable<IOperator> ApplicableOperators(UCTNode node)
        {
            return env.ApplicableOperators(node.State);
        }

        public UCTNode Generate(UCTNode node, IOperator op)
        {
            IOutcome outcome = env.act(node.State, op);
            UCTNode succ = new UCTNode();
            succ.State = outcome.State;
            succ.UsedOperator = op;
            succ.Parent = node;
            succ.Q = double.MaxValue;
            succ.N = 0;
            return succ;
        }

        public IEnvironment getEnvironment()
        {
            return env;
        }



    }
}
