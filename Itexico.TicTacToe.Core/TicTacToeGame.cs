using System;
using System.Collections.Generic;

namespace Itexico.TicTacToe.Core
{
	public class TicTacToeGame
	{
		public Player CurrentPlayer { get; private set;}
		public Player Winner { get; set;}
		public Cell[][] Board { get; private set; }

        private Solver Solver = new Solver();

		public TicTacToeGame ()
		{
			Board = new Cell[3][];
			CurrentPlayer = Player.CROSS;
			Winner = Player.NOTHING;
		}

		public bool SomePlayerHasWin 
		{ 
			get
			{
				return Winner != Player.NOTHING; 
			}
		}

		public void Init(int screenWidth, int screenHeight, int cellSize)
		{
			var totalSize = cellSize * 3; //3 cells
			var adjustHorizontal = CalculAdjust(screenWidth, totalSize); // We want it centered, whatever the resolution is
			var adjustVertical = CalculAdjust(screenHeight, totalSize);

			for (int i = 0; i < Board.Length; i++) 
			{
				Board[i] = new Cell[3];
				for (int j = 0; j < Board[i].Length; j++) 
				{
					Board[i][j] = new Cell(cellSize, j, i, adjustHorizontal, adjustVertical);
				}
			}
		}

		public void CellClicked(Cell cell)
		{
			if (Winner == Player.NOTHING) 
			{
				if (Board [cell.Y] [cell.X].Move == Player.NOTHING) 
				{
					Board [cell.Y] [cell.X].Move = CurrentPlayer;
					CurrentPlayer = (CurrentPlayer == Player.CROSS) ? Player.CIRCLE : Player.CROSS;
				}
                Solver.Solve(cell.X, cell.Y, 3, this);
			}
		}

		private int CalculAdjust(int screenTotal, int cellsTotal)
		{
			if (cellsTotal >= screenTotal)
				return 0;
			return (screenTotal - cellsTotal) / 2;
		}

	}
}

