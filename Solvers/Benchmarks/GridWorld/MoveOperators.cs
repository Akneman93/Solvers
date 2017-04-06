using Solvers.Algorithms;
using Solvers.Interfaces;

namespace Solvers.GridWorldBench
{
    public static class MoveOperators
    {

		public static IOperator Up = new DefaultOperator("Up");
		public static IOperator Down = new DefaultOperator("Down");
		public static IOperator Left = new DefaultOperator("Left");
		public static IOperator Right = new DefaultOperator("Right");



        public static IState MoveUp(IState currentState, int blockSize)
        {
			GridWorldState state = (GridWorldState) currentState;
			GridWorldState new_state = new GridWorldState(state.X, state.Y - blockSize);
            return new_state;
        }
        public static IState MoveDown(IState currentState, int blockSize)
        {
			GridWorldState state = (GridWorldState)currentState;
			GridWorldState new_state = new GridWorldState(state.X, state.Y + blockSize);
			return new_state;
        }
        public static IState MoveLeft(IState currentState, int blockSize)
        {
			GridWorldState state = (GridWorldState)currentState;
			GridWorldState new_state = new GridWorldState(state.X - blockSize, state.Y);
            return new_state;
        }
        public static IState MoveRight(IState currentState, int blockSize)
        {
			GridWorldState state = (GridWorldState)currentState;
			GridWorldState new_state = new GridWorldState(state.X + blockSize, state.Y);
            return new_state;
        }
    }
}
