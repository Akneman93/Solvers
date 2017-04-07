using System;
using System.Collections.Generic;

namespace Solvers.Interfaces
{
    /// <summary>
    /// Interface for a class that calculates the policy
    /// </summary>

    public interface ISolver
	{
		IPolicy getPolicy();

        bool InitStates(INode Start, INode Goal);


        /// <summary>
        /// Starts calcualtion of the policy
        /// </summary>
        void Execute();

        String Name { get; }

        ISearchInfo GetSearchInfo();

        void Stop();

        void setParameters(Dictionary<string, string> parameters);   

    }
}
