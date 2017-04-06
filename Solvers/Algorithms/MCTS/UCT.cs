using Solvers.Algorithms.Exceptions;
using Solvers.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Solvers.Algorithms.MCTS
{
    public class UCT:ISolver
	{

		private double Cp;
        private string Cp_Name = "Cp";

        private Dictionary<UCTNode,IEnumerable<UCTNode>> childrenDic = new Dictionary<UCTNode, IEnumerable<UCTNode>>();

		private ISuccGenerator<UCTNode> SuccGen;		

        private int maxDepth;
        private string maxDepth_Name = "Max depth";


        public readonly Stopwatch stopwatch = new Stopwatch();

        private double timeAvailable;
        private string timeAvailable_Name = "Available time";

        private int NumberOfGeneratedNodes = 0;
        private string NumberOfGeneratedNodes_Name = "Number of generated nodes";

        private volatile bool stopped = false; 

        private IEnvironment Env;


        private UCTNode Goal;

        private UCTNode Start;

        private UCTNode FoundGoal;



        public void setParameters(Dictionary<string, string> parameters)
        {
            maxDepth = int.Parse(parameters[maxDepth_Name]);
            timeAvailable = int.Parse(parameters[timeAvailable_Name]);
            Cp = double.Parse(parameters[Cp_Name]);            
        }


        public virtual ISearchInfo GetSearchInfo()
        {
            Result result = new Result();
            result.SearchResutlts.Add(Name, Name);
            result.SearchResutlts.Add(NumberOfGeneratedNodes_Name, NumberOfGeneratedNodes.ToString());

            result.SearchParameters.Add(timeAvailable_Name, timeAvailable.ToString());
            result.SearchParameters.Add(Cp_Name, Cp.ToString());
            result.SearchParameters.Add(maxDepth_Name, maxDepth.ToString());
            return result;
        }





        public IPolicy getPolicy()
        {
            if (FoundGoal == null)
                return null;
            return new Policy(this);
        }


        public bool InitStates(INode start, INode goal)
        {
            Start = new UCTNode(start);
            Start.Q = double.MaxValue;
            Start.N = 0;

            Goal = new UCTNode(goal);
            Goal.Q = 100;
            Goal.N = 0;
            return true;
        }
        

        public String Name
        {
            get
            {
                return "UCT";
            }
        }




        public void Execute()
        {
            stopwatch.Restart();            

            UCTNode lastNode;

            double reward = 0;

            while (stopwatch.Elapsed.TotalMilliseconds < timeAvailable && !stopped)
            {
                lastNode = TreePolicy(Start);
                reward = DefaultPolicy(lastNode);
                Backup(lastNode, reward);
            }
            return;

        }


        public void Stop()
        {
            stopped = true;
        }




        public UCT(ISuccGenerator<UCTNode> succGen, double cp, int maxDepth)
        {
            SuccGen = succGen;
            Cp = cp;
            this.maxDepth = maxDepth;
            Env = succGen.getEnvironment();
        }

		

		private UCTNode TreePolicy(UCTNode node)
		{

            UCTNode Node = node;
            IEnumerable<UCTNode> children;
            while (!isTerminal(Node.State) && stopwatch.Elapsed.TotalMilliseconds < timeAvailable && !stopped)
            {                
                if (childrenDic.TryGetValue(Node, out children))
                {
                    foreach(UCTNode child in children)
                    {
                        if (!isTried(child))
                        {                           
                            return child;
                        }
                    }

                    Node = BestChild(children);
                    Node.N += 1;
                }
                else
                    Expand(Node);
            }

            if (isTerminal(Node))
                FoundGoal = Node;

            return Node;

        }


		private UCTNode BestChild(UCTNode node) 
		{

			IEnumerable<UCTNode> children;

			if (!childrenDic.TryGetValue(node, out children) )
			{
				throw new NoChildrenException("no children");
			}

			if (children.Count() == 0)
				return node;

			UCTNode bestChild = children.ElementAt(0);
			double UCTFuncValueOfBestChild = UCTFunc(bestChild);
			

			foreach (UCTNode child in children)
			{
				double UCTFuncValue = UCTFunc(child);
				if (UCTFuncValueOfBestChild > UCTFuncValue)
				{
					UCTFuncValueOfBestChild = UCTFuncValue;
					bestChild = child;
				}
			}           

            return bestChild;

		}


        private UCTNode BestChild(IEnumerable<UCTNode> children)
        {           

            if (children.Count() == 0)
                return null;

            UCTNode bestChild = children.ElementAt(0);
            double UCTFuncValueOfBestChild = UCTFunc(bestChild);


            foreach (UCTNode child in children)
            {
                double UCTFuncValue = UCTFunc(child);
                if (UCTFuncValueOfBestChild < UCTFuncValue)
                {
                    UCTFuncValueOfBestChild = UCTFuncValue;
                    bestChild = child;
                }
            }
            

            return bestChild;

        }
        




        private double UCTFunc(UCTNode node)
		{
            double Q = Math.Abs(node.Q);
            UCTNode parent = (UCTNode)node.Parent;


            return Q / node.N + Cp * Math.Sqrt(2 * Math.Log(parent.N) / node.N);
		}


		private void Expand(UCTNode node)
		{
            if (childrenDic.ContainsKey(node))
                return;

			IEnumerable<UCTNode> children;          

			children = SuccGen.Generate(node);
			childrenDic.Add(node, children);

            NumberOfGeneratedNodes += children.Count();


        }
        

		private void Backup(UCTNode node, double delta)
		{

			while (node != null)
			{
				node.Q = node.Q + delta;
				//node.N = node.N + 1;
				node = (UCTNode)node.Parent;				
			}            
        }
		private double DefaultPolicy(UCTNode node)
		{
            if (isTerminal(node.State))
                return node.Q;
            node.Q = 0;
            node.N = 1;


            Random rand = new Random();           
            int depth = 0;
            double sumQ = 0;
            IOutcome outcome;
            IState currentState = node.State;
            IOperator usedOp = null;
            while (depth < maxDepth && stopwatch.Elapsed.TotalMilliseconds < timeAvailable && !isTerminal(currentState) && !stopped)
            {
                IEnumerable<IOperator> operators = Env.ApplicableOperators(currentState);
                int opCount = operators.Count();
                if (opCount == 0)
                    return sumQ;
                usedOp = operators.ElementAt(rand.Next(opCount));
                outcome = Env.act(currentState, usedOp);
                sumQ += outcome.QValue;
                depth += 1;
                currentState = outcome.State;
            }

            if (isTerminal(currentState))
                return Goal.Q;



            return sumQ;
        }


        private bool isTerminal(IState state)
        {
            return Goal.State.Equals(state);
        }

        private bool isTerminal(UCTNode node)
        {
            return isTerminal(node.State);
        }


        private bool isTried(UCTNode node)
        {
            return node.Q != double.MaxValue;
        }


        private class Policy :IPolicy
        {
            UCT uct;

            public Policy(UCT uct)
            {
                this.uct = uct;
            }


            public IOperator action(IState s)
            {
                IEnumerable<UCTNode> children;

                UCTNode node = new UCTNode();                
                node.State = s;
                node.Parent = null;

                if (!uct.childrenDic.TryGetValue(node, out children))

                    throw new NoChildrenException();


                UCTNode bestChild = children.ElementAt(0);
                double maxQ = bestChild.Q/bestChild.N;


                foreach (UCTNode child in children)
                {                    
                    if (maxQ < child.Q / child.N)
                    {
                        maxQ = child.Q / child.N;
                        bestChild = child;
                    }
                }
                return bestChild.UsedOperator;
            }


            public double actionProb(IState s, IOperator a)
            {
                throw new NotImplementedException();
            }


            public bool definedFor(IState s)
            {
                UCTNode node = new UCTNode();               
                node.State = s;
                node.Parent = null;
                return uct.childrenDic.ContainsKey(node);               

            }


            public bool isGoal(IState s)
            {                
                return uct.Goal.State.Equals(s);
            }
        }
         










    }


}
