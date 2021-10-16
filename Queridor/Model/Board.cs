using System;
using System.Collections.Generic;
using System.Text;

namespace Queridor.Model
{
    public class Board
    {
        public List<Cell> Cells { get; set; }
        public List<Corner> Corners { get; set; }
        public Pawn firstPlayer { get; set; }
        public Pawn secondPlayer { get; set; }
    }
}
