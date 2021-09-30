using Queridor.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quoridor.Model.GameObjects
{
    public class Pawn
    {
        public Cell Cell { get; internal set; }
        public int AwailableWalls { get; internal set; } = 10;
    }
}
