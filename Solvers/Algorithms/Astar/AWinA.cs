using Solvers.Interfaces;
using System.Collections.Generic;
using System.Linq;


namespace Solvers.Algorithms.Astar
{


    public class AWinA : abstrA
    {        
        private NodeTable<ANode> suspendList;

        
        private int currentLvl;

        private int winSize;
        private string winSize_Name = "Window size";
        private IComparer<ANode> comparer;





        public AWinA(ISuccGenerator<ANode> successorNodesGenerator,                         
                          IHCalculator<ANode> hValueCalculator)
        {
            comparer = new FComparer(this);
            suspendList = new NodeTable<ANode>(comparer);
            succGen = successorNodesGenerator;           
            hCalc = hValueCalculator;                     
            currentLvl = -1;
            winSize = 0;              
            Name = "AWinA*";
            timeAvaliable = 5000;
            openList = new NodeTable<ANode>(Comparer);
            closedList = new NodeTable<ANode>(Comparer);
            

        }


        private class FComparer : IComparer<ANode>
        {
            private AWinA A;

            public FComparer(AWinA A)
            {
                this.A = A;
            }

            public int Compare(ANode x, ANode y)
            {
                return A.fvalue(x) < A.fvalue(y) ? -1 : (A.fvalue(x) > A.fvalue(y) ? 1 : 0);
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
            //winSize = int.Parse(parameters[winSize_Name]);
        }





        public override ISearchInfo GetSearchInfo()
        {
            ISearchInfo result = base.GetSearchInfo();
           // result.SearchParameters.Add(winSize_Name, winSize.ToString());
            return result;
        }



        public override bool InitStates(INode Start, INode Goal)
        {
            startNode = new ANode(Start);
			goalNode = new ANode(Goal);
            startNode.G = 0;
            startNode.H = hCalc.Calculate(startNode, goalNode);
            startNode.Level = 0;
            openList.AddValue( startNode);
            return true;
        }        

        public override bool Reset()
        {
            openList.Clear();
            closedList.Clear();
            suspendList.Clear();
            expansions = 0;
            foundGoalNode = null;
            return true;
        }



   

        

        private double fvalue(ANode node)
        {
            return node.G + node.H;
        }

        public override long StoredCount
        {
            get
            {
                return openList.NumberOfElements + closedList.NumberOfElements + suspendList.NumberOfElements;
            }
        }



     


       public override void Execute()
       {         
           stopwatch.Restart();
           while (stopwatch.Elapsed.TotalMilliseconds < timeAvaliable && !stopped)
           {               
               winA(timeAvaliable);
               if (suspendList.Count == 0 || stopwatch.Elapsed.TotalMilliseconds >= timeAvaliable)
               {
                   if (suspendList.Count == 0)
                       isOptimalFound = true;    
                   stopwatch.Stop();
                   return;
               }
               closedList.AddValues(openList);
               openList.Clear();              
               openList = suspendList;
               suspendList = new NodeTable<ANode>(comparer);
               winSize += 1;
           }
           stopwatch.Stop();          

           
       }





       public bool winA(double TimeAvaliable)
        {
            currentLvl = -1;

            while (stopwatch.Elapsed.TotalMilliseconds < TimeAvaliable && !stopped)
            {
                if (openList.Count == 0)
                    return false;             


                ANode currentNode = openList.Min;

				openList.RemoveValue(currentNode);

                closedList.AddValue(currentNode);

                if (foundGoalNode != null && fvalue(currentNode) > fvalue(foundGoalNode))
                    return false;
                

                if (currentNode.Level <= currentLvl - winSize)
                {
                    closedList.RemoveValue(currentNode);
                    suspendList.AddValue(currentNode);
                    continue;
                }

                if (currentNode.Level > currentLvl)
                    currentLvl = currentNode.Level;


                if (currentNode.Equals(goalNode))
                {
                    foundGoalNode = currentNode;
                    rewards.Add(stopwatch.Elapsed.TotalMilliseconds,currentNode.G);
                    return true;
                }

                IEnumerable<ANode> successorNodes = succGen.Generate(currentNode);
                expansions++;
                nodesGenerated += successorNodes.Count();

                foreach (ANode successorNode in successorNodes)
                {
                    ANode node1;
                    ANode node2;
                    ANode node3;                  
                
                    if (openList.TryGetValue(successorNode, out node1))
                    {
                        if (node1.G > successorNode.G)
                        {                            
                            
                            successorNode.H = hCalc.Calculate(successorNode, goalNode);
                            successorNode.Level = currentNode.Level + 1;
                            

							openList.RemoveValue(node1);
                            openList.AddValue(successorNode);
                        }
                    }

                    else
                        if (suspendList.TryGetValue(successorNode, out node3))
                        {
                            if (node3.G > successorNode.G)
                            {                              
                               
                                successorNode.H = hCalc.Calculate(successorNode, goalNode);
                                successorNode.Level = currentNode.Level + 1;
                                

                                suspendList.RemoveValue(node3);
                                suspendList.AddValue(successorNode);
                            }
                        }
                        else
                            if (closedList.TryGetValue(successorNode, out node2))
                            {
                                if (node2.G > successorNode.G)
                                {
									closedList.RemoveValue(node2);                                   
                                  
                                    successorNode.H = hCalc.Calculate(successorNode, goalNode);
                                    successorNode.Level = currentNode.Level + 1;                                    
                                    openList.AddValue(successorNode);
                                }
                            }

                            else
                               
                                {
                                    successorNode.Parent = currentNode;                                   
                                    successorNode.H = hCalc.Calculate(successorNode, goalNode);
                                    successorNode.Level = currentNode.Level + 1;                                    
                                    openList.AddValue(successorNode);
                                }
                }
            }

            return false;
        }
    }

}