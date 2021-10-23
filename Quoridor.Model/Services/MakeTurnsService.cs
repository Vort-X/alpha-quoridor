using Queridor.Model;
using Queridor.ServicesAbstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Queridor.Services
{
    public class MakeTurnsService : IMakeTurnService
    {
        private Board board;

        public MakeTurnsService(Board board)
        {
            this.board = board;
        }

        public void MakeTurn(bool isFirstPlayer, int x, int y)
        {
            var cell = board.Cells.Find(c => c.X == x && c.Y == y);
            if (isFirstPlayer) board.FirstPlayer.Cell = cell;
            else board.SecondPlayer.Cell = cell;
        }

        public void PlaceWall(bool isFirstPlayer, int x, int y, bool horizontal)
        {
            var corner = board.Corners.Find(c => c.X == x && c.Y == y);
            if (horizontal)
            {
                corner.HorizontalEdges.Key.IsBlocked = true;
                corner.HorizontalEdges.Value.IsBlocked = true;
            }
            else
            {
                corner.VerticalEdges.Key.IsBlocked = true;
                corner.VerticalEdges.Value.IsBlocked = true;
            }
            if (isFirstPlayer) 
                board.FirstPlayer.AvailableWalls--;
            else 
                board.SecondPlayer.AvailableWalls--;
        }
    }
}
