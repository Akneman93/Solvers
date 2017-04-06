using System;
using System.Collections.Generic;

namespace Solvers.Interfaces
{
    public interface ISolver
	{
		IPolicy getPolicy();

        bool InitStates(INode Start, INode Goal);

        void Execute();

        String Name { get; }

        ISearchInfo GetSearchInfo();

        void Stop();

        void setParameters(Dictionary<string, string> parameters);   

    }
}
