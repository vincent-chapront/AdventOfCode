using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode.Y2021
{
    internal class Day04 : GenericDay
    {
        private static Bingo BuildBingo(string[] input)
        {
            var numbers = input[0].Split(",").Select(x => int.Parse(x)).ToArray();
            var grid = new Cell[5, 5];
            var grids = new List<Grid>();

            for (int i = 2; i < input.Length; i += 6)
            {
                for (int j = 0; j < 5; j++)
                {
                    var row = input[i + j].Trim().Replace("  ", " ").Split(" ").Select(x => int.Parse(x)).Select(x => new Cell(x));
                    var k = 0;
                    foreach (var cell in row)
                    {
                        grid[j, k] = cell;
                        k++;
                    }
                }
                grids.Add(new Grid(grid));
                grid = new Cell[5, 5];
            }

            return new Bingo(grids, numbers);
        }

        private static int CalcResult(int number, Grid grid)
        {
            int sumNotChecked = 0;
            for (int i = 0; i < grid.Cells.GetLength(0); i++)
            {
                for (int j = 0; j < grid.Cells.GetLength(1); j++)
                {
                    if (!grid.Cells[i, j].IsChecked)
                    {
                        sumNotChecked += grid.Cells[i, j].Value;
                    }
                }
            }
            return sumNotChecked * number;
        }

        public string Compute1(params string[] input)
        {
            var bingo = BuildBingo(input);

            var (number, grid) = FindFirstValid(bingo);
            if (grid != null)
            {
                return CalcResult(number, grid).ToString();
            }
            return "ERROR";
        }

        public string Compute2(params string[] input)
        {
            var bingo = BuildBingo(input);

            var (number, grid) = FindLastValid(bingo);
            if (grid != null)
            {
                return CalcResult(number, grid).ToString();
            }

            return "ERROR";
        }

        private static (int number, Grid grid) FindFirstValid(Bingo bingo)
        {
            foreach (var number in bingo.Numbers)
            {
                foreach (var grid in bingo.Grid)
                {
                    grid.CheckNumber(number);
                    if (grid.IsValid())
                    {
                        return (number, grid);
                    }
                }
            }
            return (int.MinValue, null);
        }

        private static (int number, Grid grid) FindLastValid(Bingo bingo)
        {
            var invalidGrids = bingo.Grid.Count;
            foreach (var number in bingo.Numbers)
            {
                foreach (var grid in bingo.Grid)
                {
                    if (grid.IsValid())
                    {
                        continue;
                    }

                    grid.CheckNumber(number);
                    if (grid.IsValid())
                    {
                        invalidGrids--;
                        if (invalidGrids == 0)
                        {
                            return (number, grid);
                        }
                    }
                }
            }
            return (int.MinValue, null);
        }

        public class Bingo
        {
            public Bingo(List<Grid> grid, int[] numbers)
            {
                Grid = grid;
                Numbers = numbers;
            }

            public List<Grid> Grid { get; }
            public int[] Numbers { get; }
        }

        public class Cell
        {
            public Cell(int value)
            {
                Value = value;
            }

            public bool IsChecked { get; set; }
            public int Value { get; }

            public override string ToString()
            {
                return Value + "(" + (IsChecked ? "X" : " ") + ")";
            }
        }

        public class Grid
        {
            public Grid(Cell[,] cells)
            {
                Cells = cells;
            }

            public Cell[,] Cells { get; }

            public void CheckNumber(int value)
            {
                for (int i = 0; i < Cells.GetLength(0); i++)
                {
                    for (int j = 0; j < Cells.GetLength(1); j++)
                    {
                        if (Cells[i, j].Value == value)
                        {
                            Cells[i, j].IsChecked = true;
                        }
                    }
                }
            }

            public bool IsValid()
            {
                for (int i = 0; i < 5; i++)
                {
                    if (IsRowValid(i)) return true;
                    if (IsColumnValid(i)) return true;
                }
                return false;
            }

            private bool IsColumnValid(int i)
            {
                return Cells[i, 0].IsChecked
                    && Cells[i, 1].IsChecked
                    && Cells[i, 2].IsChecked
                    && Cells[i, 3].IsChecked
                    && Cells[i, 4].IsChecked;
            }

            private bool IsRowValid(int i)
            {
                return Cells[0, i].IsChecked
                    && Cells[1, i].IsChecked
                    && Cells[2, i].IsChecked
                    && Cells[3, i].IsChecked
                    && Cells[4, i].IsChecked;
            }
        }
    }
}