using System;

namespace Itexico.TicTacToe.Core
{
	public class Cell
	{
		public Player Move { get; set; }

		public int Size { get; private set; }

		public int X { get; private set; }
		public int Y { get; private set; }

		public int PositionOnScreenX { get; set; }
		public int PositionOnScreenY { get; set; }

		public Cell (int size, int x, int y, 
			int adjustHorizontal, int adjustVertical)
		{
			Move = Player.NOTHING;
			Size = size;
			X = x;
			Y = y;
			PositionOnScreenX = x * size + adjustHorizontal;
			PositionOnScreenY = y * size + adjustVertical;
		}

		public Vector Center
		{
			get
			{
				return new Vector (PositionOnScreenX + (Size / 2), 
					PositionOnScreenY + (Size / 2));
			}
		}

		public bool Collide(int x, int y, int width, int height)
		{
			return (
				(Math.Abs (x - PositionOnScreenX) * 2 < Math.Abs (width + Size)) &&
				(Math.Abs (y - PositionOnScreenY) * 2 < Math.Abs (height + Size))
			);
		}
	}
}

