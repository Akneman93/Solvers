namespace Solvers.Interfaces
{
    public interface IPolicy
	{

		IOperator action(IState s);


		double actionProb(IState s, IOperator a);


		bool definedFor(IState s);


        bool isGoal(IState s);

	}

}
