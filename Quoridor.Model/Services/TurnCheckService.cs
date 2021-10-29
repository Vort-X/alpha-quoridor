using Queridor.AIAlgorithmsAbstract;
using Queridor.AIAlgotithms;
using Queridor.Model;
using Queridor.ServicesAbstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Queridor.Services
{
    public class TurnCheckService : ITurnCheckService
    {
        private IAstar algorithm;
        private Board board;
        private IMakeTurnService makeTurnService;

        public TurnCheckService(IAstar algorithm, Board board, IMakeTurnService makeTurnService)
        {
            this.algorithm = algorithm;
            this.board = board;
            this.makeTurnService = makeTurnService;
        }

        public bool CanMakeTurnCheck(bool isFirstPlayer, int x, int y)
        {
            var finishCell = board.Cells.Find(c => c.X == x && c.Y == y);
            if (board.FirstPlayer.Cell == finishCell 
                || board.SecondPlayer.Cell == finishCell) return false;
            else return CheckSituationWithMoveThroughtEnemy(finishCell,
                isFirstPlayer ? board.SecondPlayer : board.FirstPlayer,
                isFirstPlayer ? board.FirstPlayer : board.SecondPlayer, board.Cells)
                    || FindAvaliableNeihbours(isFirstPlayer ? board.FirstPlayer.Cell : board.SecondPlayer.Cell).Contains(finishCell);
        }

        private List<Cell> FindAvaliableNeihbours(Cell start)
        {
            return start.Edges.Where(e => !e.IsBlocked)
                .Select(e => (e.Cells.Key != start) ? e.Cells.Key : e.Cells.Value)
                .ToList();
        }

        private bool CheckSituationWithMoveThroughtEnemy(Cell finishCell, Pawn enemy, Pawn player, List<Cell> cells)
        {
            List<Cell> enemyNeighbours = FindAvaliableNeihbours(enemy.Cell);
            if (FindAvaliableNeihbours(player.Cell).Contains(enemy.Cell) 
                && enemyNeighbours.Contains(finishCell)) 
            {
                Cell cellBehind = FindCellByCoordinates(FindCellCoordinatesBehind(enemy, player), cells);

                if (cellBehind == null) return true; 

                if (enemyNeighbours.Contains(cellBehind) && cellBehind != finishCell) return false;

                return true;
            } 
            
            return false;
        }

        private KeyValuePair<int, int> FindCellCoordinatesBehind(Pawn enemy, Pawn player)
        {
            if(player.Cell.X - enemy.Cell.X == 1) return new KeyValuePair<int, int>(enemy.Cell.X - 1, enemy.Cell.Y);
            else if (player.Cell.X - enemy.Cell.X == -1) return new KeyValuePair<int, int>(enemy.Cell.X + 1, enemy.Cell.Y);
            else if (player.Cell.Y - enemy.Cell.Y == 1) return new KeyValuePair<int, int>(enemy.Cell.X, enemy.Cell.Y - 1);
            else return new KeyValuePair<int, int>(enemy.Cell.X, enemy.Cell.Y + 1);
        }

        private Cell FindCellByCoordinates(KeyValuePair<int, int> coords, List<Cell> allCells)
        {
            return allCells.FirstOrDefault(c => c.X == coords.Key && c.Y == coords.Value);
        }
        public bool CanPlaceWallCheck(bool isFirstPlayer, int x, int y, bool horizontal)
        {
            var corner = board.Corners.Find(c => c.X == x && c.Y == y);
            if ((horizontal & (corner.HorizontalEdges.Key.IsBlocked || corner.HorizontalEdges.Value.IsBlocked))
                || (!horizontal & (corner.VerticalEdges.Key.IsBlocked || corner.VerticalEdges.Value.IsBlocked))
                || (!horizontal & corner.HorizontalEdges.Key.IsBlocked & corner.HorizontalEdges.Value.IsBlocked)
                || (horizontal & corner.VerticalEdges.Key.IsBlocked & corner.VerticalEdges.Value.IsBlocked))
                return false;
            makeTurnService.PlaceWall(isFirstPlayer, x, y, horizontal);
            if (isFirstPlayer) 
                board.FirstPlayer.AvailableWalls++;
            else 
                board.SecondPlayer.AvailableWalls++;
            if (!CheckIfWinPathExist(board.FirstPlayer)
                || !CheckIfWinPathExist(board.SecondPlayer))
            {
                DestroyWalls(corner, horizontal);
                return false;
            }
            DestroyWalls(corner, horizontal);
            return true;
        }

        public void DestroyWalls(Corner corner, bool horizontal)
        {
            if (horizontal)
            {
                corner.HorizontalEdges.Key.IsBlocked = false;
                corner.HorizontalEdges.Value.IsBlocked = false;
            }
            else
            {
                corner.VerticalEdges.Key.IsBlocked = false;
                corner.VerticalEdges.Value.IsBlocked = false;
            }
        }

        private bool CheckIfWinPathExist(Pawn player)
        {
            return algorithm.FindBestWay(player, board) != null;
        }

        public bool VictoryCheck(bool isFirstPlayer)
        {
            return isFirstPlayer ?
                board.FirstPlayer.Cell.Y == board.FirstPlayer.WinCoordinate
                : board.SecondPlayer.Cell.Y == board.SecondPlayer.WinCoordinate;
        }

        public List<Cell> FindAvaliableCells(bool isFirstPlayer)
        {
            List<Cell> neihbours = FindAvaliableNeihbours(isFirstPlayer ? board.FirstPlayer.Cell : board.SecondPlayer.Cell);
            if (neihbours.Contains(isFirstPlayer ? board.SecondPlayer.Cell : board.FirstPlayer.Cell))
            {
                neihbours.AddRange(FindAvaliableNeihbours(isFirstPlayer ? board.SecondPlayer.Cell : board.FirstPlayer.Cell)
                                        .Where(e => CanMakeTurnCheck(isFirstPlayer, e.X, e.Y))
                                        .ToList());
                neihbours.Remove(isFirstPlayer ? board.SecondPlayer.Cell : board.FirstPlayer.Cell);
            }
            return neihbours;
        }

        public int GetAvaliableWallsCount(bool isFirstPlayer)
        {
            return isFirstPlayer ? board.FirstPlayer.AvailableWalls : board.SecondPlayer.AvailableWalls;
        }

        public Cell GetPlayerCell(bool isFirstPlayer)
        {
            return isFirstPlayer ? board.FirstPlayer.Cell : board.SecondPlayer.Cell;
        }

        public List<KeyValuePair<Corner, bool>> FindAvaliableWalls(bool isFirstPlayer)
        {
            List<KeyValuePair<Corner, bool>> result = new List<KeyValuePair<Corner, bool>>();
            int X = GetPlayerCell(isFirstPlayer).X;
            int Y = GetPlayerCell(isFirstPlayer).Y;
            int[] coords = new int[2] { -1, 0 };
            int y = isFirstPlayer ? -1 : 0;
            foreach(int x in coords)
            {
               Corner c = GetCorner(X + x, Y + y);
               if (c != null)
               {
                    result.Add(new KeyValuePair<Corner, bool>(c, true));
                    result.Add(new KeyValuePair<Corner, bool>(c, false));
               }
           
            }
            return result;
        }

        public float FuncFromBoard(bool isFirstPlayer)
        {
            List<Cell> bestWay = algorithm.FindBestWay(isFirstPlayer ? board.FirstPlayer : board.SecondPlayer, board);
            
            List<Cell> enemyBestWay = algorithm.FindBestWay(isFirstPlayer ? board.SecondPlayer : board.FirstPlayer, board);
        
            return (float)1/bestWay.Count - (float)1/enemyBestWay.Count; 
            
        }

        public Corner GetCorner(int X, int Y)
        {
            return board.Corners.Find(i => i.X == X && i.Y == Y);
        }
    }
}