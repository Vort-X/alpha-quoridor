using Queridor.AIAlgorithmsAbstract;
using Queridor.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Queridor.AIAlgotithms
{
    public class AStar : IAstar
    {
        public List<Cell> FindBestWay(Pawn p, Board board)
        {
            Collection<PathNode> openSet = new Collection<PathNode>();
            Collection<PathNode> closedSet = new Collection<PathNode>();

            PathNode startNode = new PathNode()
            {
                Position = p.Cell,
                CameFrom = null,
                PathLengthFromStart = 0,
                HeuristicEstimatePathLength = GetHeuristicPathLength(p.Cell, p.WinCoordinate)
            };

            openSet.Add(startNode);
            while (openSet.Count > 0)
            {
                PathNode currentNode = openSet.OrderBy(node =>
                  node.EstimateFullPathLength).First();

                if (currentNode.Position.Y == p.WinCoordinate)
                    return GetPathForNode(currentNode);

                openSet.Remove(currentNode);
                closedSet.Add(currentNode);

                foreach (PathNode neighbourNode in GetNeighbours(currentNode, p.WinCoordinate))
                {
                    if (closedSet.Count(node => node.Position == neighbourNode.Position) > 0)
                        continue;

                    PathNode openNode = openSet.FirstOrDefault(node =>
                      node.Position == neighbourNode.Position);

                    if (openNode == null)
                        openSet.Add(neighbourNode);
                    else
                      if (openNode.PathLengthFromStart > neighbourNode.PathLengthFromStart)
                    {
                        openNode.CameFrom = currentNode;
                        openNode.PathLengthFromStart = neighbourNode.PathLengthFromStart;
                    }
                }
            }
            return null;
        }

        public int GetHeuristicPathLength(Cell start, int Y)
        {
            return Y-start.Y;
        }

        private Collection<PathNode> GetNeighbours(PathNode pathNode, int X)
        {
            var result = new Collection<PathNode>();

            foreach (Edge e in pathNode.Position.Edges)
            {
                if (!e.IsBlocked)
                {
                    var neighbourNode = new PathNode()
                    {
                        Position = (e.Cells.Key != pathNode.Position) ? (e.Cells.Key) : (e.Cells.Value),
                        CameFrom = pathNode,
                        PathLengthFromStart = pathNode.PathLengthFromStart + 1,
                    };

                    neighbourNode.HeuristicEstimatePathLength = GetHeuristicPathLength(neighbourNode.Position, X);
                    result.Add(neighbourNode);
                }
            }
            return result;
        }

        /*
        public List<Cell> FindBestWay(Cell startCell, Cell finishCell)
        {
            Collection<PathNode> openSet = new Collection<PathNode>();
            Collection<PathNode> closedSet = new Collection<PathNode>();

            PathNode startNode = new PathNode()
            {
                Position = startCell,
                CameFrom = null,
                PathLengthFromStart = 0,
                HeuristicEstimatePathLength = GetHeuristicPathLength(startCell, finishCell)
            };

            openSet.Add(startNode);
            while (openSet.Count > 0)
            {
                PathNode currentNode = openSet.OrderBy(node =>
                  node.EstimateFullPathLength).First();

                if (currentNode.Position == finishCell)
                    return GetPathForNode(currentNode);

                openSet.Remove(currentNode);
                closedSet.Add(currentNode);
     
                foreach (PathNode neighbourNode in GetNeighbours(currentNode, finishCell))
                {
                    if (closedSet.Count(node => node.Position == neighbourNode.Position) > 0)
                        continue;
                    
                    PathNode openNode = openSet.FirstOrDefault(node =>
                      node.Position == neighbourNode.Position);
                    
                    if (openNode == null)
                        openSet.Add(neighbourNode);
                    else
                      if (openNode.PathLengthFromStart > neighbourNode.PathLengthFromStart)
                    {
                        openNode.CameFrom = currentNode;
                        openNode.PathLengthFromStart = neighbourNode.PathLengthFromStart;
                    }
                }
            }
            return null;
        }
        */
        private static List<Cell> GetPathForNode(PathNode pathNode)
        {
            List<Cell> result = new List<Cell>();
            PathNode currentNode = pathNode;
            while (currentNode != null)
            {
                result.Add(currentNode.Position);
                currentNode = currentNode.CameFrom;
            }
            result.Reverse();
            return result;
        }
        /*
        public int GetHeuristicPathLength(Cell startCell, Cell finishCell)
        {
            return Math.Abs(startCell.X - finishCell.X) + Math.Abs(startCell.Y - finishCell.Y);
        }

        private Collection<PathNode> GetNeighbours(PathNode pathNode, Cell goal)
        {
           var result = new Collection<PathNode>();

           foreach(Edge e in pathNode.Position.Edges)
            {
                if (!e.IsBlocked) 
                {
                    var neighbourNode = new PathNode()
                    {
                        Position = (e.Cells.Key != pathNode.Position) ? (e.Cells.Key) : (e.Cells.Value),
                        CameFrom = pathNode,
                        PathLengthFromStart = pathNode.PathLengthFromStart + 1,
                    };

                    neighbourNode.HeuristicEstimatePathLength = GetHeuristicPathLength(neighbourNode.Position, goal);
                    result.Add(neighbourNode);
                }
            }
            return result;
        }
        */
    }
}
