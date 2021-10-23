using System;
using Quoridor.Model.Abstract;
using Quoridor.Model.Turns;

namespace Quoridor.Model.PlayerTypes
{
    public interface ITurnProviderConsole
    {
        void RequestTurn(ConsolePlayer player);
    }
    public class ConsolePlayer: IPlayer
    {
        private readonly ITurnProviderConsole _turnProvider;
        private bool _isFirstPlayer;
        
        public ConsolePlayer(string pawnColor, ITurnProviderConsole turnProvider)
        {
            _turnProvider = turnProvider;
            UserFriendlyName = pawnColor;
            _isFirstPlayer = pawnColor == "black";
        }
        
        public event Action<IPlayer, Turn> TurnFinished;
        
        public void NotifyTurn()
        {
            _turnProvider.RequestTurn(this);
        }

        public string UserFriendlyName { get; }
        
        public void OnWallTurn(Tuple<int, int> cornerCoordinates, bool isHorizontal)
        {
            var wallTurn = new PlaceWallTurn(_isFirstPlayer, 
                cornerCoordinates.Item1, cornerCoordinates.Item2, isHorizontal);
            TurnFinished?.Invoke(this, wallTurn);
        }

        public void OnMoveTurn(Tuple<int, int> cellCoordinates)
        {
            var moveTurn = new MakeMoveTurn(_isFirstPlayer, cellCoordinates.Item1, cellCoordinates.Item2);
            TurnFinished?.Invoke(this, moveTurn);
        }
    }
}