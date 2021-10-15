using Queridor.Model;
using Queridor.ServicesAbstract;
using Quoridor.Model.Abstract;
using Quoridor.Model.TurnFactories;
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
        private readonly Pawn pawn;
        private readonly Pawn enemy;
        private readonly ITurnCheckService turnCheckService;

        public RandomBotAlgorithm(Board board, Pawn pawn, Pawn enemy, ITurnCheckService turnCheckService)
        {
            this.board = board;
            this.pawn = pawn;
            this.enemy = enemy;
            this.turnCheckService = turnCheckService;
        }

        public Turn GetTurn()
        {
            var random = new Random();
            if (random.NextDouble() < MOVE_RATIO || pawn.AvailableWalls == 0)
            {
                var ns = turnCheckService.FindAvaliableCells(pawn, enemy, board.Cells);
                var r = random.Next(0, ns.Count);
                return MakeMoveTurnFactory.CreateTurn(pawn, ns[r].X, ns[r].Y);
            }
            else
            {
                bool horizontal = true;
                if (random.NextDouble() < 0.5)
                {
                    horizontal = false;
                }
                var cs = board.Corners.Where(c => turnCheckService.CanPlaceWallCheck(board.Cells, c, horizontal, pawn, enemy)).ToList();
                var r = random.Next(0, cs.Count);
                return PlaceWallTurnFactory.CreateTurn(pawn, cs[r].X, cs[r].Y, horizontal);
            }
        }
    }
}
