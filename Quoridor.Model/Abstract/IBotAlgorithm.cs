using Quoridor.Model.Turns;
using System;
using System.Collections.Generic;
using System.Text;

namespace Quoridor.Model.Abstract
{
    public interface IBotAlgorithm
    {
        Turn GetTurn();
    }
}
