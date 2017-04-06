using Solvers.Interfaces;
using System;
using System.Collections.Generic;
using System.Windows.Forms.DataVisualization.Charting;

namespace Solvers.Algorithms
{

  [Serializable]
  public  class Result: ISearchInfo
    {
        private Dictionary<String, String> searchResutlts = new Dictionary<string, string>();
        private Dictionary<String, String> searchParameters = new Dictionary<string, string>();

        private List<Chart> chartList = new List<Chart>();

        public Dictionary<String, String> SearchResutlts
        {
            get
            {
                return searchResutlts;
            }
        }


        public Dictionary<String, String> SearchParameters
        {
            get
            {
                return searchParameters;
            }
        }


        public List<Chart> ChartList
        {
            get
            {
                return chartList;
            }
        }




    }
    
}
