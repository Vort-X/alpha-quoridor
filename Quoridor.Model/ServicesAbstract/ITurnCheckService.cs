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
        (bool, bool) CanMakeTurnCheck(bool isFirstPlayer, int x, int y);
        bool CanPlaceWallCheck(bool isFirstPlayer, int x, int y, bool horizontal);
        List<Cell> FindAvaliableCells(bool isFirstPlayer);
        List<KeyValuePair<Corner, bool>> FindAvaliableWalls(bool isFirstPlayer);
        Cell GetPlayerCell(bool isFirstPlayer);
        int GetAvaliableWallsCount(bool isFirstPlayer);
        void DestroyWalls(Corner corner, bool horizontal);
        bool VictoryCheck(bool isFirstPlayer);
        float FuncFromBoard(bool isFirstPlayer);
        Corner GetCorner(int X, int Y);
    }
}
