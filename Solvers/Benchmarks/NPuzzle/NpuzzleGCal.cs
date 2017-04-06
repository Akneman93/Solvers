using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Planning.Interfaces;
using Planning.Algorithms;

namespace Planning.NPuzzleBench
{    
    public class NPuzzleGCal : IGCalculator
    {
        private const int CostFactor = 1; 
 
        #region IGValueCalculator Members

		public int Calculate(ANode Child, IOperator op)
        {
            return Child.Parent.G + CostFactor;
        }
 
        #endregion
    }
}
