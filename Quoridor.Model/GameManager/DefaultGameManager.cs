using Quoridor.Model.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quoridor.Model.GameManager
{
    class DefaultGameManager : IGameManager
    {
        private readonly IBoardPresenter boardPresenter;
        private PlayerTurnStateMachine ptsm;

        public DefaultGameManager(IBoardPresenter boardPresenter)
        {
            this.boardPresenter = boardPresenter;
            State = GameState.Waiting;
        }

        public IBoardPresenter BoardPresenter => boardPresenter;
        public GameState State { get; set; }

        public event Action BoardUpdated;

        void IGameManager.MakeTurn(IPlayer sender, object turn)
        {
            if (sender != ptsm.ActivePlayer)
            {
                //????
                return;
            }
            boardPresenter.MakeTurn(turn);
            ptsm.MoveNext();
            BoardUpdated?.Invoke();
        }

        internal void RegisterPlayers(IPlayer player1, IPlayer player2)
        {
            ptsm = new PlayerTurnStateMachine(player1, player2);
        }

        private class PlayerTurnStateMachine
        {
            private IPlayer player1;
            private IPlayer player2;

            public PlayerTurnStateMachine(IPlayer player1, IPlayer player2)
            {
                this.player1 = player1;
                this.player2 = player2;
                ActivePlayer = player1;
            }

            public IPlayer ActivePlayer { get; private set; }

            public void MoveNext()
            {
                ActivePlayer = ActivePlayer != player1 ? player1 : player2;
            }
        }
    }
}
