using Queridor.AIAlgotithms;
using Queridor.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Queridor.AIAlgorithmsAbstract
{
    public interface IAstar
    {
        List<Cell> FindBestWay(Cell startCell, Cell finishCell);
        int GetHeuristicPathLength(Cell startCell, Cell finishCell);
    }
}
