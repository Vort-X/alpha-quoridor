using System;
using System.Collections.Generic;
using System.Text;

namespace Queridor.Model
{
    public class Pawn
    {
        public Cell Cell { get; internal set; }
        public int WinCoordinate { get; internal set; }
        public int AvailableWalls { get; internal set; } = 10;

        public override string ToString()
        {
            return $"Pawn at: {Cell}";
        }
    }
}
