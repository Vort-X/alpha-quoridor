using Queridor.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Queridor.BoardFabricAbstract
{
    public interface IBoardFactory
    {
        Board CreateBoard();
    }
}
