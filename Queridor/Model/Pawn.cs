using System;
using System.Collections.Generic;
using System.Text;

namespace Queridor.Model
{
    public class Pawn
    {
        public Cell Cell { get; set; }
        public int WinCoordinate { get; set; }
        public int AvailableWalls { get; set; } = 10;
    }
}
