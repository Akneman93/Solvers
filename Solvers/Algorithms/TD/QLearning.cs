using Solvers.Algorithms.Exceptions;
using Solvers.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms.DataVisualization.Charting;

namespace Solvers.Algorithms.TD
{
    class QLearning : ISolver
    {
        private Dictionary<IState, IEnumerable<qTurple>> QValues = new Dictionary<IState, IEnumerable<qTurple>>();

        private INode Goal;

        private INode Start;

        private double Alpha;
        private string Alpha_Name = "Alpha";

        private double Gamma;
        private string Gamma_Name = "Gamma";

        private double epsilon;
        private string epsilon_Name = "Epsilon";

        private double timeAvailable;
        private string timeAvailable_Name = "Available time";

        private int episodeCount;
        private string episodeCount_Name = "Number of episodes";

        private double EpisodeReward;

        private List<double> EpisodeRewards = new List<double>();

        private List<double> TimesList = new List<double>();
        private volatile bool stopped;




        public void setParameters(Dictionary<string, string> parameters)
        {
            Alpha = Double.Parse(parameters[Alpha_Name]);
            Gamma = Double.Parse(parameters[Gamma_Name]);
            epsilon = Double.Parse(parameters[epsilon_Name]);
            timeAvailable = int.Parse(parameters[timeAvailable_Name]);
            
        }



        public virtual ISearchInfo GetSearchInfo()
        {
            Result result = new Result();           
            result.SearchParameters.Add(Alpha_Name, Alpha.ToString());
            result.SearchParameters.Add(timeAvailable_Name, timeAvailable.ToString());
            result.SearchParameters.Add(Gamma_Name, Gamma.ToString());
            result.SearchParameters.Add(epsilon_Name, epsilon.ToString());

            result.SearchResults.Add(Name, Name);
            result.SearchResults.Add(episodeCount_Name, episodeCount.ToString());


            Chart chart = new Chart();
            chart.Series.Add(Name);            
            chart.ChartAreas.Add("area");
            chart.Name = "Reward per training episode";
            chart.ChartAreas[0].AxisY.Title = "Training episode reward";
            chart.ChartAreas[0].AxisX.Title = "Training episode";

            chart.ChartAreas[0].AxisX.Minimum = 0;
            chart.Series[Name].Points.AddXY(0, 0);


            for (int i = 0; i < episodeCount; i++)
            {
                chart.Series[Name].Points.AddXY(i+1, EpisodeRewards[i]);
            }

            result.ChartList.Add(chart);
            return result;
        }




        private IEnvironment Env;

       


        public readonly Stopwatch stopwatch = new Stopwatch();

        private IPolicy trainingPolicy = null;

        private bool goalFound = false;


        public string Name {

            get { return "Q-learning"; }

        }


        public QLearning(IEnvironment Env, double Alpha, double Gamma, double epsilon)
        {
            this.Env = Env;
            this.Alpha = Alpha;
            this.Gamma = Gamma;
            trainingPolicy = new EpsilonGreedyPolicy(this, epsilon);
            this.epsilon = epsilon;
            timeAvailable = 5000;

        }





        private void AddNewState(IState state)
        {
            List<qTurple> list = new List<qTurple>();
            foreach(IOperator op in Env.ApplicableOperators(state))
            {
               qTurple turple = new qTurple();
               turple.Q = DefaultQ(state, op);
               turple.Op = op;
               list.Add(turple);
            }
            QValues.Add(state, list);
        }

        private qTurple getTurple(IState s, IOperator op)
        {
            IEnumerable<qTurple> list = null;

            if (QValues.TryGetValue(s, out list))
            {
                return list.First(n => n.Op.Equals(op));
            }

            else
            {
                AddNewState(s);
                return getTurple(s, op);
            }            
        }


        private qTurple GetBestTurple(IState s)
        {

            IEnumerable<qTurple> list = null;
            if (QValues.TryGetValue(s, out list))
            {
                if (list.Count() == 0)
                    throw new NoOperatorFoundException();
                qTurple bestTurple = list.ElementAt(0);
                foreach (qTurple turple in list)
                {
                    if (turple.Q > bestTurple.Q)
                        bestTurple = turple;
                }

                return bestTurple;
            }
            else
            {
                AddNewState(s);
                return GetBestTurple(s);
            }

        }


        private IOperator GetBestOp(IState s)
        {
            return GetBestTurple(s).Op;
        }









        private void Train()
        {
            episodeCount += 1;

            EpisodeReward = 0;

            IState currentState = Start.State;

            while (!currentState.Equals(Goal.State) && stopwatch.Elapsed.TotalMilliseconds < timeAvailable && !stopped)
            {
                IOperator op = trainingPolicy.action(currentState);

                IOutcome outcome = Env.act(currentState, op);

                EpisodeReward += outcome.QValue;

                qTurple turple = getTurple(currentState, op);

                currentState = outcome.State;

                if (!goalFound && currentState.Equals(Goal.State))
                    turple.Q += 100; 

                turple.Q = turple.Q + Alpha * (outcome.QValue + Gamma * GetBestTurple(outcome.State).Q - turple.Q);

                
            }

            EpisodeRewards.Add(EpisodeReward);

            TimesList.Add(stopwatch.Elapsed.TotalMilliseconds);

            if (currentState.Equals(Goal.State))
                goalFound = true;
                
        }


       




        public void Execute()
        {
            stopwatch.Restart();            
            while (stopwatch.Elapsed.TotalMilliseconds < timeAvailable && !stopped)
                  Train();
        }

        public void Stop()
        {
            stopped = true;
        }





        public IPolicy getPolicy()
        {
            if (!goalFound)
                return null;
            return new Policy(this);
        }

        public bool InitStates(INode start, INode goal)
        {
            Start = start;
            Goal = goal;
            goalFound = false;

            return true;
        }

        private class EpsilonGreedyPolicy : IPolicy
        {
            QLearning qLearning = null;

            Random rand = new Random();

            double epsilon;

            public EpsilonGreedyPolicy(QLearning qLearning, double epsilon)
            {
                this.qLearning = qLearning;
                this.epsilon = epsilon;
            }

            


            public IOperator action(IState s)
            {                             

                if (rand.NextDouble() < epsilon)
                {
                    int size = qLearning.Env.ApplicableOperators(s).Count();
                    return qLearning.Env.ApplicableOperators(s).ElementAt(rand.Next(size));
                }
                else
                {
                    return qLearning.GetBestOp(s);
                }


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
                throw new NotImplementedException();
            }
        }


        private class qTurple
        {
            public double Q { get; set; }
            public IOperator Op { get; set; }
        }


        private double DefaultQ(IState state, IOperator op)
        {
            return 0;
        }

        private class Policy : IPolicy
        {
            QLearning qLearning;

            public Policy(QLearning qLearning)
            {
                this.qLearning = qLearning;
            }


            public IOperator action(IState s)
            {
                return qLearning.GetBestOp(s);
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
                return qLearning.Goal.State.Equals(s);
            }
        }




    }
}
