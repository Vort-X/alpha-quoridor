using Queridor.AIAlgotithms;
using Queridor.BoardFabric;
using Queridor.Services;
using Quoridor.Model.Abstract;
using Quoridor.Model.BoardPresenter;
using Quoridor.Model.BotAlgorithms;
using Quoridor.Model.GameManager;
using Quoridor.Model.PlayerTypes;
using Quoridor.Model.TurnFactories;
using System;

namespace Quoridor.Model
{
    public static class GameCreator
    {
        public static Game NewGameVsPlayer(ITurnProvider turnProvider)
        {
            var bf = new BoardFactory();
            var b = bf.CreateBoard();
            var p1 = bf.CreatePawn(b, true);
            var p2 = bf.CreatePawn(b, false);
            var ast = new AStar();
            var mts = new MakeTurnsService();
            var tcs = new TurnCheckService(ast, mts);
            var gbp = new GraphBoardPresenter(p1, p2);

            var player1 = new LocalPlayer(p1, turnProvider, true);
            var player2 = new LocalPlayer(p2, turnProvider, false);
            
            var dgm = new DefaultGameManager(b, gbp, tcs, player1, player2);
            
            return new Game() { GameManager = dgm, Player1 = player1, Player2 = player2 };
        }

        public static Game NewGameVsBot(ITurnProvider turnProvider)
        {
            var bf = new BoardFactory();
            var b = bf.CreateBoard();
            var p1 = bf.CreatePawn(b, true);
            var p2 = bf.CreatePawn(b, false);
            var ast = new AStar();
            var mts = new MakeTurnsService();
            var tcs = new TurnCheckService(ast, mts);
            var gbp = new GraphBoardPresenter(p1, p2);

            var player1 = new LocalPlayer(p1, turnProvider, true);
            var rba = new RandomBotAlgorithm(b, p2, p1, tcs);
            var player2 = new BotPlayer(rba);

            var dgm = new DefaultGameManager(b, gbp, tcs, player1, player2);

            return new Game() { GameManager = dgm, Player1 = player1, Player2 = player2 };
        }
    }
}
