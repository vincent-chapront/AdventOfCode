using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;

namespace A_star
{
    internal class A_star
    {
        // Class Astar, which finds the shortest path
        public class Astar
        {
            // The array of the cells
            public Cell[,] cells;

            // The list of the closed cells
            public SortedList<int, Coordinates> closed = new SortedList<int, Coordinates>();

            public bool debugMode = false;

            // The end of the searched path
            public Coordinates finishCell;

            // The list of the opened cells
            public SortedList<int, Coordinates> opened = new SortedList<int, Coordinates>();

            // The possible path found
            public SortedList<int, Coordinates> path = new SortedList<int, Coordinates>();

            private readonly Stopwatch sw = new Stopwatch();
            private int maxOpened = 0;
            private int size;

            // The constructor
            public Astar(Coordinates start, Coordinates end, Cell[,] cells, bool debugMode)
            {
                this.debugMode = debugMode;
                finishCell = end;
                this.cells = cells;
                size = cells.GetLength(0);

                Display(cells);
                if (debugMode)
                {
                    Console.WriteLine("Start");
                    Console.ReadKey();
                }

                // Adding the start cell on the list opened
                opened.Add(start.row * size + start.col, start);

                // Boolean value which indicates if a path is found
                Boolean pathFound = false;

                // Loop until the list opened is empty or a path is found
                int iterationNumber = 1;
                var dtStart = DateTime.Now;
                var nfi = (NumberFormatInfo)CultureInfo.InvariantCulture.NumberFormat.Clone();
                nfi.NumberGroupSeparator = " ";
                Stopwatch swTotal = new Stopwatch();
                swTotal.Start();
                do
                {
                    /*if (iterationNumber > 20000000)
                    {
                        var dtEnd = DateTime.Now;
                        var span = dtEnd - dtStart;
                        Console.WriteLine("    " + iterationNumber.ToString("0000") + " - " + span.Seconds);
                        throw new NotImplementedException();
                        //Console.ReadKey();
                    }*/
                    if (iterationNumber % 100_000_000 == 0)
                    {
                        Console.WriteLine(iterationNumber.ToString("#,0", nfi) + " - " + maxOpened + " - " + swTotal.Elapsed.TotalSeconds / 60);
                    }
                    iterationNumber++;

                    // The next cell analyzed
                    Coordinates currentCell = ShorterExpectedPath();
                    // The list of cells reachable from the actual one
                    foreach (Coordinates newCell in neighborsCells(currentCell))
                    {
                        // If the cell considered is the final one
                        if (newCell.row == finishCell.row && newCell.col == finishCell.col)
                        {
                            cells[newCell.row, newCell.col].g = cells[currentCell.row, currentCell.col].g + cells[newCell.row, newCell.col].cost;
                            cells[newCell.row, newCell.col].parent.row = currentCell.row;
                            cells[newCell.row, newCell.col].parent.col = currentCell.col;
                            pathFound = true;
                            break;
                        }

                        // If the cell considered is not between the open and closed ones
                        else if (!ContainsCoordinates(opened, newCell) && !ContainsCoordinates(closed, newCell))
                        {
                            UpdateCell(cells, currentCell, newCell);
                            SetCell(newCell, opened);
                        }

                        // If the cost to reach the considered cell from the actual one is
                        // smaller than the previous one
                        else if (cells[newCell.row, newCell.col].g > cells[currentCell.row, currentCell.col].g + cells[newCell.row, newCell.col].cost)
                        {
                            UpdateCell(cells, currentCell, newCell);
                            SetCell(newCell, opened);
                            ResetCell(newCell, closed);
                        }
                    }
                    SetCell(currentCell, closed);
                    ResetCell(currentCell, opened);
                } while (opened.Count > 0 && !pathFound);

                Console.WriteLine(iterationNumber.ToString("#,0", nfi) + " - " + maxOpened + " - " + DateTime.Now.ToString("HH:mm:ss") + " - " + swTotal.Elapsed.TotalSeconds + " - " + sw.Elapsed.TotalSeconds);
                if (pathFound)
                {
                    path.Add(finishCell.row * size + finishCell.col, finishCell);
                    Coordinates currentCell = new Coordinates(finishCell.row, finishCell.col);
                    // It reconstructs the path starting from the end
                    while (cells[currentCell.row, currentCell.col].parent.row >= 0)
                    {
                        path.Add(currentCell.row * size + currentCell.col, cells[currentCell.row, currentCell.col].parent);
                        int tmp_row = cells[currentCell.row, currentCell.col].parent.row;
                        currentCell.col = cells[currentCell.row, currentCell.col].parent.col;
                        currentCell.row = tmp_row;
                    }

                    Display(cells);

                    if (debugMode)
                    {
                        // Printing the coordinates of the cells of the path
                        Console.Write("\nPath: ");
                        for (int i = path.Count - 1; i >= 0; i--)
                        {
                            Console.Write("({0},{1})", path[i].row, path[i].col);
                        }

                        // Printing the cost of the path
                        Console.WriteLine("\nPath cost: {0}", path.Count - 1);

                        // Waiting to the key Enter to be pressed to end the program
                        String wt = Console.ReadLine();
                    }
                }
            }

