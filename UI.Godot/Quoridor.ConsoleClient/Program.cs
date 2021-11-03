using Quoridor.Model;
using Quoridor.Model.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quoridor.ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var consoleGame = new ConsoleGameSession();
            var gameManager = CreateManager(consoleGame);

            while (!consoleGame.GameHasFinished)
            {
                gameManager.GameLoop();
            }
        }

        static IGameManager CreateManager(ConsoleGameSession consoleGame)
        {
            var console = new ConsoleAPI();
            var pawnColor = console.Read();
            var gameManager = GameCreator.NewGameVsConsole(consoleGame, pawnColor).GameManager;
            consoleGame.Console = console;
            consoleGame.SubscribeOnEvents(gameManager);
            return gameManager;
        }
    }
}
