using System;
using System.Collections.Generic;
using System.Text;

namespace Queridor.Model
{
    public class Cell
    {
        public int X { get; set; }
        public int Y { get; set; }
        public List<Edge> Edges { get; set; }
    }
}
