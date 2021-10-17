using System;
using System.Collections.Generic;
using System.Text;

namespace Queridor.Model
{
    public class Edge
    {
        public KeyValuePair<Cell, Cell> Cells { get; internal set; }
        public bool IsBlocked { get; internal set; }
    }
}
