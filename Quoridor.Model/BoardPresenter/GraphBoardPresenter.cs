using Queridor.Model;
using Queridor.ServicesAbstract;
using Quoridor.Model.Abstract;
using Quoridor.Model.GameObjects;
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

        public GraphBoardPresenter(Board board, ITurnCheckService turnCheckService)
        {
            this.board = board;
            this.turnCheckService = turnCheckService;
            Walls = new List<Wall>();
        }

        public Pawn Pawn1 { get; set; }
        public Pawn Pawn2 { get; set; }
        public List<Wall> Walls { get; set; }

        public void MakeTurn(object turn)
        {
            throw new NotImplementedException();
        }
    }
}
