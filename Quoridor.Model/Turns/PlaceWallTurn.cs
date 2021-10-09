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

        public PlaceWallTurn(Pawn player, int x, int y, bool isHorizontal) : base(player, x, y)
        {
            this.isHorizontal = isHorizontal;
        }

        public event Action<Corner, bool> PlaceWall; //TODO: find better solution

        internal override void Execute(Board board, Pawn player, Pawn enemy, ITurnCheckService turnCheckService)
        {
            if (player.AvailableWalls == 0)
            {
                throw new Exception("current player don't have any walls");
            }
            Corner corner = board.Corners.First(c => c.X == x && c.Y == y);
            if (turnCheckService.CanPlaceWallCheck(board.Cells, corner, isHorizontal, player, enemy))
            {
                throw new Exception("cannot place wall");
            }
            turnCheckService.MakeTurnService.PlaceWall(corner, isHorizontal);
            PlaceWall(corner, isHorizontal);
            player.AvailableWalls -= 1;
        }
    }
}
