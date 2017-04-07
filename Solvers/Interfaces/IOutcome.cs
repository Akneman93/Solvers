namespace Solvers.Interfaces
{

    /// <summary>
    /// Result of performing action in environment
    /// </summary>
    public interface IOutcome
	{
		IState State { get; set; }
		double Reward { get; set; }
	}
}
