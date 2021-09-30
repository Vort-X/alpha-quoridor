using System;
using System.Collections.Generic;
using System.Text;

namespace Queridor.Model
{
    public class Corner
    {
        public int X { get; set; }
        public int Y { get; set; }
        public KeyValuePair<Edge, Edge> HorizontalEdges { get; set; }
        public KeyValuePair<Edge, Edge> VerticalEdges { get; set; }
    }
}
