using Quoridor.Model.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quoridor.Model.Abstract
{
    abstract class Player
    {
        public Pawn Pawn { get; }

        //TODO: commands
        internal abstract object RequestTurn();
    }
}
