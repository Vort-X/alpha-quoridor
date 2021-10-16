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
            if (!CheckIfWinPathExist(board.FirstPlayer, FindWinCells(board.FirstPlayer, board.Cells))
                || !CheckIfWinPathExist(board.SecondPlayer, FindWinCells(board.SecondPlayer, board.Cells)))
            {
                DestroyWalls(corner, horizontal);
                return false;
            }
            DestroyWalls(corner, horizontal);
            return true;
        }

        private void DestroyWalls(Corner corner, bool horizontal)
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

        private bool CheckIfWinPathExist(Pawn player, List<Cell> winCells)
        {
            return !winCells.TrueForAll(c => algorithm.FindBestWay(player.Cell, c) == null);
        }

        private List<Cell> FindWinCells(Pawn player, List<Cell> allCells)
        {
            List<Cell> result = new List<Cell>(allCells);
            result.RemoveRange(player.WinCoordinate == 0 ? 9 : 0, 72);
            return result;
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
    }
}