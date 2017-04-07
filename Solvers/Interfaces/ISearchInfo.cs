using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace Solvers.Interfaces
{
    /// <summary>
    /// Contains parameters of an algorithm
    /// </summary>
    public interface ISearchInfo
    {
        Dictionary<String, String> SearchResults
        {
            get;          
        }

        Dictionary<String, String> SearchParameters
        {
            get;
        }

        List<Chart> ChartList
        {
            get;          
        }
    }
}
