using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Solvers.Interfaces;

namespace Solvers.Algorithms.TD
{
    class QNodeSucGen : ISuccGenerator<QNode>
    {
        IEnvironment env;

        public QNodeSucGen(IEnvironment Env)
        {
            env = Env;
        }

        public IEnumerable<QNode> Generate(QNode node)
        {
            List<QNode> successors = new List<QNode>();

            foreach (IOperator op in env.ApplicableOperators(node.State))
            {
                IOutcome outcome = env.act(node.State, op);
                QNode succ = new QNode();
                succ.State = outcome.State;
                succ.Parent = node;
                succ.UsedOperator = op;
                succ.Q = 0;
                successors.Add(succ);
            }

            return successors;
        }


        public IEnumerable<IOperator> ApplicableOperators(QNode node)
        {
            return env.ApplicableOperators(node.State);
        }

        public QNode Generate(QNode node, IOperator op)
        {
            IOutcome outcome = env.act(node.State, op);
            QNode succ = new QNode();
            succ.State = outcome.State;
            succ.Parent = node;
            succ.UsedOperator = op;
            succ.Q = 0;           
            return succ;
        }

        public IEnvironment getEnvironment()
        {
            return env;
        }



    }
}
