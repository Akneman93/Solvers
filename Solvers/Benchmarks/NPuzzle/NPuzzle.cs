using Solvers.Algorithms;
using Solvers.Algorithms.Exceptions;
using Solvers.Interfaces;
using System.Collections.Generic;
namespace Solvers.NPuzzleBench
{
    public class NPuzzle:IEnvironment
	{

		private const int cost = -1;
		private int N;
		public NPuzzle(int Size)
		{
			N = Size;
		}



		public IOutcome act(IState currentState, IOperator op)
		{
			

			NPuzzleState state = (NPuzzleState)currentState;

			IState succ;

			if (op.Equals(MoveOperator.Up))
			{ 
				if (state.EmptyTileIndex > N - 1)
				{
					succ = MoveOperator.UpMove(state, N);
					return new Outcome(succ, cost);
				}

				else
					throw new NotApplicableOperatorException("not applicable operator");		
			}

			if (op.Equals(MoveOperator.Down))
			{
				if (state.EmptyTileIndex < N * (N - 1))
				{
					succ = MoveOperator.DownMove(state, N);
					return new Outcome(succ, cost);
				}

				else
					throw new NotApplicableOperatorException("not applicable operator");
			}

			if (op.Equals(MoveOperator.Left))
			{
				if (state.EmptyTileIndex % N > 0)
				{
					succ = MoveOperator.LeftMove(state, N);
					return new Outcome(succ, cost);
				}

				else
					throw new NotApplicableOperatorException("not applicable operator");
			}

			if (op.Equals(MoveOperator.Right))
			{
				if (state.EmptyTileIndex % N < N - 1)
				{
					succ = MoveOperator.RightMove(state, N);
					return new Outcome(succ, cost);
				}

				else
					throw new NotApplicableOperatorException("not applicable operator");
			}

			throw new NotApplicableOperatorException("Unknown operator");

		}

		public IEnumerable<IOperator> ApplicableOperators(IState currentState)
		{ 
			NPuzzleState state = (NPuzzleState)currentState;
			List<IOperator> applicableOperators = new List<IOperator>();
			if (state.EmptyTileIndex < N * (N - 1))
				applicableOperators.Add(MoveOperator.Down);
			if (state.EmptyTileIndex % N > 0)
				applicableOperators.Add(MoveOperator.Left);
			if (state.EmptyTileIndex % N < N - 1)
				applicableOperators.Add(MoveOperator.Right);
			if (state.EmptyTileIndex > N - 1)
				applicableOperators.Add(MoveOperator.Up);

			return applicableOperators;
		
		
		}

	}
}
