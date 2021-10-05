using Quoridor.Model.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quoridor.Model.Abstract
{
    public interface IBoardPresenter
    {
        PawnObj Pawn1 { get; }
        PawnObj Pawn2 { get; }
        List<Wall> Walls { get; }

        void MakeTurn(object turn);
    }
}
