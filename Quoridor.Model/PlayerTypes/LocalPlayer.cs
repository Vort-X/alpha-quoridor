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
        public Pawn Pawn { get; private set; }
        private readonly ITurnProvider _turnProvider;

        //public LocalPlayer(ITurnProvider turnProvider)
        //{
        //    _turnProvider = turnProvider;
        //}

        public LocalPlayer(Pawn pawn, ITurnProvider turnProvider)
        {
            Pawn = pawn;
            _turnProvider = turnProvider;
        }

        public event Action<IPlayer, Turn> TurnFinished;

        public void NotifyTurn()
        {
            _turnProvider.RequestTurn(this);
        }

        public void OnWallTurn(Tuple<int, int> cornerCoordinates, bool isHorizontal)
        {
            var wallTurn = PlaceWallTurnFactory.CreateTurn(Pawn, 
                cornerCoordinates.Item1, cornerCoordinates.Item2, isHorizontal);
            TurnFinished?.Invoke(this, wallTurn);
        }

        public void OnCellTurn(Tuple<int, int> cellCoordinates)
        {
            var moveTurn = MakeMoveTurnFactory.CreateTurn(Pawn, cellCoordinates.Item1, cellCoordinates.Item2);
            TurnFinished?.Invoke(this, moveTurn);
        }

        public override string ToString()
        {
            return $"{Pawn.Cell.X}, {Pawn.Cell.Y}";
        }
    }
}
