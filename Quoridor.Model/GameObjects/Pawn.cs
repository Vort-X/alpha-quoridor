using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quoridor.Model.GameObjects
{
    public class Pawn
    {
        public PawnCell Cell { get; internal set; }
        public int AvailableWalls { get; internal set; } = 10;
    }
}
