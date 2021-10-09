using Queridor.Model;
using Quoridor.Model.Turns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quoridor.Model.TurnFactories
{
    public static class PlaceWallTurnFactory
    {
        public static Turn CreateTurn(Pawn player, int x, int y, bool isHorizontal)
        {
            return new PlaceWallTurn(player, x, y, isHorizontal);
        }
    }
}
