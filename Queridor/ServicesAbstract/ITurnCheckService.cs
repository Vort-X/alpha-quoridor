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
        bool CanMakeTurnCheck(bool isFirstPlayer, int x, int y);
        bool CanPlaceWallCheck(bool isFirstPlayer, int x, int y, bool horizontal);
        List<Cell> FindAvaliableCells(bool isFirstPlayer);
        int GetAvaliableWallsCount(bool isFirstPlayer);
        bool VictoryCheck(bool isFirstPlayer);
    }
}
