using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Solvers.Interfaces;

namespace Solvers.Algorithms.TD
{
    
    public class QNode : INode
    {
        public QNode(INode node)
        {
            Parent = node.Parent;
            State = node.State;
            UsedOperator = node.UsedOperator;
        }

        public QNode()
        {            
        }

        public double Q { get; set; }

        public IState State { get; set; }

        public INode Parent { get; set; }

        public IOperator UsedOperator { get; set; }


        override public bool Equals(Object obj)
        {
            var Node = obj as QNode;
            if (obj == null)
                return false;
            return Node.State.Equals(this.State);
        }



        override public int GetHashCode()
        {
            return this.State.GetHashCode();
        }


    }


    

}
