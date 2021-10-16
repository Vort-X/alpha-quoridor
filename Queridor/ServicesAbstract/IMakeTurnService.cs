using Queridor.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Queridor.ServicesAbstract
{
    public interface IMakeTurnService
    {
        void MakeTurn(bool isFirstPlayer, int x, int y);
        void PlaceWall(bool isFirstPlayer, int x, int y, bool horizontal);
    }
}
