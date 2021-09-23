using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quoridor.Model.Abstract
{
    public interface IGame
    {
        IBoard Board { get; }
        IPlayer Player1 { get; }
        IPlayer Player2 { get; }
        GameState State { get; }

        void MakeTurn(IPlayer sender, object turn);
    }

    public enum GameState
    {
        Waiting, 
        FinishedTurn,
    }
}
