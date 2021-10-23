using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Queridor.Services;
using Quoridor.Model;
using Quoridor.Model.Abstract;
using Quoridor.Model.PlayerTypes;
using Quoridor.Model.Turns;

namespace Quoridor.ConsoleClient
{
    class ConsoleGameSession: ITurnProviderConsole
    {
        private IPlayer _turnReciever;
        
        private Dictionary<char, int> _cellLetterCoordinates = new Dictionary<char, int>()
        {
            ['A'] = 1,
            ['B'] = 2,
            ['C'] = 3,
            ['D'] = 4,
            ['E'] = 5,
            ['F'] = 6,
            ['G'] = 7,
            ['H'] = 8,
            ['I'] = 9,
        };
        
        private Dictionary<char, int> _wallLetterCoordinates = new Dictionary<char, int>()
        {
            ['S'] = 1,
            ['T'] = 2,
            ['U'] = 3,
            ['V'] = 4,
            ['W'] = 5,
            ['X'] = 6,
            ['Y'] = 7,
            ['Z'] = 8,
        };

        static void Main(string[] args)
        {
            var pawnColor = Console.ReadLine();
            var consoleGame = new ConsoleGameSession();

            var gameManager = GameCreator.NewGameVsConsole(consoleGame, pawnColor).GameManager;
            gameManager.BoardUpdated += consoleGame.OnBoardUpdate;

            var isGameFinished = false;
            gameManager.PlayerWon += _ => isGameFinished = true;
            
            while (!isGameFinished)
            {
                gameManager.GameLoop();
            }
        }

        private void OnBoardUpdate(Turn turn, IPlayer sender)
        {
            if (sender != _turnReciever)
                Console.WriteLine(turn.ToString());
        }

        public void RequestTurn(ConsolePlayer turnReceiver)
        {
            _turnReciever = turnReceiver;
            
            var (turnType, turnParameters) = ReadTurnData();

            if (isMoveTurn(turnType))
            {
                SendMoveTurn(turnReceiver, turnParameters);
            }
            else
            {
                SendWallTurn(turnReceiver, turnParameters);
            }
            
        }

        private void SendWallTurn(ConsolePlayer turnReceiver, string turnParameters)
        {
            int x = _wallLetterCoordinates[turnParameters[0]];
            int y = turnParameters[1] - '0';
            bool isHorizontal = turnParameters[2] == 'h';

            turnReceiver.OnWallTurn(new Tuple<int, int>(x - 1, y - 1), isHorizontal);
        }

        private void SendMoveTurn(ConsolePlayer turnReceiver, string turnParameters)
        {
            int x = _cellLetterCoordinates[turnParameters[0]];
            int y = turnParameters[1] - '0';

            turnReceiver.OnMoveTurn(new Tuple<int, int>(x - 1, y - 1));
        }

        private bool isMoveTurn(string turnType)
        {
            return turnType == "move";
        }

        private static (string turnType, string turnParameters) ReadTurnData()
        {
            var turnString = Console.ReadLine();
            
            var turnType = turnString.Split(' ')[0];
            var turnParameters = turnString.Split(' ')[1];
            
            return (turnType, turnParameters);
        }
    }
}