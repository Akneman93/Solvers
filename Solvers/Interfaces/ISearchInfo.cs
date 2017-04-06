using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace Solvers.Interfaces
{
    public interface ISearchInfo
    {
        Dictionary<String, String> SearchResutlts
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
