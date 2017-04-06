namespace Solvers.Interfaces
{
    public interface IQProvider
	{
		double getQ(IState state, IOperator op);
	}
}