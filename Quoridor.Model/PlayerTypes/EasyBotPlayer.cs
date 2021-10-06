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
        private readonly ITurnCheckService turnCheckService;

        public EasyBotPlayer(ITurnCheckService turnCheckService)
        {
            this.turnCheckService = turnCheckService;
        }

        protected override Turn GetTurnFromAlgorythm()
        {
            throw new NotImplementedException();
        }
    }
}
