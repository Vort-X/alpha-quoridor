using Queridor.Model;
using Queridor.ServicesAbstract;
using Quoridor.Model.Abstract;
using Quoridor.Model.TurnFactories;
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
        private readonly Board board;
        private readonly IBoardPresenter boardPresenter;
        private readonly ITurnCheckService turnCheckService;

        public EasyBotPlayer(Pawn pawn, Board board, IBoardPresenter boardPresenter, ITurnCheckService turnCheckService) : base(pawn)
        {
            this.board = board;
            this.boardPresenter = boardPresenter;
            this.turnCheckService = turnCheckService;
        }

        protected override Turn GetTurnFromAlgorythm()
        {
            var enemy = pawn == boardPresenter.Pawn1 ? boardPresenter.Pawn2 : boardPresenter.Pawn1;
            var random = new Random();
            if (random.NextDouble() < 0.5)
            {
                var ns = turnCheckService.FindAvaliableNeighbours(pawn.Cell);
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
