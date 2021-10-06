using Quoridor.Model.Turns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quoridor.Model.TurnFactories
{
    public class MakeMoveTurnFactory
    {
        public Turn CreateTurn(bool isFirstPlayer, int x, int y)
        {
            return new MakeMoveTurn(isFirstPlayer, x, y);
        }
    }
}
