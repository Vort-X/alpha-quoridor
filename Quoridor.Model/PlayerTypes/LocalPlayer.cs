using Quoridor.Model.Abstract;
using Quoridor.Model.Turns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quoridor.Model.TurnFactories;

namespace Quoridor.Model.PlayerTypes
{
    
    public interface ITurnProvider
    {
        void RequestTurn(LocalPlayer turnReceiver);
    }
    
    public class LocalPlayer : IPlayer
    {
        private readonly ITurnProvider _turnProvider;

        public LocalPlayer(ITurnProvider turnProvider)
        {
            _turnProvider = turnProvider;
        }
        
        public event Action<IPlayer, Turn> TurnFinished;

        public void NotifyTurn()
        {
            _turnProvider.RequestTurn(this);
        }

        public void OnWallTurn(Tuple<int, int> cornerCoordinates)
        {
            
        }
    }
}
