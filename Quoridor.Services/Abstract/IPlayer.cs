using Quoridor.Model.GameObjects;
using Quoridor.Model.Turns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quoridor.Model.Abstract
{
    public interface IPlayer
    {
        event Action<IPlayer, Turn> TurnFinished;

        void NotifyTurn();
        string UserFriendlyName { get; }
    }
}
