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

        public void MakeTurn(bool isFirstPlayer, Cell cell)
        {
            if (isFirstPlayer) board.firstPlayer.Cell = cell;
            else board.secondPlayer.Cell = cell;
        }

        public void PlaceWall(Corner corner, bool horizontal)
        {
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
        }
    }
}
