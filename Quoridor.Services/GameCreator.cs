using Queridor.AIAlgotithms;
using Queridor.BoardFabric;
using Queridor.Services;
using Quoridor.Model.Abstract;
using Quoridor.Model.BoardPresenter;
using Quoridor.Model.BotAlgorithms;
using Quoridor.Model.GameManager;
using Quoridor.Model.PlayerTypes;
using System;

namespace Quoridor.Model
{
    public static class GameCreator
    {

        private static string _whitePawn = "white";
        private static string _blackPawn = "black";

        public static Game NewGameHardBotVsRandomBot(ITurnProviderConsole turnProvider, string pawnColor)
        {
            var bf = new BoardFactory();
            var b = bf.CreateBoard();
            var ast = new AStar();
            var mts = new MakeTurnsService(b);
            var tcs = new TurnCheckService(ast, b, mts);
            var gbp = new GraphBoardPresenter(b);

            var player1 = new RandomBotAlgorithm(b, pawnColor == "black", tcs);
            var rba = new HardBotAlgorithm(pawnColor == "white", tcs, mts);
            var player2 = new BotPlayer(rba);

            var dgm = pawnColor == _blackPawn ? new DefaultGameManager(gbp, mts, tcs, player1, player2) 
                : new DefaultGameManager(gbp, mts, tcs, player2, player1);

            return pawnColor == _blackPawn ? new Game() {GameManager = dgm, Player1 = player1, Player2 = player2} 
                : new Game() {GameManager = dgm, Player1 = player2, Player2 = player1};
        }

        public static Game NewGameVsConsole(ITurnProviderConsole turnProvider, string pawnColor)
        {
            var bf = new BoardFactory();
            var b = bf.CreateBoard();
            var ast = new AStar();
            var mts = new MakeTurnsService(b);
            var tcs = new TurnCheckService(ast, b, mts);
            var gbp = new GraphBoardPresenter(b);

            var player1 = new ConsolePlayer(pawnColor, turnProvider);
            var rba = new HardBotAlgorithm(pawnColor == "white", tcs, mts);
            var player2 = new BotPlayer(rba);

            var dgm = pawnColor == _blackPawn ? new DefaultGameManager(gbp, mts, tcs, player1, player2) 
                : new DefaultGameManager(gbp, mts, tcs, player2, player1);

            return pawnColor == _blackPawn ? new Game() {GameManager = dgm, Player1 = player1, Player2 = player2} 
                : new Game() {GameManager = dgm, Player1 = player2, Player2 = player1};
        }
        public static Game NewGameVsPlayer(ITurnProvider turnProvider)
        {
            var bf = new BoardFactory();
            var b = bf.CreateBoard();
            var ast = new AStar();
            var mts = new MakeTurnsService(b);
            var tcs = new TurnCheckService(ast, b, mts);
            var gbp = new GraphBoardPresenter(b);

            var player1 = new LocalPlayer(b.FirstPlayer, turnProvider, true);
            var player2 = new LocalPlayer(b.SecondPlayer, turnProvider, false);
            
            var dgm = new DefaultGameManager(gbp, mts, tcs, player1, player2);
            
            return new Game() { GameManager = dgm, Player1 = player1, Player2 = player2 };
        }

        public static Game NewGameVsBot(ITurnProvider turnProvider)
        {
            var bf = new BoardFactory();
            var b = bf.CreateBoard();
            var ast = new AStar();
            var mts = new MakeTurnsService(b);
            var tcs = new TurnCheckService(ast, b, mts);
            var gbp = new GraphBoardPresenter(b);

            var player1 = new LocalPlayer(b.FirstPlayer, turnProvider, true);
            var rba = new RandomBotAlgorithm(b, false, tcs);
            var player2 = new BotPlayer(rba);

            var dgm = new DefaultGameManager(gbp, mts, tcs, player1, player2);

            return new Game() { GameManager = dgm, Player1 = player1, Player2 = player2 };
        }
    }
}
