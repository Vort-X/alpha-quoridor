using Quoridor.Model.Turns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Queridor.ServicesAbstract;
using Queridor.Model;

namespace Quoridor.Model.Abstract
{
    public interface IGameManager
    {
        IBoardPresenter BoardPresenter { get; }
        event Action BoardUpdated;
        event Action<string> InvalidTurn;
        public event Action<IPlayer> PlayerWon;

        void GameLoop();
        List<Cell> FindAvaliableCells(bool isFirstPlayer); //TODO: find better solution
    }

    public enum GameState
    {
        Waiting, 
        FinishedTurn,
    }
}
