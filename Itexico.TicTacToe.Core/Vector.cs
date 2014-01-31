using System;

namespace Itexico.TicTacToe.Core
{
	public class Vector
	{
		public Vector ()
		{
		}

		public Vector(int x, int y)
		{
			X = x;
			Y = y;
		}

		public int X { get; set; }
		public int Y { get; set; }
	}
}

