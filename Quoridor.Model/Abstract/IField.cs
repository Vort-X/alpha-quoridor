using Quoridor.Model.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quoridor.Model.Abstract
{
    public interface IField
    {
        public Pawn Pawn1 { get; }
        public Pawn Pawn2 { get; }
        public List<Wall> Walls { get; }

        public PawnCell GetPawnCell(int x, int y);
        public WallCell GetWallCell(int x, int y);
    }
}
