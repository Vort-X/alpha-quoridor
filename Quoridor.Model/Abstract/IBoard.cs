using Quoridor.Model.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quoridor.Model.Abstract
{
    public interface IBoard
    {
        Pawn Pawn1 { get; }
        Pawn Pawn2 { get; }
        List<Wall> Walls { get; }

        PawnCell GetPawnCell(int x, int y);
        WallCell GetWallCell(int x, int y);
    }
}
