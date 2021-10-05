using Queridor.Model;
using Queridor.ServicesAbstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Queridor.Services
{
    public class MakeTurnsService : IMakeTurnService
    {
        public void MakeTurn(Pawn player, Cell cell)
        {
            player.Cell = cell;
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
