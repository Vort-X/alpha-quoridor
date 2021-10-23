using Queridor.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quoridor.Model.GameObjects
{
    public class Wall
    {
        public Corner Corner { get; internal set; }
        public bool IsHorizontal { get; internal set; }
    }
}
