using Queridor.BoardFabricAbstract;
using Queridor.Model;
using Queridor.ServicesAbstract;
using Quoridor.Model.Abstract;
using Quoridor.Model.GameObjects;
using Quoridor.Model.Turns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quoridor.Model.BoardPresenter
{
    class GraphBoardPresenter : IBoardPresenter
    {
        private Board board;
        private ITurnCheckService turnCheckService;

        public GraphBoardPresenter(Board board, Pawn pawn1, Pawn pawn2, ITurnCheckService turnCheckService)
        {
            this.board = board;
            Pawn1 = pawn1;
            Pawn2 = pawn2;
            this.turnCheckService = turnCheckService;
            Walls = new List<Wall>();
        }

        public Pawn Pawn1 { get; set; }
        public Pawn Pawn2 { get; set; }
        public List<Wall> Walls { get; set; }

        void IBoardPresenter.MakeTurn(Turn turn)
        {
            Pawn p1, p2;
            (p1, p2) = turn.IsFirstPlayer ? (Pawn1, Pawn2) : (Pawn2, Pawn1);

            //TODO: find better solution
            if (turn is PlaceWallTurn pwTurn)
            {
                pwTurn.PlaceWall += AddWall;
            }

            turn.Execute(board, p1, p2, turnCheckService);
        }

        private void AddWall(Corner corner, bool isHorizontal)
        {
            Walls.Add(new Wall() { Corner = corner, IsHorizontal = isHorizontal });
        }
    }
}
