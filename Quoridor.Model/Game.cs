using Quoridor.Model.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quoridor.Model
{
    public class Game
    {
        public IGameManager GameManager { get; internal set; }
        public IPlayer Player1 { get; internal set; }
        public IPlayer Player2 { get; internal set; }
    }
}
