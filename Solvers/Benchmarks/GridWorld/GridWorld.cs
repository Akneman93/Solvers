using Solvers.Algorithms;
using Solvers.Algorithms.Exceptions;
using Solvers.Interfaces;
using System.Collections.Generic;


namespace Solvers.GridWorldBench
{
    public class GridWorld:IEnvironment
	{
		private const int cost = -1; 
		private GridWorldMap GWMap;
		private int blockSize;
		public int BlockSize
		{
			get
			{
				return blockSize;
			}
			set
			{
				blockSize = value;
			}
		}
		public GridWorldMap GridWorldMap
		{
			get
			{
				return GWMap;
			}
			set
			{
				GWMap = value;
			}
		}

		public GridWorld(GridWorldMap gwm, int BlockSize)
		{
			GWMap = gwm;
			blockSize = BlockSize;
		}




		public IOutcome act(IState currentState, IOperator op)
		{
			GridWorldState state = (GridWorldState)currentState;
			IState succ;

			if (op.Equals(MoveOperators.Up))
			{

				if (!GWMap.isUpFree(state.X, state.Y, blockSize))
					throw new NotApplicableOperatorException("not applicable operator");

				succ = MoveOperators.MoveUp(currentState, blockSize);
				return new Outcome(succ, cost);
			}



			if (op.Equals(MoveOperators.Down))
			{

				if (!GWMap.isDownFree(state.X, state.Y, blockSize))
					throw new NotApplicableOperatorException("not applicable operator");

				succ = MoveOperators.MoveDown(currentState, blockSize);
				return new Outcome(succ, cost);
			}


			if (op.Equals(MoveOperators.Left))
			{

				if (!GWMap.isLeftFree(state.X, state.Y, blockSize))
					throw new NotApplicableOperatorException("not applicable operator");

				succ = MoveOperators.MoveLeft(currentState, blockSize);
				return new Outcome(succ, cost);
			}


			if (op.Equals(MoveOperators.Right))
			{

				if (!GWMap.isRightFree(state.X, state.Y, blockSize))
					throw new NotApplicableOperatorException("not applicable operator");

				succ = MoveOperators.MoveRight(currentState, blockSize);
				return new Outcome(succ, cost);
			}


			throw new NotApplicableOperatorException("Unknown operator");		
		
		}


		public IEnumerable<IOperator> ApplicableOperators(IState currentState)
		{
			GridWorldState state = (GridWorldState)currentState;
			List<IOperator> applicableOperators = new List<IOperator>();
			if (GWMap.isDownFree(state.X, state.Y, blockSize))
				applicableOperators.Add(MoveOperators.Down);
			if (GWMap.isLeftFree(state.X, state.Y, blockSize))
				applicableOperators.Add(MoveOperators.Left);
			if (GWMap.isRightFree(state.X, state.Y, blockSize))
				applicableOperators.Add(MoveOperators.Right);
			if (GWMap.isUpFree(state.X, state.Y, blockSize))
				applicableOperators.Add(MoveOperators.Up);

			return applicableOperators;
		
		}
	}
}
