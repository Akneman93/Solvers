using Solvers.Interfaces;
using System;
using System.Linq;
namespace Solvers.NPuzzleBench
{
    public class NPuzzleState:IState
	{
		private byte _emptyTileIndex;

		public byte[] Tiles { get; set; }

		public byte EmptyTileIndex
		{
			get
			{
				if (_emptyTileIndex == byte.MaxValue)
					_emptyTileIndex = GetEmptyTilePosition(this);

				return _emptyTileIndex;
			}
		}

		private static byte GetEmptyTilePosition(NPuzzleState node)
		{
            byte emptyTilePos = byte.MaxValue;

			for (byte i = 0; i < node.Tiles.Length; i++)
			{
				if (node.Tiles[i] == 0)
				{
					emptyTilePos = i;
					break;
				}
			}

			return emptyTilePos;
		}




		public NPuzzleState()
		{
			_emptyTileIndex = byte.MaxValue;
		}

		public NPuzzleState(byte[] tiles)
		{
			Tiles = tiles;
			_emptyTileIndex = byte.MaxValue;
		}


		override public bool Equals(Object obj)
		{
			var gridState = obj as NPuzzleState;
			if (obj == null)
				return false;
			return gridState.Tiles.SequenceEqual(this.Tiles);
		}



		override public int GetHashCode()
		{
			int result = 0;
			int shift = 0;
			for (int i = 0; i < Tiles.Length; i++)
			{
				shift = (shift + 11) % 21;
				result ^= (Tiles[i] + 1024) << shift;
			}
			return result;
		}




	}
}
