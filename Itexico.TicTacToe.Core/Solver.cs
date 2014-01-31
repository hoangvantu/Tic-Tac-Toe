using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Itexico.TicTacToe.Core
{
    public class Solver
    {

        public void Solve(int x, int y, int n, TicTacToeGame game)
        {
            SolveInternal(Player.CROSS, 0, y, n, game);
            SolveInternal(Player.CROSS, 1, y, n, game);
            SolveInternal(Player.CROSS, 2, y, n, game);
			SolveInternal(Player.CROSS, x, 0, n, game);
			SolveInternal(Player.CROSS, x, 1, n, game);
			SolveInternal(Player.CROSS, x, 2, n, game);

            SolveInternal(Player.CIRCLE, 0, y, n, game);
            SolveInternal(Player.CIRCLE, 1, y, n, game);
            SolveInternal(Player.CIRCLE, 2, y, n, game);
			SolveInternal(Player.CIRCLE, x, 0, n, game);
			SolveInternal(Player.CIRCLE, x, 1, n, game);
			SolveInternal(Player.CIRCLE, x, 2, n, game);
        }

        private void SolveInternal(Player testAgainst, int x, int y, int n, TicTacToeGame game)
        {
            //check col
            for (int i = 0; i < n; i++)
            {
                if (game.Board[x][i].Move != testAgainst)
                    break;
                if (i == n - 1)
                    game.Winner = testAgainst;
            }

            //check row
            for (int i = 0; i < n; i++)
            {
                if (game.Board[i][y].Move != testAgainst)
                    break;
                if (i == n - 1)
                    game.Winner = testAgainst;
            }

            //check diag
            if (x == y)
            {
                for (int i = 0; i < n; i++)
                {
                    if (game.Board[i][i].Move != testAgainst)
                        break;
                    if (i == n - 1)
                        game.Winner = testAgainst;
                }
            }

            //check anti diag
            for (int i = 0; i < n; i++)
            {
                if (game.Board[i][(n - 1) - i].Move != testAgainst)
                    break;
                if (i == n - 1)
                    game.Winner = testAgainst;
            }
        }
    }
}
