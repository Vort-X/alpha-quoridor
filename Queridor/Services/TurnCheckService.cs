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
        public TurnCheckService(IAstar algorithm, IMakeTurnService makeTurnService)
        {
            Algorithm = algorithm;
            MakeTurnService = makeTurnService;
        }

        public IAstar Algorithm { get; private set; }
        public IMakeTurnService MakeTurnService { get; private set; }

        public bool CanMakeTurnCheck(Cell finishCell, Pawn enemy, Pawn player, List<Cell> cells)
        {
            if (player.Cell == finishCell || enemy.Cell == finishCell) return false;
            else return CheckSituationWithMoveThroughtEnemy(finishCell, enemy, player, cells) 
                    || FindAvaliableNeihbours(player.Cell).Contains(finishCell);
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

        public bool CanPlaceWallCheck(List<Cell> cells, Corner corner, bool horizontal, Pawn player, Pawn enemy)
        {
            if ((horizontal & (corner.HorizontalEdges.Key.IsBlocked || corner.HorizontalEdges.Value.IsBlocked))
                || (!horizontal & (corner.VerticalEdges.Key.IsBlocked || corner.VerticalEdges.Value.IsBlocked))
                || (!horizontal & corner.HorizontalEdges.Key.IsBlocked & corner.HorizontalEdges.Value.IsBlocked)
                || (horizontal & corner.VerticalEdges.Key.IsBlocked & corner.VerticalEdges.Value.IsBlocked))
                return false;

            MakeTurnService.PlaceWall(corner, horizontal);
            if (!CheckIfWinPathExist(player, FindWinCells(player, cells))
                || !CheckIfWinPathExist(enemy, FindWinCells(enemy, cells)))
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
            return !winCells.TrueForAll(c => Algorithm.FindBestWay(player.Cell, c) == null);
        }

        private List<Cell> FindWinCells(Pawn player, List<Cell> allCells)
        {
            List<Cell> result = new List<Cell>(allCells);
            result.RemoveRange(player.WinCoordinate == 0 ? 9 : 0, 72);
            return result;
        }

        public bool VictoryCheck(Pawn player)
        {
            return player.Cell.Y == player.WinCoordinate;
        }

        public List<Cell> FindAvaliableCells(Pawn user, Pawn enemy, List<Cell> cells)
        {
            List<Cell> neihbours = FindAvaliableNeihbours(user.Cell);
            if (neihbours.Contains(enemy.Cell))
            {
                neihbours.AddRange(FindAvaliableNeihbours(enemy.Cell)
                                        .Where(e => e != user.Cell || CanMakeTurnCheck(e, enemy, user, cells))
                                        .ToList());
                neihbours.Remove(enemy.Cell);
            }
            return neihbours;
        }
    }
}
