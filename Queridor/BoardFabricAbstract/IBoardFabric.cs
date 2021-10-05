using Queridor.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Queridor.BoardFabricAbstract
{
    public interface IBoardFabric
    {
        Board CreateBoard();

        Pawn CreatePawn(Board board, bool isFirstPlayer);
    }
}
