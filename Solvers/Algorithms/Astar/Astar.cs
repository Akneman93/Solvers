using Solvers.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Solvers.Algorithms.Astar
{


    class Astar : abstrA
    {
        private IComparer<ANode> comparer;

        public Astar(ISuccGenerator<ANode> successorNodesGenerator,                         
                          IHCalculator<ANode> hValueCalculator)
        {
            comparer = new FComparer(this);
            succGen = successorNodesGenerator;           
            hCalc = hValueCalculator;                                
            Name = "A*";
            timeAvaliable = 5000;
            openList = new NodeTable<ANode>(Comparer);
            closedList = new NodeTable<ANode>(Comparer);


        }

        private class FComparer : IComparer<ANode>
        {
            private Astar A;

            public FComparer(Astar A)
            {
                this.A = A;
            }

            public int Compare(ANode x, ANode y)
            {
                return A.fvalue(x) < A.fvalue(y) ? -1 : (A.fvalue(x) > A.fvalue(y) ? 1 : 0);
            }
        }





        public override bool InitStates(INode Start, INode Goal)
        {
            startNode = new ANode(Start);
			goalNode = new ANode(Goal);
            startNode.G = 0;
            startNode.H = hCalc.Calculate(startNode, goalNode);
            openList.AddValue(startNode);
            return true;
        }

        public override bool Reset()
        {
            openList.Clear();
            closedList.Clear();
            expansions = 0;
            foundGoalNode = null;
         
            return true;
        }




        public override IComparer<ANode> Comparer
        {
            get
            {

                return comparer;
            }


        }








        private double fvalue(ANode n)
        {
            return n.G + n.H;
        }


        public override void Execute()
        {
           
            timeAvaliable = TimeAvaliable;
            stopwatch.Restart();
            Search(TimeAvaliable);
            stopwatch.Stop();
        }







        private bool Search(double timeAvaliable)
        {
            while (stopwatch.Elapsed.TotalMilliseconds < timeAvaliable && !stopped)
            {
                if (openList.Count == 0)
                {
                    isOptimalFound = true;
                    return true;
                }
                ANode currentNode = openList.Min;
				openList.RemoveValue(currentNode);
                if (currentNode.Equals(goalNode))
                {
                    foundGoalNode = currentNode;
                    isOptimalFound = true;

                    rewards.Add(stopwatch.Elapsed.TotalMilliseconds,currentNode.G);                   
                    return false;
                }
                
                    closedList.AddValue(currentNode);
                    IEnumerable<ANode> successorNodes = succGen.Generate(currentNode);
                    expansions++;
                    nodesGenerated = nodesGenerated + successorNodes.Count();
                foreach (ANode successorNode in successorNodes)
                    {                       

                            ANode node1;
                            ANode node2;                           
                            bool InOpen;
                            bool InClosed;


                            if (InOpen = openList.TryGetValue(successorNode, out node1))
                            {
                                if (node1.G > successorNode.G)
                                {
                                    
                                    successorNode.H = hCalc.Calculate(successorNode, goalNode);


									openList.RemoveValue(node1);
                                    openList.AddValue(successorNode);
                                }


                            }

                            else

                                if (InClosed = closedList.TryGetValue(successorNode, out node2))
                                {
									if (node2.G > successorNode.G)
                                    {
										closedList.RemoveValue(node2);                                        
                                        successorNode.H = hCalc.Calculate(successorNode, goalNode);

                                        openList.AddValue(successorNode);
                                    }
                                }

                                else
                                {                                   
                                    successorNode.H = hCalc.Calculate(successorNode, goalNode);
                                    openList.AddValue(successorNode);
                                }
                        }
                    }



            return false;
        }       



    }
}
