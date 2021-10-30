using System;
using System.Collections.Generic;
using System.Text;

namespace Queridor.Model
{
    public class Pawn
    {
        public Cell Cell { get; internal set; }
        public int WinCoordinate { get; internal set; }

        private int _availableWalls = 10;

        public int AvailableWalls
        {
            get => _availableWalls;
            internal set => _availableWalls = value;
        }

        public override string ToString()
        {
            return $"Pawn at: {Cell}";
        }
    }
}
