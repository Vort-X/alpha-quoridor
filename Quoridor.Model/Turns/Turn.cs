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
        protected bool isFirstPlayer;
        protected int x;
        protected int y;

        protected Turn(bool isFirstPlayer, int x, int y)
        {
            this.isFirstPlayer = isFirstPlayer;
            this.x = x;
            this.y = y;
        }

        public bool IsFirstPlayer => isFirstPlayer;

        internal abstract void Execute(Board board, Pawn player, Pawn enemy, ITurnCheckService turnCheckService);
    }
}
