using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Y2015
{
    internal class Day18 : GenericDay
    {
        public string Compute1(string[] input, string args)
        {
            var steps = Convert.ToInt32(args);
            var grid = GetGrid(input);
            var size = grid.GetLength(0);
            for (int i = 0; i < steps; i++)
            {
                grid = NextStep(grid, size);
            }

            return CountOn(grid, size).ToString();
        }

        public string Compute2(string[] input, string args)
        {
            var steps = Convert.ToInt32(args);
            var grid = GetGrid(input);
            var size = grid.GetLength(0);
            grid[0, 0] = true;
            grid[size - 1, 0] = true;
            grid[0, size - 1] = true;
            grid[size - 1, size - 1] = true;
            for (int i = 0; i < steps; i++)
            {
                grid = NextStep(grid, size);
                grid[0, 0] = true;
                grid[size - 1, 0] = true;
                grid[0, size - 1] = true;
                grid[size - 1, size - 1] = true;
            }

            return CountOn(grid, size).ToString();
        }

        private static long CountOn(bool[,] grid, int size)
        {
            var res = 0;
            for (int j = 0; j < size; j++)
            {
                for (int i = 0; i < size; i++)
                {
                    if (grid[i, j])
                    {
                        res++;
                    }
                }
            }
            return res;
        }

        private static IEnumerable<Point2d> GetAllNeighboors(Point2d p)
        {
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (i != 0 || j != 0)
                        yield return new Point2d(p.Row + i, p.Col + j);
                }
            }
        }

        private static bool[,] GetGrid(string[] input)
        {
            var size = input.Length;

            var grid = new bool[size, size];
            int y = 0;
            foreach (var line in input)
            {
                int x = 0;
                foreach (var lamp in line)
                {
                    grid[x, y] = lamp != '.';
                    x++;
                }
                y++;
            }

            return grid;
        }

        private static bool[,] NextStep(bool[,] grid, int size)
        {
            var newGrid = new bool[size, size];
            for (int j = 0; j < size; j++)
            {
                for (int i = 0; i < size; i++)
                {
                    var countOn = GetAllNeighboors(new Point2d(i, j)).Where(x => x.Row >= 0 && x.Col >= 0 && x.Row < size && x.Col < size).Count(x => grid[x.Row, x.Col]);

                    if (grid[i, j] && countOn != 2 && countOn != 3)
                    {
                        newGrid[i, j] = false;
                    }
                    else if (!grid[i, j] && countOn == 3)
                    {
                        newGrid[i, j] = true;
                    }
                    else
                    {
                        newGrid[i, j] = grid[i, j];
                    }
                }
            }
            return newGrid;
        }
    }
}