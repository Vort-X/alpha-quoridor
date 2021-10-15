﻿using Queridor.AIAlgorithmsAbstract;
using Queridor.AIAlgotithms;
using Queridor.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Queridor.ServicesAbstract
{
    public interface ITurnCheckService
    {
        IMakeTurnService MakeTurnService { get; }
        IAstar Algorithm { get; }
        bool CanMakeTurnCheck(Cell finishCell, Pawn enemy, Pawn player, List<Cell> cells);
        bool CanPlaceWallCheck(List<Cell> cells, Corner corner, bool horizontal, Pawn player, Pawn enemy);
        List<Cell> FindAvaliableCells(Pawn user, Pawn enemy, List<Cell> cells);
        bool VictoryCheck(Pawn player);
    }
}
