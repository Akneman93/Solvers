using Solvers.Interfaces;
using System;
namespace Solvers.GridWorldBench
{
    public class GridWorldState : IState
	{
		private readonly int x;
		private readonly int y;

		public int X
		{
			get
			{
				return x;
			}
		}

		public int Y
		{
			get
			{
				return y;
			}
		}



		public GridWorldState(int X, int Y)
		{
			x = X;
			y = Y;
		}

		override public bool Equals(Object obj)
		{
			var gridState = obj as GridWorldState;
			if (obj == null)
				return false;
			return (gridState.x == x) && (gridState.y == y);
		}



		override public int GetHashCode()
		{
			return (x.ToString() + y.ToString()).GetHashCode();
		}




	}
}
