using Queridor.Model;
using Quoridor.Model.Turns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quoridor.Model.TurnFactories
{
    public static class MakeMoveTurnFactory
    {
        public static Turn CreateTurn(Pawn player, int x, int y)
        {
            return new MakeMoveTurn(player, x, y);
        }
    }
}
