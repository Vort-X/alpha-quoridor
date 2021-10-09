using Queridor.Model;
using Queridor.ServicesAbstract;
using Quoridor.Model.Abstract;
using Quoridor.Model.Turns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quoridor.Model.PlayerTypes
{
    abstract class BotPlayer : IPlayer
    {
        protected Pawn pawn;

        protected BotPlayer(Pawn pawn)
        {
            this.pawn = pawn;
        }

        public event Action<IPlayer, Turn> TurnFinished;

        public void NotifyTurn()
        {
            var turn = GetTurnFromAlgorythm();
            TurnFinished?.Invoke(this, turn);
        }

        protected abstract Turn GetTurnFromAlgorythm();
    }
}
