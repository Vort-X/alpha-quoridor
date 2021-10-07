using Queridor.Model;
using Queridor.ServicesAbstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quoridor.Model.Turns
{
    class PlaceWallTurn : Turn
    {
        protected bool isHorizontal;

        public PlaceWallTurn(bool isFirstPlayer, int x, int y, bool isHorizontal) : base(isFirstPlayer, x, y)
        {
            this.isHorizontal = isHorizontal;
        }

        internal override void Execute(Board board, Pawn player, Pawn enemy, ITurnCheckService turnCheckService)
        {
            Corner corner = null;
            if (!false)
            {
                throw new Exception("cannot find corner");
            }
            turnCheckService.MakeTurnService.PlaceWall(corner, isHorizontal);
            player.AvailableWalls -= 1;
        }
    }
}
