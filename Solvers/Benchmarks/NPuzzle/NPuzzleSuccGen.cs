using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Planning.Interfaces;
using Planning.Algorithms;

namespace Planning.NPuzzle
{
    class NPuzzleSucGen:ISuccGenerator<ANode>
    {
        int N;
		private NPuzzleGCal gCal = new NPuzzleGCal();
        public NPuzzleSucGen(int Size)
        {
            N = Size;
        }

        public IEnumerable<ANode> Generate(ANode currentNode)
        {
            var node = currentNode as ANode;

			NPuzzleState state = (NPuzzleState)currentNode.State;

            List<ANode> SuccessorNodes = new List<ANode>();

            Node succ;

			ANode ANodeSucc;

            if (state.EmptyTileIndex > N - 1)
            {
                succ = MoveOperator.UpMove(node, N);
				ANodeSucc = new ANode(succ, node);
				ANodeSucc.G = gCal.Calculate(ANodeSucc, succ.UsedOperator);
                if (!ANodeSucc.Equals(node.Parent))
                    SuccessorNodes.Add(ANodeSucc);
            }
            if (state.EmptyTileIndex < N * (N - 1))
            {
                succ = MoveOperator.DownMove(node, N);
				ANodeSucc = new ANode(succ, node);
				ANodeSucc.G = gCal.Calculate(ANodeSucc, succ.UsedOperator);

                if (!ANodeSucc.Equals(node.Parent))
                    SuccessorNodes.Add(ANodeSucc);
            }
            if (state.EmptyTileIndex % N == 0)
            {
                succ = MoveOperator.RightMove(node, N);
				ANodeSucc = new ANode(succ, node);
				ANodeSucc.G = gCal.Calculate(ANodeSucc, succ.UsedOperator);
                if (!ANodeSucc.Equals(node.Parent))
                   SuccessorNodes.Add(ANodeSucc);
            }
            else
                if (state.EmptyTileIndex % N == N - 1)
                {
                    succ = MoveOperator.LeftMove(node, N);
					ANodeSucc = new ANode(succ, node);
					ANodeSucc.G = gCal.Calculate(ANodeSucc, succ.UsedOperator);
                    if (!ANodeSucc.Equals(node.Parent))
                        SuccessorNodes.Add(ANodeSucc);
                }
                else
                {
                    succ = MoveOperator.LeftMove(node, N);
					ANodeSucc = new ANode(succ, node);
					ANodeSucc.G = gCal.Calculate(ANodeSucc, succ.UsedOperator);
                    if (!ANodeSucc.Equals(node.Parent))
                        SuccessorNodes.Add(ANodeSucc);

                    succ = MoveOperator.RightMove(node, N);
					ANodeSucc = new ANode(succ, node);
					ANodeSucc.G = gCal.Calculate(ANodeSucc, succ.UsedOperator);
                    if (!ANodeSucc.Equals(node.Parent))
                        SuccessorNodes.Add(ANodeSucc);                   
                    
                }

           

            return SuccessorNodes;

        }

        
    }
}
