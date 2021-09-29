using Queridor.AIAlgorithmsAbstract;
using Queridor.AIAlgotithms;
using Queridor.Model;
using Queridor.Services;
using Queridor.ServicesAbstract;
using System;
using System.Collections.Generic;

namespace CosoleMainTester
{
    class Program
    {
        static void Main(string[] args)
        {
            Board board = new Board();
            
            Cell cell1 = new Cell() { X = 0, Y = 0, Edges = new List<Edge>()};
            Cell cell2 = new Cell() { X = 0, Y = 1, Edges = new List<Edge>() };
            Cell cell3 = new Cell() { X = 0, Y = 2, Edges = new List<Edge>() };
            Cell cell4 = new Cell() { X = 1, Y = 0, Edges = new List<Edge>() };
            Cell cell5 = new Cell() { X = 1, Y = 1, Edges = new List<Edge>() };
            Cell cell6 = new Cell() { X = 1, Y = 2, Edges = new List<Edge>() };
            Cell cell7 = new Cell() { X = 2, Y = 0, Edges = new List<Edge>() };
            Cell cell8 = new Cell() { X = 2, Y = 1, Edges = new List<Edge>() };
            Cell cell9 = new Cell() { X = 2, Y = 2, Edges = new List<Edge>() };

            Edge ed1 = new Edge() { Id = 1, IsBlocked = true, Cells = new KeyValuePair<Cell, Cell>(cell1, cell2) };
            Edge ed2 = new Edge() { Id = 2, IsBlocked = false, Cells = new KeyValuePair<Cell, Cell>(cell2, cell3) };
            Edge ed3 = new Edge() { Id = 3, IsBlocked = false, Cells = new KeyValuePair<Cell, Cell>(cell1, cell4) };
            Edge ed4 = new Edge() { Id = 4, IsBlocked = true, Cells = new KeyValuePair<Cell, Cell>(cell2, cell5) };
            Edge ed5 = new Edge() { Id = 5, IsBlocked = false, Cells = new KeyValuePair<Cell, Cell>(cell3, cell6) };
            Edge ed6 = new Edge() { Id = 6, IsBlocked = false, Cells = new KeyValuePair<Cell, Cell>(cell4, cell5) };
            Edge ed7 = new Edge() { Id = 7, IsBlocked = true, Cells = new KeyValuePair<Cell, Cell>(cell5, cell6) };
            Edge ed8 = new Edge() { Id = 8, IsBlocked = false, Cells = new KeyValuePair<Cell, Cell>(cell4, cell7) };
            Edge ed9 = new Edge() { Id = 9, IsBlocked = false, Cells = new KeyValuePair<Cell, Cell>(cell5, cell8) };
            Edge ed10 = new Edge() { Id = 10, IsBlocked = false, Cells = new KeyValuePair<Cell, Cell>(cell6, cell9) };
            Edge ed11 = new Edge() { Id = 11, IsBlocked = false, Cells = new KeyValuePair<Cell, Cell>(cell7, cell8) };
            Edge ed12 = new Edge() { Id = 12, IsBlocked = false, Cells = new KeyValuePair<Cell, Cell>(cell8, cell9) };

            cell1.Edges.Add(ed1);
            cell1.Edges.Add(ed3);

            cell2.Edges.Add(ed1);
            cell2.Edges.Add(ed2); 
            cell2.Edges.Add(ed4);
            
            cell3.Edges.Add(ed2); 
            cell3.Edges.Add(ed5);

            cell4.Edges.Add(ed3);
            cell4.Edges.Add(ed6);
            cell4.Edges.Add(ed8);

            cell5.Edges.Add(ed4);
            cell5.Edges.Add(ed6);
            cell5.Edges.Add(ed7);
            cell5.Edges.Add(ed9);

            cell6.Edges.Add(ed5);
            cell6.Edges.Add(ed7);
            cell6.Edges.Add(ed10);

            cell7.Edges.Add(ed8);
            cell7.Edges.Add(ed11);

            cell8.Edges.Add(ed9);
            cell8.Edges.Add(ed11);
            cell8.Edges.Add(ed12);

            cell9.Edges.Add(ed10);
            cell9.Edges.Add(ed12);

            ITurnCheckService tcheck = new TurnCheckService();
            tcheck.Algoritms = new AStar();
           

        }
    }
}
