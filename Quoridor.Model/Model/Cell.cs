using System;
using System.Collections.Generic;
using System.Text;

namespace Queridor.Model
{
    public class Cell
    {
        public int X { get; internal set; }
        public int Y { get; internal set; }
        public List<Edge> Edges { get; internal set; }

        public override string ToString()
        {
            return $"({X}, {Y})";
        }
    }
}
