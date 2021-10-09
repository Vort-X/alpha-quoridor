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
        public GraphBoardPresenter(Pawn pawn1, Pawn pawn2)
        {
            Pawn1 = pawn1;
            Pawn2 = pawn2;
            Walls = new List<Wall>();
        }

        public Pawn Pawn1 { get; set; }
        public Pawn Pawn2 { get; set; }
        public List<Wall> Walls { get; set; }
    }
}
