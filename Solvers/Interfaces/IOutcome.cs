namespace Solvers.Interfaces
{
    public interface IOutcome
	{
		IState State { get; set; }
		double QValue { get; set; }
	}
}
