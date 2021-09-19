using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quoridor.Model.Abstract
{
    public interface IGame
    {
        public IField Field { get; }

        public void NextTurn();
    }
}
