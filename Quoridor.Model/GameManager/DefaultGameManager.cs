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
        private readonly Board _board;
        private readonly IBoardPresenter _boardPresenter;
        private readonly ITurnCheckService _turnCheckService;
        
        private PlayerTurnStateMachine _ptsm;
        private bool isGameOver = false;

        public DefaultGameManager(Board board, IBoardPresenter boardPresenter, ITurnCheckService turnCheckService, IPlayer player1, IPlayer player2)
        {
            _board = board;
            _boardPresenter = boardPresenter;
            _turnCheckService = turnCheckService;
            RegisterPlayers(player1, player2);

            State = GameState.FinishedTurn;
        }

        public IBoardPresenter BoardPresenter => _boardPresenter;
        public ITurnCheckService TurnCheckService => _turnCheckService;
        private GameState State { get; set; }
        public event Action BoardUpdated;
        public event Action<IPlayer> PlayerWon;
        public event Action<string> InvalidTurn;

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

            try
            {
                Pawn enemy = turn.Player == _boardPresenter.Pawn1 ? _boardPresenter.Pawn2 : _boardPresenter.Pawn1;

                //TODO: find better solution
                if (turn is PlaceWallTurn pwTurn)
                {
                    pwTurn.PlaceWall += (corner, isHorizontal) => _boardPresenter.Walls.Add(new Wall() { Corner = corner, IsHorizontal = isHorizontal });
                }

                turn.Execute(_board, turn.Player, enemy, _turnCheckService);
                _ptsm.MoveNext();
                BoardUpdated?.Invoke();
                State = GameState.FinishedTurn;
                if (_turnCheckService.VictoryCheck(turn.Player)) 
                { 
                    PlayerWon?.Invoke(sender);
                    State = GameState.Waiting;
                    isGameOver = true;
                }
            }
            catch (Exception e)
            {
                InvalidTurn?.Invoke(e.Message);
            }
            finally
            {
                State = GameState.FinishedTurn;
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
