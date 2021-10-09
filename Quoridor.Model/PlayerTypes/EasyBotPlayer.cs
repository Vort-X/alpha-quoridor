﻿using Queridor.Model;
using Queridor.ServicesAbstract;
using Quoridor.Model.Abstract;
using Quoridor.Model.Turns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quoridor.Model.PlayerTypes
{
    class EasyBotPlayer : BotPlayer
    {
        private readonly IBoardPresenter boardPresenter;
        private readonly ITurnCheckService turnCheckService;

        public EasyBotPlayer(bool isFirstPlayer, IBoardPresenter boardPresenter, ITurnCheckService turnCheckService) : base(isFirstPlayer)
        {
            this.boardPresenter = boardPresenter;
            this.turnCheckService = turnCheckService;
        }

        protected override Turn GetTurnFromAlgorythm()
        {
            var (pawnPlayer, pawnEnemy) = isFirstPlayer ? (boardPresenter.Pawn1, boardPresenter.Pawn2) : (boardPresenter.Pawn2, boardPresenter.Pawn1);
            var random = new Random();
            if (random.NextDouble() < 0.5)
            {
                var ns = turnCheckService.FindAvaliableNeighbours(pawnPlayer.Cell);
                var r = random.Next(0, ns.Count);
                return null;
            }
            else
            {
                throw new NotImplementedException();
            }
        }
    }
}
