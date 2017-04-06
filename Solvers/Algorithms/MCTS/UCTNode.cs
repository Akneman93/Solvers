using Solvers.Interfaces;
using System;

namespace Solvers.Algorithms.MCTS
{
    public class UCTNode : INode
	{
		public UCTNode(INode node)
		{
            Parent = node.Parent;
            State = node.State;
            UsedOperator = node.UsedOperator;
        }

        public UCTNode()
        {           
        }



        public int N { get; set; }
		public double Q { get; set; }


		public int Level
		{
			get;
			set;
		}


        public IState State { get; set; }

		public INode Parent { get; set; }

        public IOperator UsedOperator { get; set; }


		override public bool Equals(Object obj)
		{
			var Node = obj as UCTNode;
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
