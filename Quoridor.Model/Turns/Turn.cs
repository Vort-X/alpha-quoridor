using Queridor.Model;
using Queridor.ServicesAbstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quoridor.Model.Turns
{
    public abstract class Turn
    {
        protected Pawn player;
        protected int x;
        protected int y;

        protected Turn(Pawn player, int x, int y)
        {
            this.player = player;
            this.x = x;
            this.y = y;
        }

        public Pawn Player => player;

        internal abstract void Execute(Board board, Pawn player, Pawn enemy, ITurnCheckService turnCheckService);
    }
}
