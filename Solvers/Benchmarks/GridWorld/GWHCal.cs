using Solvers.Algorithms;
using Solvers.Algorithms.Astar;
using Solvers.Interfaces;

namespace Solvers.GridWorldBench
{
    public class GWHCal:IHCalculator<ANode>
    {
        int blockSize;
        public int BlockSize
        {
            get
            {
                return blockSize;
            }
            set
            {
                blockSize = value;
            }
        }
        public GWHCal(int BlockSize)
        {
            blockSize = BlockSize;
        }
        public int Calculate(ANode Node, ANode Goal)
        {
			var node = Node;
            var goal = Goal;

			GridWorldState node_state = (GridWorldState)node.State;
			GridWorldState goal_state = (GridWorldState)goal.State;




            int positive1 = (node_state.X - goal_state.X)/blockSize;
            positive1 = positive1 > 0 ? positive1 : -positive1;
            int positive2 = (node_state.Y - goal_state.Y)/blockSize;
            positive2 = positive2 > 0 ? positive2 : -positive2;
            return positive1 + positive2;
        }

    }
}
