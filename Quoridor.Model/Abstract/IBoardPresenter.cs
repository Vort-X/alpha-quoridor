using Queridor.Model;
using Quoridor.Model.GameObjects;
using Quoridor.Model.Turns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quoridor.Model.Abstract
{
    public interface IBoardPresenter
    {
        Pawn Pawn1 { get; }
        Pawn Pawn2 { get; }
        List<Wall> Walls { get; }
        
    }
}
