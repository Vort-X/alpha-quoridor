using Queridor.Model;
using Queridor.ServicesAbstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quoridor.Model.Turns
{
    class MakeMoveTurn : Turn
    {
        public MakeMoveTurn(Pawn player, int x, int y) : base(player, x, y)
        {
        }

        internal override void Execute(Board board, Pawn player, Pawn enemy, ITurnCheckService turnCheckService)
        {
            Cell finishCell = board.Cells.First(c => c.X == x && c.Y == y);
            if (!turnCheckService.CanMakeTurnCheck(finishCell, enemy, player, board.Cells))
            {
                throw new Exception("Cannot make move");
            }
            turnCheckService.MakeTurnService.MakeTurn(player, finishCell);
        }
    }
}
