using Queridor.Model;
using Queridor.ServicesAbstract;
using Quoridor.Model.Abstract;
using Quoridor.Model.Turns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Quoridor.Model.BotAlgorithms
{
    class RandomBotAlgorithm : IBotAlgorithm
    {
        private const double MOVE_RATIO = 2d / 3;

        private readonly Board board;
        private readonly bool isFirstPlayer;
        private readonly ITurnCheckService turnCheckService;

        public RandomBotAlgorithm(Board board, bool isFirstPlayer, ITurnCheckService turnCheckService)
        {
            this.board = board;
            this.isFirstPlayer = isFirstPlayer;
            this.turnCheckService = turnCheckService;
        }

        public Turn GetTurn()
        {
            var random = new Random();
            if (random.NextDouble() < MOVE_RATIO || turnCheckService.GetAvaliableWallsCount(isFirstPlayer) == 0)
            {
                var ns = turnCheckService.FindAvaliableCells(isFirstPlayer);
                var r = random.Next(0, ns.Count);
                return new MakeMoveTurn(isFirstPlayer, ns[r].X, ns[r].Y);
            }
            else
            {
                bool horizontal = true;
                if (random.NextDouble() < 0.5)
                {
                    horizontal = false;
                }
                var cs = board.Corners.FindAll(c => turnCheckService.CanPlaceWallCheck(isFirstPlayer, c.X, c.Y, horizontal));
                var r = random.Next(0, cs.Count);
                return new PlaceWallTurn(isFirstPlayer, cs[r].X, cs[r].Y, horizontal);
            }
        }
    }
}
