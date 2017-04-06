/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApplication1.NPuzzle;
using WindowsFormsApplication1.Interfaces;
using WindowsFormsApplication1.Algorithms;

namespace WindowsFormsApplication1
{
    class ARepairA : baseA
    {        
        private NodeSortedDic incons;
        private double w;
        private double dec;
        public ARepairA(ISuccGenerator successorNodesGenerator,
                          IGCalculator gValueCalculator,
                          IHCalculator hValueCalculator,
                          double W)
        {
            succGen = successorNodesGenerator;
            gCalc = gValueCalculator;
            hCalc = hValueCalculator;
            expansions = 0;
            bestSol = null;
            startNode = null;
            goalNode = null;
            openList = new NodeSortedDic();
            closedList = new NodeSortedDic();
            incons = new NodeSortedDic();
            TimePoints = new List<double>();
            Solutions = new List<double>();
            ExpansionsList = new List<int>();
            isInterrupted = false;
            Name = "ARepairA" + "(w = " + W.ToString() + ")";
            w = W;
            dec = 0.1;
        }

        public override bool InitStates(INode Start, INode Goal)
        {
            startNode = Start;
            goalNode = Goal;
            startNode.G = 0;
            startNode.H = hCalc.Ex(startNode, goalNode);
            openList.AddNod(fvalue2(startNode),startNode);
            return true;
        }        

        public bool ResetA()
        {
            openList.Clear();
            closedList.Clear();
            expansions = 0;
            bestSol = null;
            return true;
        }

        public override double Error
        {
            get
            {
               //if (openList.Count == 0 || w < 1)
                if (w < 1)
                  return 0;
               
                // else if (incumbent != null)
                // return incumbent.F - openList.Values.Aggregate((c, d) => c. < d.F ? c : d).F;
                else
                    return 999;
            }
        }

        public override int StoredCount
        {
            get
            {
                return closedList.Size() + openList.Size() + incons.Size();
            }
        }



        

        private double fvalue2(INode node)
        {
            return node.G + w * node.H;
        }
        private double fvalue(INode n)
        {
            return n.G + n.H;
        }

        public override void Execute(double TimeAvaliable)
        {
            isExecuted = false;
            timeAvaliable = TimeAvaliable;
            stopwatch.Start();
            repairA();
            while (w - dec >= 1 && !isInterrupted && stopwatch.Elapsed.TotalMilliseconds < TimeAvaliable)
            {
                w -= dec;
                openList.AddMoreNodes(incons);
                incons.Clear();
                openList.Reorder(w);
                closedList.Clear();
                repairA();
            }
            stopwatch.Stop();
            isExecuted = true;
        }


        public bool repairA()
        {
            while (!isInterrupted && stopwatch.Elapsed.TotalMilliseconds < TimeAvaliable && openList.Count != 0 && (BestSol == null || fvalue2(BestSol) > openList.First().Key))            
            
            {                               
                INode currentNode = openList.Min;               
                
                openList.RemoveNode(currentNode.GetID());

                if (currentNode.Equals(goalNode))
                {
                    bestSol = currentNode;
                    AddSolut(bestSol.G, stopwatch.Elapsed.TotalMilliseconds, expansions);
                }
                   
                

                closedList.AddNod(fvalue2(currentNode),currentNode);

                IEnumerable<INode> successorNodes = succGen.Ex(currentNode);
                expansions++;
                foreach (INode successorNode in successorNodes)
                {               
                   INode node1;
                   INode node2;
                   INode node3;
                   int keyval = successorNode.GetID();
                   bool InOpen;
                   bool InClosed;                       


                   if (InOpen = openList.TryGetNode(keyval, out node1))
                   {
                       if (node1.G > gCalc.Ex(successorNode))
                       {
                           openList.RemoveNode(keyval);
                           successorNode.G = gCalc.Ex(successorNode);
                           successorNode.H = hCalc.Ex(successorNode, goalNode);
                           successorNode.Parent = currentNode;
                           openList.AddNod(fvalue2(successorNode), successorNode);
                       }

                   }
                   
                   else                     

                       if (InClosed = closedList.TryGetNode(keyval, out node2))
                       {
                           if (node2.G > gCalc.Ex(successorNode))
                           {

                               closedList.RemoveNode(keyval);
                               successorNode.G = gCalc.Ex(successorNode);
                               successorNode.H = hCalc.Ex(successorNode, goalNode);
                               successorNode.Parent = currentNode;
                               closedList.AddNod(fvalue2(successorNode), successorNode);

                               if (incons.TryGetNode(keyval, out node3))
                                   incons.RemoveNode(keyval);

                               incons.AddNod(fvalue2(successorNode), successorNode);
                           }

                       }

                       else

                           if (!InOpen && !InClosed)
                           
                           {
                               successorNode.G = gCalc.Ex(successorNode);
                               successorNode.H = hCalc.Ex(successorNode, goalNode);
                               successorNode.Parent = currentNode;
                               openList.AddNod(fvalue2(successorNode),successorNode);
                           }             
                }
            }

            return true;
        }
    }
}*/