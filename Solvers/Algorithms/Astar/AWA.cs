using Solvers.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Solvers.Algorithms.Astar
{


    class AWA : abstrA
    {       
        private double w;
        private string w_Name = "W";
        private IComparer<ANode> comparer;


        public override ISearchInfo GetSearchInfo()
        {
            ISearchInfo result = base.GetSearchInfo();
            result.SearchParameters.Add(w_Name, w.ToString());
            return result;
        }




        public AWA(ISuccGenerator<ANode> successorNodesGenerator,                          
                          IHCalculator<ANode> hValueCalculator,
                          int W)
        {
            succGen = successorNodesGenerator;            
            hCalc = hValueCalculator;                        
            w = W;            
            Name = "AWA*";
            timeAvaliable = 5000;
            comparer = new FComparer(this);
            openList = new NodeTable<ANode>(Comparer);
            closedList = new NodeTable<ANode>(Comparer);

        }



        private class FComparer : IComparer<ANode>
        {
            private AWA A;

            public FComparer (AWA A)
            {
                this.A = A;
            }

            public int Compare(ANode x, ANode y)
            {
                return A.fvalue2(x) < A.fvalue2(y) ? -1 : (A.fvalue2(x) > A.fvalue2(y) ? 1 : 0);
            }
        }        



        public override IComparer<ANode> Comparer
        {
            get
            {

                return comparer;
            }
                
                
        }





        public override void setParameters(Dictionary<string, string> parameters)
        {
            base.setParameters(parameters);
            w = double.Parse(parameters[w_Name]);
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


        public void SetWeight(int Weight)
        {
            w = Weight;
        }


        public override bool Reset()
        {          
                     
           openList.Clear();
           closedList.Clear();
           expansions = 0;
           foundGoalNode = null;           
           return true;
        }
        
      
        

        private double fvalue2(ANode n)
        {
            return n.G + w * n.H;
        }

        private double fvalue(ANode n)
        {
            return n.G + n.H;
        }
       


        public override void Execute()
        {       
           
            stopwatch.Restart();			

            while (stopwatch.Elapsed.TotalMilliseconds < timeAvaliable && !stopped)
            {                
               if (openList.Count == 0)
               {
                   isOptimalFound = true;                  
                   stopwatch.Stop();
                   return;
               }               



                ANode currentNode = openList.Min;
				openList.RemoveValue(currentNode);
                if (currentNode.Equals(goalNode))
                {
                    foundGoalNode = currentNode;

                    rewards.Add(stopwatch.Elapsed.TotalMilliseconds,currentNode.G);
                    continue;
                }


                if (foundGoalNode == null || fvalue(currentNode) < foundGoalNode.G)
                {
                    closedList.AddValue(currentNode);
                    IEnumerable<ANode> successorNodes = succGen.Generate(currentNode);
                    expansions++;                  


					nodesGenerated = nodesGenerated + successorNodes.Count();
                    foreach (ANode successorNode in successorNodes)
                    {

                        if (foundGoalNode == null ||successorNode.G + hCalc.Calculate(successorNode, goalNode) < foundGoalNode.G)
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
                }
            }


           
            stopwatch.Stop();

        }



        


    }
}
