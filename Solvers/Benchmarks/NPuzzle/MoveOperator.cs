using Solvers.Algorithms;
using Solvers.Interfaces;

namespace Solvers.NPuzzleBench
{
    public static class MoveOperator
    {
		public static IOperator Up = new DefaultOperator("Up");
		public static IOperator Down = new DefaultOperator("Down");
		public static IOperator Left = new DefaultOperator("Left");
		public static IOperator Right = new DefaultOperator("Right");



      public static IState LeftMove(IState currentState, int N)
        {
			NPuzzleState state = (NPuzzleState)currentState;
            int EmptyTileIndex = state.EmptyTileIndex;
            var newTiles = state.Tiles.Clone() as byte[];
            if (newTiles == null) return null;
            newTiles[EmptyTileIndex] = newTiles[EmptyTileIndex - 1];
            newTiles[EmptyTileIndex - 1] = 0;
			return new NPuzzleState(newTiles);
        }
      public static IState DownMove(IState currentState, int N)
       {
			NPuzzleState state = (NPuzzleState)currentState;
           int EmptyTileIndex = state.EmptyTileIndex;
           var newTiles = state.Tiles.Clone() as byte[];
           if (newTiles == null) return null;
           newTiles[EmptyTileIndex] = newTiles[EmptyTileIndex + N];
           newTiles[EmptyTileIndex + N] = 0;
			return new NPuzzleState(newTiles);

       }

      public static IState UpMove(IState currentState, int N)
       {
			NPuzzleState state = (NPuzzleState)currentState;
           int EmptyTileIndex = state.EmptyTileIndex;
           var newTiles = state.Tiles.Clone() as byte[];
           if (newTiles == null) return null;
           newTiles[EmptyTileIndex] = newTiles[EmptyTileIndex - N];
           newTiles[EmptyTileIndex - N] = 0;
			return new NPuzzleState(newTiles);

       }

      public static IState RightMove(IState currentState, int N)
       {
			NPuzzleState state = (NPuzzleState)currentState;
           int EmptyTileIndex = state.EmptyTileIndex;
           var newTiles = state.Tiles.Clone() as byte[];
           if (newTiles == null) return null;
           newTiles[EmptyTileIndex] = newTiles[EmptyTileIndex + 1];
           newTiles[EmptyTileIndex + 1] = 0;
			return new NPuzzleState(newTiles);

       }

       

    }
}
