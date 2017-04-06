using Solvers.Algorithms;
using Solvers.Algorithms.Astar;
using Solvers.Interfaces;
using System.Collections.Generic;

namespace Solvers.NPuzzleBench
{
    public class NPuzzleManDistCal : IHCalculator
    {
        private const int Costfactor = 1;

        private int N;
        private int tilesNumber;

        public NPuzzleManDistCal(int Size)
        {
            N = Size;
            tilesNumber = N * N;
        }

       
        public int Calculate(ANode node, ANode goal)
        {
            int result = 0;
             var currentNode = node as ANode;
              var goalNode = goal as ANode;

             List<double> list = new List<double>();
             int currentNumber;
             int currentIndex;



              for (int i = 0; i < tilesNumber; i++)
              {

                  NPuzzleState state = (NPuzzleState)currentNode.State;
                   currentNumber = state.Tiles[i];
                   if (currentNumber == 0)
                       continue;
                   currentIndex = FindTileCurrentIndex(currentNumber, goalNode);

                   result += GetDistanceToGoalTileForIndex(i, currentIndex);                 
              }              

            

           

            return result;
            
        }

       

        private int GetDistanceToGoalTileForIndex(int i, int currentIndex)
        {

            if (i == currentIndex)
                return 0;

            int a = i;
            int b = currentIndex;
            
                        
            int positive1 = (a / N - b / N);
            positive1 = positive1 > 0 ? positive1 : -positive1;
            int positive2 = (a % N - b % N);
            positive2 = positive2 > 0 ? positive2 : -positive2;
            return positive1 + positive2;            
        }

        private int FindTileCurrentIndex(int goalNumber, ANode current)
        {
			NPuzzleState state = (NPuzzleState)current.State;

            for (int j = 0; j < tilesNumber; j++)
            {
                if (state.Tiles[j] == goalNumber)
                {
                    return j;
                }
            }

            return -1;
        }
    }
}
