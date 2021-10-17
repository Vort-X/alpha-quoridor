using System;
using System.Collections.Generic;
using System.Text;

namespace Queridor.Model
{
    public class Corner
    {
        public int X { get; internal set; }
        public int Y { get; internal set; }
        public KeyValuePair<Edge, Edge> HorizontalEdges { get; internal set; }
        public KeyValuePair<Edge, Edge> VerticalEdges { get; internal set; }
    }
}
