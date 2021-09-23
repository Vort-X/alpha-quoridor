using Quoridor.Model.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quoridor.Model.Abstract
{
    public interface IPlayer
    {
        event Action<IPlayer, object> TurnFinished;

        void NotifyTurn();
    }
}
