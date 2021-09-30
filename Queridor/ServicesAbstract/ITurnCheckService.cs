using Queridor.AIAlgorithmsAbstract;
using Queridor.AIAlgotithms;
using Queridor.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Queridor.ServicesAbstract
{
    public interface ITurnCheckService
    {
        IAstar Algoritms { get; set; }
        bool CanMakeTurnCheck(Cell startCell, Cell finishCell);
        bool CanPlaceWallCheck(Corner corner, bool horizontal);
        bool VictoryCheck(Cell cellToCheck, bool isFirstPlayer);
    }
}
