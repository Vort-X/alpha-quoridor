using Queridor.Model;
using Queridor.ServicesAbstract;
using Quoridor.Model.Abstract;
using Quoridor.Model.Turns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quoridor.Model.PlayerTypes
{
    class EasyBotPlayer : BotPlayer
    {
        private readonly Pawn pawn;
        private readonly ITurnCheckService turnCheckService;

        public EasyBotPlayer(Pawn pawn, ITurnCheckService turnCheckService)
        {
            this.pawn = pawn;
            this.turnCheckService = turnCheckService;
        }

        protected override Turn GetTurnFromAlgorythm()
        {
            throw new NotImplementedException();
        }
    }
}
