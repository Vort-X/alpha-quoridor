using System;
using System.Collections.Generic;
using System.Text;

namespace Queridor.Model
{
    public class Board
    {
        public List<Cell> Cells { get; internal set; }
        public List<Corner> Corners { get; internal set; }
        public Pawn FirstPlayer { get; internal set; }
        public Pawn SecondPlayer { get; internal set; }
    }
}
