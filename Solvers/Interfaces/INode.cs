
namespace Solvers.Interfaces
{
    /// <summary>
    /// interface for a node 
    /// </summary>
    public interface INode
    {      
        /// <summary>
        /// Operator used in parent's state
        /// </summary>
        IOperator UsedOperator{get; set;}

        /// <summary>
        /// Parent of this node
        /// </summary>
        INode Parent { get; set; }
       
        /// <summary>
        /// State of the environment
        /// </summary>
		IState State
		{
			get;
			set;
		}   	
	}
}
