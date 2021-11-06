using Queridor.Model;
using Queridor.ServicesAbstract;
using Quoridor.Model.Abstract;
using Quoridor.Model.Turns;
using System;
using System.Collections.Generic;

namespace Quoridor.Model.BotAlgorithms
{
    class HardBotAlgorithm : IBotAlgorithm
    {

        private readonly bool isFirstPlayer;
        private readonly ITurnCheckService turnCheckService;
        private readonly IMakeTurnService makeTurnService;

        public HardBotAlgorithm(bool isFirstPlayer, ITurnCheckService turnCheckService, IMakeTurnService makeTurnService)
        {
            this.isFirstPlayer = isFirstPlayer;
            this.turnCheckService = turnCheckService;
            this.makeTurnService = makeTurnService;
        }

        public Turn GetTurn()
        {
            var bestScore = float.MinValue;
            Cell startPos = turnCheckService.GetPlayerCell(isFirstPlayer);
            Turn result = null;
            foreach (Cell c in turnCheckService.FindAvaliableCells(isFirstPlayer))
            {
                if (turnCheckService.CanMakeTurnCheck(isFirstPlayer, c.X, c.Y).Item1)
                {
                    makeTurnService.MakeTurn(isFirstPlayer, c.X, c.Y);
                    float score = MiniMax(false, 0);
                    makeTurnService.MakeTurn(isFirstPlayer, startPos.X, startPos.Y);
                    if (score > bestScore)
                    {
                        bestScore = score;
                        result = new MakeMoveTurn(isFirstPlayer, c.X, c.Y);
                    }
                }
            }
            
            foreach (KeyValuePair<Corner, bool> kvp in turnCheckService.FindAvaliableWalls(!isFirstPlayer))
            {
                if (turnCheckService.GetAvaliableWallsCount(isFirstPlayer) > 0
                && turnCheckService.CanPlaceWallCheck(isFirstPlayer, kvp.Key.X, kvp.Key.Y, kvp.Value)) 
                {
                    makeTurnService.PlaceTestWall(isFirstPlayer, kvp.Key.X, kvp.Key.Y, kvp.Value);
                    float score = MiniMax(false, 0);
                    turnCheckService.DestroyWalls(kvp.Key, kvp.Value);
                    if (score > bestScore)
                    {
                        bestScore = score;
                        result = new PlaceWallTurn(isFirstPlayer, kvp.Key.X, kvp.Key.Y, kvp.Value); ;
                    }
                }
                    
            }
            
            return result;
        }

        private float MiniMax(bool isMaximazing, int depth)
        {
            if (turnCheckService.VictoryCheck(!isFirstPlayer))
            {
                return -100;
            }
            if (turnCheckService.VictoryCheck(isFirstPlayer))
            {
                return 100;
            }
            
            if (depth == 4)
            {
                return turnCheckService.FuncFromBoard(isFirstPlayer);
            }

            float bestScore;
            if (isMaximazing)
            {
                bestScore = float.MinValue;
                Cell newStartPos = turnCheckService.GetPlayerCell(isFirstPlayer);
                foreach (Cell c in turnCheckService.FindAvaliableCells(isFirstPlayer))
                {
                    if (turnCheckService.CanMakeTurnCheck(isFirstPlayer, c.X, c.Y).Item1)
                    {
                        makeTurnService.MakeTurn(isFirstPlayer, c.X, c.Y);
                        float score = MiniMax(false, depth + 1);
                        makeTurnService.MakeTurn(isFirstPlayer, newStartPos.X, newStartPos.Y);
                        bestScore = Math.Max(bestScore, score);
                    }
                }
                
                foreach (KeyValuePair<Corner, bool> kvp in turnCheckService.FindAvaliableWalls(!isFirstPlayer))
                {
                    if (turnCheckService.GetAvaliableWallsCount(isFirstPlayer) > 0
                     && turnCheckService.CanPlaceWallCheck(isFirstPlayer, kvp.Key.X, kvp.Key.Y, kvp.Value))
                    {
                        makeTurnService.PlaceTestWall(isFirstPlayer, kvp.Key.X, kvp.Key.Y, kvp.Value);
                        float score = MiniMax(false, depth + 1);
                        turnCheckService.DestroyWalls(kvp.Key, kvp.Value);
                        bestScore = Math.Max(bestScore, score);
                    }
                }
                
            }
            else
            {
                bestScore = float.MaxValue;
                Cell newStartPos = turnCheckService.GetPlayerCell(!isFirstPlayer);
                foreach (Cell c in turnCheckService.FindAvaliableCells(!isFirstPlayer))
                {
                    Turn t = new MakeMoveTurn(!isFirstPlayer, c.X, c.Y);
                    if (t.CanExecute(turnCheckService))
                    {
                        t.Execute(makeTurnService);
                        float score = MiniMax(true, depth + 1);
                        t = new MakeMoveTurn(!isFirstPlayer, newStartPos.X, newStartPos.Y);
                        t.Execute(makeTurnService);
                        bestScore = Math.Min(bestScore, score);
                    }
                }
            }
            return bestScore;
        }
    }
}
