using Solvers.Algorithms.Exceptions;
using Solvers.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms.DataVisualization.Charting;

namespace Solvers.Algorithms.Astar
{


    /// <summary>
    /// abstract class for A*-based algorithms
    /// </summary>
    public abstract class abstrA : ISolver
    {


        protected string name = "abstrA";
        protected ISuccGenerator<ANode> succGen;
        protected IHCalculator<ANode> hCalc;

        protected NodeTable<ANode> openList;
        protected NodeTable<ANode> closedList;

        protected ANode foundGoalNode;
        protected ANode startNode;
        protected ANode goalNode;


        protected int expansions;
        protected string expansions_Name = "Expansions";


        protected double timeAvaliable;
        protected string timeAvaliable_Name = "Available time(ms)";

        protected volatile bool stopped = false;



        protected bool isOptimalFound;
        
		protected long nodesGenerated;
        protected string nodesGenerated_Name = "Number of generated nodes";


        /// <summary>
        /// contains pairs (time, r) where 
        /// r is cummulative reward of found solution, 
        /// time - time when solution was found
        /// </summary>
        public Dictionary<double, double> rewards;        

        public readonly Stopwatch stopwatch = new Stopwatch();








		public abstrA()
		{
			expansions = 0;
			foundGoalNode = null;
			startNode = null;
			goalNode = null;
            timeAvaliable = 5000;
            rewards = new Dictionary<double, double>();
            openList = new NodeTable<ANode>(Comparer);
            closedList = new NodeTable<ANode>(Comparer);
        }

        
        public virtual void setParameters(Dictionary<string, string> parameters)
        {            
            timeAvaliable =  Double.Parse(parameters[timeAvaliable_Name]);
        }      





        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }           
        }        

       

        public virtual ANode FoundGoalNode
        {
            get
            {
                return foundGoalNode;
            }
        }
             
       public double TimeAvaliable
        {
            get
            {
                return timeAvaliable;
            }
            protected set
            {
                timeAvaliable = value;
            }
        }
       public bool IsOptimalFound
       {
           get
           {
               return isOptimalFound;
           }           
       }

       


        public abstract void Execute();       
       
        public abstract bool InitStates(INode Start, INode Goal);

		public abstract bool Reset();

        /// <summary>
        /// compare nodes by their priority 
        /// </summary>
        public abstract IComparer<ANode> Comparer { get; }


        public void Stop()
        {
            stopped = true;
        }        
     

        public int Expansions
        {
            get
            {
                return expansions;
            }
        }        


        public virtual long StoredCount
        {
            get
            {
                return openList.NumberOfElements + closedList.NumberOfElements;
            }
        }            

        
        /// <summary>
        /// returns nodes between start and goal node in solution
        /// </summary>       
        public List<INode> GetPath()
		{
			if (foundGoalNode == null)
				throw new NoSolutionException("Goal not found");

			List<INode> list = new List<INode>();

			INode node = foundGoalNode;
			list.Add(node);
			while (node.Parent != null)
			{
				node = node.Parent;
				list.Add(node);
			}
			list.Reverse();
			return list;
		}




             
       public List<IOperator> GetSolution()
       {
			if (foundGoalNode == null)
				throw new NoSolutionException("Goal not found");
			
			List<IOperator> list = new List<IOperator>();
           
           
           INode node = foundGoalNode;
			list.Add(node.UsedOperator);
           while (node.Parent != null)
           {
			   node = node.Parent;
               list.Add(node.UsedOperator);
              
           }
           list.Reverse();
           
           return list;
       }


       public double GetCost()
       {
            if (foundGoalNode != null)
                return foundGoalNode.G;
            else
                throw new NoSolutionException();
       }


		public IPolicy getPolicy()
		{
            if (foundGoalNode == null)
                return null;
            return new Policy(this);		
			
		}








        private class Policy : IPolicy
        {
            private Dictionary<IState, IOperator> solutionNodes = new Dictionary<IState, IOperator>();

            private INode foundGoalNode;


            public Policy(abstrA A)
            {
                if (A.foundGoalNode != null)
                {
                    foundGoalNode = A.foundGoalNode;

                    INode node = A.foundGoalNode;  


                    do
                    {
                        solutionNodes.Add(node.Parent.State, node.UsedOperator);
                        node = node.Parent;
                    } while (node.Parent != null);
                }

            }




            public IOperator action(IState s)
            {
                IOperator outRef;
                bool found = solutionNodes.TryGetValue(s, out outRef);
                if (found)
                    return outRef;
                else
                    return null;

            }


            public double actionProb(IState s, IOperator a)
            {
                throw new NotImplementedException();
            }


            public bool definedFor(IState s)
            {
                throw new NotImplementedException();
            }

            public bool isGoal(IState s)
            {
                return foundGoalNode.State.Equals(s);
            }



        }



        public virtual ISearchInfo GetSearchInfo()
        {
            Result result = new Result();
            result.SearchResults.Add("Algorithm", Name);
            result.SearchResults.Add(expansions_Name, expansions.ToString());            
            result.SearchResults.Add(nodesGenerated_Name, nodesGenerated.ToString());

            result.SearchParameters.Add(timeAvaliable_Name, timeAvaliable.ToString());


            
            Chart chart = new Chart();
            chart.Series.Add(Name);
            chart.Series[0].ChartType = SeriesChartType.Line;
            chart.Name = "Cummulative reward per found solution";

            chart.ChartAreas.Add("area");

            chart.ChartAreas[0].AxisY.Title = "Cummulative reward";
            chart.ChartAreas[0].AxisX.Title = "Time";

            chart.ChartAreas[0].AxisX.Minimum = 0;
            chart.Series[Name].Points.AddXY(0, 0);

            foreach(KeyValuePair<double,double> pair in rewards)
            {
                chart.Series[Name].Points.AddXY(pair.Key, pair.Value);
            }

            result.ChartList.Add(chart);

            return result;


        }    
       
        
    }
}
