﻿using Queridor.Model;
using Queridor.ServicesAbstract;
using Quoridor.Model.Abstract;
using Quoridor.Model.GameObjects;
using Quoridor.Model.Turns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quoridor.Model.GameManager
{
    class DefaultGameManager : IGameManager
    {
        private readonly Board board;
        private readonly IBoardPresenter boardPresenter;
        private readonly ITurnCheckService turnCheckService;
        private PlayerTurnStateMachine ptsm;

        public DefaultGameManager(Board board, IBoardPresenter boardPresenter, ITurnCheckService turnCheckService, IPlayer player1, IPlayer player2)
        {
            this.board = board;
            this.boardPresenter = boardPresenter;
            this.turnCheckService = turnCheckService;
            RegisterPlayers(player1, player2);

            State = GameState.Waiting;
        }

        public IBoardPresenter BoardPresenter => boardPresenter;
        public GameState State { get; set; }

        public event Action BoardUpdated;
        public event Action InvalidTurn;

        public void GameLoop()
        {
            if (State is GameState.Waiting) return;

            State = GameState.Waiting;
            
            ptsm.ActivePlayer.NotifyTurn();
        }

        private void MakeTurn(IPlayer sender, Turn turn)
        {
            if (ptsm is null)
            {
                throw new NullReferenceException("State machine is null. Call method \"DefaultGameManager.RegisterPlayers\" to create state machine.");
            }
            if (sender != ptsm.ActivePlayer)
            {
                //????
                return;
            }

            try
            {
                boardPresenter.MakeTurn(turn);
                Pawn enemy = turn.Player == boardPresenter.Pawn1 ? boardPresenter.Pawn2 : boardPresenter.Pawn1;

                //TODO: find better solution
                if (turn is PlaceWallTurn pwTurn)
                {
                    pwTurn.PlaceWall += (corner, isHorizontal) => boardPresenter.Walls.Add(new Wall() { Corner = corner, IsHorizontal = isHorizontal });
                }

                turn.Execute(board, turn.Player, enemy, turnCheckService);
            }
            catch (Exception e)
            {
                InvalidTurn?.Invoke();
            }
            finally
            {
                State = GameState.FinishedTurn;
            }
            ptsm.MoveNext();
            BoardUpdated?.Invoke();
        }

        private void RegisterPlayers(IPlayer player1, IPlayer player2)
        {
            ptsm = new PlayerTurnStateMachine(player1, player2);
            player1.TurnFinished += MakeTurn;
            player2.TurnFinished += MakeTurn;
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
