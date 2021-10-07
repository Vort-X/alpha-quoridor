using Queridor.BoardFabricAbstract;
using Queridor.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Queridor.BoardFabric
{
    public class BoardFabric : IBoardFabric
    {
        public Board CreateBoard()
        {
            Board board = new Board() { Cells = new List<Cell>(), Corners = new List<Corner>() };

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    board.Cells.Add(new Cell() { X = i, Y = j, Edges = new List<Edge>() });

                }
            }


            for (int i = 0; i < 81; i++)
            {
                if (i + 1 < 81 && !((i + 1) % 9 == 0))
                {
                    Edge e1 = new Edge() { IsBlocked = false, Cells = new KeyValuePair<Cell, Cell>(board.Cells[i], board.Cells[i + 1]) };
                    board.Cells[i].Edges.Add(e1);
                    board.Cells[i + 1].Edges.Add(e1);
                }
                if (i + 9 < 81)
                {
                    Edge e2 = new Edge() { IsBlocked = false, Cells = new KeyValuePair<Cell, Cell>(board.Cells[i], board.Cells[i + 9]) };
                    board.Cells[i].Edges.Add(e2);
                    board.Cells[i + 9].Edges.Add(e2);

                }


            }


            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    board.Corners.Add(new Corner()
                    {
                        X = i,
                        Y = j,
                        HorizontalEdges = new KeyValuePair<Edge, Edge>
                            (FindEdgeBetween(board.Cells[j + 9 * i], board.Cells[j + 9 * i + 9]),
                            FindEdgeBetween(board.Cells[j + 9 * i + 1], board.Cells[j + 9 * i + 10])),

                        VerticalEdges = new KeyValuePair<Edge, Edge>
                            (FindEdgeBetween(board.Cells[j + 9 * i], board.Cells[j + 9 * i + 1]),
                            FindEdgeBetween(board.Cells[j + 9 * i + 9], board.Cells[j + 9 * i + 10]))
                    }); ;
                }
            }


            return board;
        }

        private Edge FindEdgeBetween(Cell cell1, Cell cell2)
        {
            return cell1.Edges.Find(i => i.Cells.Key == cell1 ? i.Cells.Value == cell2 : i.Cells.Key == cell2);
        }

        public Pawn CreatePawn(Board board, bool isFirstPlayer)
        {
            return isFirstPlayer ? new Pawn() {  Cell = board.Cells[4], WinXCoordinate = 8 } : new Pawn() { Cell = board.Cells[76], WinXCoordinate = 0 };
        }
    }
}
