using Queridor.AIAlgorithmsAbstract;
using Queridor.AIAlgotithms;
using Queridor.Model;
using Queridor.ServicesAbstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Queridor.Services
{
    public class TurnCheckService : ITurnCheckService
    {
        public IAstar Algoritms { get; set; }
        public bool CanMakeTurnCheck(Cell startCell, Cell finishCell)
        {
            return Algoritms.GetHeuristicPathLength(startCell, finishCell) == 1;
            // доробити перевірку на стіни
        }

        public bool CanPlaceWallCheck(Corner corner, bool horizontal)
        {
            return horizontal ? (!corner.HorizontalEdges.Key.IsBlocked & !corner.HorizontalEdges.Key.IsBlocked) 
                : (!corner.VerticalEdges.Key.IsBlocked & !corner.VerticalEdges.Key.IsBlocked);
        }

        public bool VictoryCheck(Cell cellToCheck, bool isFirstPlayer)
        {
            return isFirstPlayer ? (cellToCheck.X == 0) : (cellToCheck.X == 8);
        }
    }
}
