using Quoridor.Model.Turns;
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
        event Action InvalidTurn;

        void GameLoop();
    }

    public enum GameState
    {
        Waiting, 
        FinishedTurn,
    }
}
