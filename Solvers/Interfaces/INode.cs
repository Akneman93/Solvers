
namespace Solvers.Interfaces
{
    /// <summary>
    /// interface for a node 
    /// </summary>
    public interface INode
    {      
        /// <summary>
        /// operator used in parent's state
        /// </summary>
        IOperator UsedOperator{get; set;}

        
        INode Parent { get; set; }
       

		IState State
		{
			get;
			set;
		}   	
	}
}
