using Queridor.Model;
using Queridor.ServicesAbstract;
using Quoridor.Model.Abstract;
using Quoridor.Model.GameObjects;
using Quoridor.Model.Turns;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quoridor.Model.GameManager
{
    class DefaultGameManager : IGameManager
    {
        private readonly IBoardPresenter _boardPresenter;
        private readonly IMakeTurnService _makeTurnService;
        private readonly ITurnCheckService _turnCheckService;
        
        private PlayerTurnStateMachine _ptsm;
        private bool isGameOver = false;

        public DefaultGameManager(IBoardPresenter boardPresenter, IMakeTurnService makeTurnService,
            ITurnCheckService turnCheckService, IPlayer player1, IPlayer player2)
        {
            _boardPresenter = boardPresenter;
            _makeTurnService = makeTurnService;
            _turnCheckService = turnCheckService;
            RegisterPlayers(player1, player2);

            State = GameState.FinishedTurn;
        }

        public IBoardPresenter BoardPresenter => _boardPresenter;
        private GameState State { get; set; }
        public event Action<Turn, IPlayer> BoardUpdated;
        public event Action<IPlayer> PlayerWon;
        public event Action<string> InvalidTurn;

        public List<Cell> FindAvailableCells(bool isFirstPlayer)
        {
            return _turnCheckService.FindAvaliableCells(isFirstPlayer);
        }

        public void GameLoop()
        {
            if (State is GameState.Waiting) return;

            if (isGameOver) return;
            
            State = GameState.Waiting;
            
            _ptsm.ActivePlayer.NotifyTurn();
        }

        private void MakeTurn(IPlayer sender, Turn turn)
        {
            if (_ptsm is null)
            {
                throw new NullReferenceException("State machine is null. Call method \"DefaultGameManager.RegisterPlayers\" to create state machine.");
            }
            if (sender != _ptsm.ActivePlayer)
            {
                //????
                return;
            }
            if (!turn.CanExecute(_turnCheckService))
            {
                InvalidTurn?.Invoke(turn.ErrorMessage);
                State = GameState.FinishedTurn;
                return;
            }
            if (turn is PlaceWallTurn pwTurn)
            {
                pwTurn.PlaceWall += _boardPresenter.PlaceWall;
            }
            turn.Execute(_makeTurnService);
            _ptsm.MoveNext();
            BoardUpdated?.Invoke(turn, sender);
            State = GameState.FinishedTurn;
            if (_turnCheckService.VictoryCheck(turn.IsFirstPlayer) || _turnCheckService.VictoryCheck(!turn.IsFirstPlayer))
            {
                PlayerWon?.Invoke(sender);
                isGameOver = true;
            }
        }

        private void RegisterPlayers(IPlayer player1, IPlayer player2)
        {
            _ptsm = new PlayerTurnStateMachine(player1, player2);
            player1.TurnFinished += MakeTurn;
            player2.TurnFinished += MakeTurn;
        }

        private class PlayerTurnStateMachine
        {
            private readonly IPlayer player1;
            private readonly IPlayer player2;

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
