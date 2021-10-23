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
    public class BotPlayer : IPlayer
    {
        private readonly IBotAlgorithm algo;

        public BotPlayer(IBotAlgorithm algo)
        {
            this.algo = algo;
        }

        
        
        public event Action<IPlayer, Turn> TurnFinished;

        public void NotifyTurn()
        {
            var turn = algo.GetTurn();
            TurnFinished?.Invoke(this, turn);
        }

        public string UserFriendlyName => $"Random Bot";
    }
}
