using Queridor.Model;
using Queridor.ServicesAbstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quoridor.Model.Turns
{
    class PlaceWallTurn : Turn
    {
        protected bool horizontal;

        public PlaceWallTurn(bool isFirstPlayer, int x, int y, bool horizontal) : base(isFirstPlayer, x, y)
        {
            this.horizontal = horizontal;
        }

        public override string ErrorMessage 
        { 
            get
            {
                var player = isFirstPlayer ? 1 : 2;
                var axis = horizontal ? "horizontal" : "vertical";
                return $"Player {player} can't place {axis} wall at {x}:{y}"; 
            } 
        }

        public event Action<int, int, bool> PlaceWall; //TODO: find better solution

        internal override bool CanExecute(ITurnCheckService turnCheckService)
        {
            return turnCheckService.GetAvaliableWallsCount(isFirstPlayer) > 0
                && turnCheckService.CanPlaceWallCheck(isFirstPlayer, x, y, horizontal);
        }

        internal override void Execute(IMakeTurnService makeTurnService)
        {
            makeTurnService.PlaceWall(isFirstPlayer, x, y, horizontal);
            PlaceWall(x, y, horizontal);
        }
    }
}
