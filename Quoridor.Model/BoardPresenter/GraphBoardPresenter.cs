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
        private readonly Board board;

        public GraphBoardPresenter(Board board)
        {
            this.board = board;
            Walls = new List<Wall>();
        }

        public Pawn Pawn1 => board.FirstPlayer;
        public Pawn Pawn2 => board.SecondPlayer;
        public List<Wall> Walls { get; set; }

        public void PlaceWall(int x, int y, bool horizontal)
        {
            Walls.Add(new Wall { Corner = board.Corners.Find(c => c.X == x && c.Y == y), IsHorizontal = horizontal});
        }
    }
}
