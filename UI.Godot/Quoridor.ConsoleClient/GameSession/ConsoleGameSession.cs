using Quoridor.ConsoleClient.Constants;
using Quoridor.Model.Abstract;
using Quoridor.Model.PlayerTypes;
using Quoridor.Model.Turns;

namespace Quoridor.ConsoleClient
{
    class ConsoleGameSession: ITurnProviderConsole
    {
        private IPlayer _turnReciever;

        public bool GameHasFinished { get; private set; } = false;
        public ConsoleAPI Console { get; internal set; }

        internal void SubscribeOnEvents(IGameManager gameManager)
        {
            gameManager.BoardUpdated += OnBoardUpdate;
            gameManager.InvalidTurn += OnInvalidTurn;
            gameManager.PlayerWon += _ => GameHasFinished = true;
        }

        private void OnBoardUpdate(Turn turn, IPlayer sender)
        {
            if (sender != _turnReciever) Console.Write(turn.ToString());
        }
        
        private void OnInvalidTurn(string errorMessage)
        {
            Console.Write(errorMessage);
        }

        public void RequestTurn(ConsolePlayer turnReceiver)
        {
            _turnReciever = turnReceiver;
            
            var (turnType, turnParameters) = Console.ReadTurnData();
            TurnSenders.ByType(turnType).Invoke(turnReceiver, turnParameters);
        }
    }
}