using Queridor.Model;
using Queridor.ServicesAbstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quoridor.Model.Turns
{
    class MakeMoveTurn : Turn
    {
        private bool _isJumpTurn;
        private Dictionary<int, char> _cellLetterCoordinates = new Dictionary<int, char>()
        {
            [0] = 'A',
            [1] = 'B',
            [2] = 'C',
            [3] = 'D',
            [4] = 'E',
            [5] = 'F',
            [6] = 'G',
            [7] = 'H',
            [8] = 'I',
        };
        public MakeMoveTurn(bool isFirstPlayer, int x, int y) : base(isFirstPlayer, x, y)
        {
        }

        public override string ErrorMessage
        {
            get
            {
                var player = isFirstPlayer ? 1 : 2;
                return $"Player {player} can't move to position {x}:{y}";
            }
        }

        internal override bool CanExecute(ITurnCheckService turnCheckService)
        {
            var (canExecute, isJumpTurn) =  turnCheckService.CanMakeTurnCheck(isFirstPlayer, x, y);
            _isJumpTurn = isJumpTurn;
            return canExecute;
        }

        internal override void Execute(IMakeTurnService makeTurnService)
        {
            makeTurnService.MakeTurn(isFirstPlayer, x, y);
        }

        public override string ToString()
        {
            if (_isJumpTurn) return $"jump {_cellLetterCoordinates[x]}{y + 1}";
            return $"move {_cellLetterCoordinates[x]}{y + 1}";
        }
    }
}
