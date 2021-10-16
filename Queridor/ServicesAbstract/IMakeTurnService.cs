using Queridor.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Queridor.ServicesAbstract
{
    public interface IMakeTurnService
    {
        void MakeTurn(bool isFirstPlayer, Cell cell);
        void PlaceWall(Corner corner, bool horizontal);
    }
}
