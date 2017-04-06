using Solvers.Interfaces;
using System;

namespace Solvers.Algorithms.Astar
{

    /// <summary>
    /// represents node for A*-based algorithms
    /// </summary>
    public class ANode : INode
	{

		public INode Parent { get; set; }

		public int Level{ get; set; }
		public double G { get; set; }
		public double H { get; set; }


        public IState State { get; set; }

        public IOperator UsedOperator { get; set; }


		public ANode(INode node)
		{
			Parent = node.Parent;
            State = node.State;
            UsedOperator = node.UsedOperator;
		}

        public ANode()
        {            
        }


        override public bool Equals(Object obj)
        {
            var Node = obj as ANode;
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