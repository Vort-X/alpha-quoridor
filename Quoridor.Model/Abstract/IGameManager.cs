using Quoridor.Model.Turns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Queridor.ServicesAbstract;

namespace Quoridor.Model.Abstract
{
    public interface IGameManager
    {
        IBoardPresenter BoardPresenter { get; }
        ITurnCheckService TurnCheckService { get; }
        event Action BoardUpdated;
        event Action<string> InvalidTurn;

        void GameLoop();
    }

    public enum GameState
    {
        Waiting, 
        FinishedTurn,
    }
}
