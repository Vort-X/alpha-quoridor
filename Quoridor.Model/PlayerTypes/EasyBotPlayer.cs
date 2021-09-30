using Queridor.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quoridor.Model.PlayerTypes
{
    class EasyBotPlayer : BotPlayer
    {
        public EasyBotPlayer(Board board) : base(board)
        {
        }

        protected override object GetTurnFromAlgorythm()
        {
            throw new NotImplementedException();
        }
    }
}
