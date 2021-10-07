using Quoridor.Model.Abstract;
using Quoridor.Model.Turns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quoridor.Model.PlayerTypes
{
    class LocalPlayer : IPlayer
    {
        public event Action<IPlayer, Turn> TurnFinished;

        public void NotifyTurn()
        {
            throw new NotImplementedException();
            var turn = (Turn) new object();
            TurnFinished?.Invoke(this, turn);
        }
    }
}
