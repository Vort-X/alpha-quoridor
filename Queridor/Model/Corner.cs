using System;
using System.Collections.Generic;
using System.Text;

namespace Queridor.Model
{
    public class Corner
    {
        public int Id { get; set; }
        public KeyValuePair<Edge, Edge> HorizontalEdges { get; set; }
        public KeyValuePair<Edge, Edge> VerticalEdges { get; set; }
    }
}
