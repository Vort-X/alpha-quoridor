using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quoridor.Model.Abstract
{
    public interface IGameManager
    {
        IBoardPresenter BoardPresenter { get; }
        GameState State { get; set; }

        event Action BoardUpdated;

        internal void MakeTurn(IPlayer sender, object turn);
    }

    public enum GameState
    {
        Waiting, 
        FinishedTurn,
    }
}
