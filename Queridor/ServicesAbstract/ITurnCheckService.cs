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
        bool CanMakeTurnCheck(Cell finishCell, bool isFirstPlayer);
        bool CanPlaceWallCheck(Corner corner, bool horizontal);
        List<Cell> FindAvaliableCells(bool isFirstPlayer);
        bool VictoryCheck(bool isFirstPlayer);
    }
}
