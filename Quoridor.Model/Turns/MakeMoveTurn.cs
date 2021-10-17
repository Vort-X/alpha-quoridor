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
        public MakeMoveTurn(bool isFirstPlayer, int x, int y) : base(isFirstPlayer, x, y)
        {
        }

        public override string ErrorMessage
        {
            get
            {
                var player = isFirstPlayer ? 1 : 2;
                return $"Player {player} can't move to position {x}:{y}";
            }
        }

        internal override bool CanExecute(ITurnCheckService turnCheckService)
        {
            return turnCheckService.CanMakeTurnCheck(isFirstPlayer, x, y);
        }

        internal override void Execute(IMakeTurnService makeTurnService)
        {
            makeTurnService.MakeTurn(isFirstPlayer, x, y);
        }
    }
}
