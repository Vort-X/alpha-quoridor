using Queridor.Model;
using Quoridor.Model.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quoridor.Model.PlayerTypes
{
    abstract class BotPlayer : IPlayer
    {
        protected readonly Board board;

        protected BotPlayer(Board board)
        {
            this.board = board;
        }

        public event Action<IPlayer, object> TurnFinished;

        public void NotifyTurn()
        {
            var turn = GetTurnFromAlgorythm();
            TurnFinished?.Invoke(this, turn);
        }

        protected abstract object GetTurnFromAlgorythm();
    }
}
