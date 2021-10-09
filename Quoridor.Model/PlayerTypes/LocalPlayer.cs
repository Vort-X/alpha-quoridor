using Quoridor.Model.Abstract;
using Quoridor.Model.Turns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quoridor.Model.TurnFactories;
using Queridor.Model;

namespace Quoridor.Model.PlayerTypes
{
    
    public interface ITurnProvider
    {
        void RequestTurn(LocalPlayer turnReceiver);
    }
    
    public class LocalPlayer : IPlayer
    {
        private readonly Pawn pawn;
        private readonly ITurnProvider _turnProvider;

        //public LocalPlayer(ITurnProvider turnProvider)
        //{
        //    _turnProvider = turnProvider;
        //}

        public LocalPlayer(Pawn pawn, ITurnProvider turnProvider)
        {
            this.pawn = pawn;
            _turnProvider = turnProvider;
        }

        public event Action<IPlayer, Turn> TurnFinished;

        public void NotifyTurn()
        {
            _turnProvider.RequestTurn(this);
        }

        public void OnWallTurn(Tuple<int, int> cornerCoordinates, bool isHorizontal)
        {
            
        }

        public void OnCellTurn(Tuple<int, int> cellCoordinates)
        {
            
        }
        
    }
}
