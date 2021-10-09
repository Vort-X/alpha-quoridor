using Quoridor.Model.Turns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quoridor.Model.TurnFactories
{
    public class PlaceWallTurnFactory
    {
        public static Turn CreateTurn(bool isFirstPlayer, int x, int y, bool isHorizontal)
        {
            return new PlaceWallTurn(isFirstPlayer, x, y, isHorizontal);
        }
    }
}