            public bool ContainsCoordinates(SortedList<int, Coordinates> sortedList, Coordinates cell)
            {
                return ContainsCoordinates(sortedList, cell.row, cell.col);
            }

            public bool ContainsCoordinates(SortedList<int, Coordinates> sortedList, int row, int col)
            {
                return sortedList.ContainsKey(row * size + col);
            }

            // The function Heuristic, which determines the shortest path that a 'king' can do
            // This is the maximum value between the orizzontal distance and the vertical one
            public int Heuristic(Coordinates cell)
            {
                int dRow = Math.Abs(finishCell.row - cell.row);
                int dCol = Math.Abs(finishCell.col - cell.col);
                return Math.Max(dRow, dCol);
            }

            // It finds che cells that could be reached from c
            public IEnumerable<Coordinates> neighborsCells(Coordinates c)
            {
                if (c.row > 0)
                {
                    yield return (new Coordinates(c.row - 1, c.col));
                }

                if (c.row + 1 < size)
                {
                    yield return (new Coordinates(c.row + 1, c.col));
                }

                if (c.col > 0)
                {
                    yield return (new Coordinates(c.row, c.col - 1));
                }

                if (c.col + 1 < size)
                {
                    yield return (new Coordinates(c.row, c.col + 1));
                }
            }

            // It removes the coordinates of cell from a list, if it's already present
            public void ResetCell(Coordinates cell, SortedList<int, Coordinates> coordinatesList)
            {
                coordinatesList.RemoveAt(cell.row * size + cell.col);
            }

            // It inserts the coordinates of cell in a list, if it's not already present
            public void SetCell(Coordinates cell, SortedList<int, Coordinates> coordinatesList)
            {
                if (!ContainsCoordinates(coordinatesList, cell))
                {
                    coordinatesList.Add(cell.row * size + cell.col, cell);
                }
            }

            // It select the cell between those in the list opened that have the smaller
            // value of f
            public Coordinates ShorterExpectedPath()
            {
                if (opened.Count > maxOpened)
                {
                    maxOpened = opened.Count;
                }
                sw.Start();
                int sep = opened.Keys[0];
                var cell = cells[opened[sep].row, opened[sep].col].f;
                if (opened.Count > 1)
                {
                    foreach (var i in opened.Keys.Skip(1))
                    {
                        if (cells[opened[i].row, opened[i].col].f < cell)
                        {
                            sep = i;
                            cell = cells[opened[sep].row, opened[sep].col].f;
                        }
                    }
                }
                sw.Stop();
                return opened[sep];
            }

            private void Display(Cell[,] cells)
            {
                if (!debugMode)
                {
                    return;
                }
                System.Console.WriteLine();
                // Printing on the screen the 'chessboard' and the path found
                for (int i = 0; i < size; i++)
                {
                    for (int j = 0; j < size; j++)
                    {
                        // Symbol for a cell that doesn't belong to the path and isn't
                        // a wall
                        char gr = '.';
                        // Symbol for a cell that belongs to the path
                        if (ContainsCoordinates(path, new Coordinates(i, j)))
                        {
                            Console.BackgroundColor = ConsoleColor.White;
                            Console.ForegroundColor = ConsoleColor.Black;
                        }
                        else
                        {
                            Console.BackgroundColor = ConsoleColor.Black;
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        Console.Write(cells[i, j].cost);
                    }
                    Console.WriteLine();
                }
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;
            }

            private void UpdateCell(Cell[,] cells, Coordinates currentCellCoordinate, Coordinates newCellCoordinate)
            {
                Cell newCell = cells[newCellCoordinate.row, newCellCoordinate.col];
                Cell currentCell = cells[currentCellCoordinate.row, currentCellCoordinate.col];
                int heuristic = Heuristic(newCellCoordinate);

                newCell.UpdateCell(currentCellCoordinate, currentCell, heuristic);
            }
        }

        // Class Cell, with the cost to reach it, the values g and f, and the coordinates
        // of the cell that precedes it in a possible path
        public class Cell
        {
            public Coordinates Coordinates;
            public int cost;
            public int f;
            public int g;
            public Coordinates parent;

            public Cell(Coordinates coordinate)
            {
                Coordinates = coordinate;
            }

            public void UpdateCell(Coordinates currentCellCoordinate, Cell currentCell, int heuristic)
            {
                g = currentCell.g + cost;
                f = g + heuristic;
                parent.row = currentCellCoordinate.row;
                parent.col = currentCellCoordinate.col;
            }
        }

        // Coordinates of a cell - implements the method Equals
        public class Coordinates : IEquatable<Coordinates>
        {
            public int col;
            public int row;

            public Coordinates()
            { row = -1; col = -1; }

            public Coordinates(int row, int col)
            { this.row = row; this.col = col; }

            public bool Equals(Coordinates c)
            {
                return row == c.row && col == c.col;
            }
        }
    }
}