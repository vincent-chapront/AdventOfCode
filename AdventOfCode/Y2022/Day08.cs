using System;
using System.Linq;

namespace AdventOfCode.Y2022
{
    internal class Day08 : GenericDay
    {
        public string Compute1(string[] input)
        {
            int[,] grid = Parse(input);
            var res = 0;
            var cs = grid.GetAllCoordinates().ToList();
            foreach (var c in cs)
            {
                if (c.Row == 0 || c.Col == 0 || c.Row == grid.GetLength(0) - 1 || c.Col == grid.GetLength(1) - 1)
                {
                    res++;
                    continue;
                }
                var isVisibleLeft = true;
                for (int i = 0; i < c.Col; i++)
                {
                    if (grid[c.Row, i] >= grid[c.Row, c.Col])
                    {
                        isVisibleLeft = false;
                        break;
                    }
                }
                var isVisibleRight = true;
                for (int i = grid.GetLength(1) - 1; i > c.Col; i--)
                {
                    if (grid[c.Row, i] >= grid[c.Row, c.Col])
                    {
                        isVisibleRight = false;
                        break;
                    }
                }
                var isVisibleTop = true;
                for (int i = 0; i < c.Row; i++)
                {
                    if (grid[i, c.Col] >= grid[c.Row, c.Col])
                    {
                        isVisibleTop = false;
                        break;
                    }
                }
                var isVisibleBottom = true;
                for (int i = grid.GetLength(0) - 1; i > c.Row; i--)
                {
                    if (grid[i, c.Col] >= grid[c.Row, c.Col])
                    {
                        isVisibleBottom = false;
                        break;
                    }
                }

                if (isVisibleLeft || isVisibleRight || isVisibleTop || isVisibleBottom)
                {
                    res++;
                }
            }

            return res.ToString();
        }

        public string Compute2(string[] input)
        {
            int[,] grid = Parse(input);
            var res = 0;
            var cs = grid.GetAllCoordinates().ToList();
            foreach (var c in cs)
            {
                var visibleLeft = 0;
                var visibleRight = 0;
                var visibleTop = 0;
                var visibleBottom = 0;

                var tallest = 0;
                for (int i = c.Col + 1; i < grid.GetLength(0); i++)
                {
                    visibleRight++;
                    if (grid[c.Row, i] >= grid[c.Row, c.Col])
                    {
                        break;
                    }
                    tallest = Math.Max(tallest, grid[c.Row, i]);
                }

                tallest = 0;
                for (int i = c.Col - 1; i >= 0; i--)
                {
                    visibleLeft++;
                    if (grid[c.Row, i] >= grid[c.Row, c.Col])
                    {
                        break;
                    }
                    tallest = Math.Max(tallest, grid[c.Row, i]);
                }

                tallest = 0;
                for (int i = c.Row + 1; i < grid.GetLength(1); i++)
                {
                    visibleBottom++;
                    if (grid[i, c.Col] >= grid[c.Row, c.Col])
                    {
                        break;
                    }
                    tallest = Math.Max(tallest, grid[i, c.Col]);
                }

                tallest = 0;
                for (int i = c.Row - 1; i >= 0; i--)
                {
                    visibleTop++;
                    if (grid[i, c.Col] >= grid[c.Row, c.Col])
                    {
                        break;
                    }
                    tallest = Math.Max(tallest, grid[i, c.Col]);
                }

                res = Math.Max(res, visibleTop * visibleBottom * visibleLeft * visibleRight);
            }

            return res.ToString();
        }

        private static int[,] Parse(string[] input)
        {
            int[,] grid = new int[input.Length, input[0].Length];

            for (int i = 0; i < input.Length; i++)
            {
                string line = input[i];
                for (int j = 0; j < line.Length; j++)
                {
                    grid[i, j] = Convert.ToInt32(line[j] - '0');
                }
            }

            return grid;
        }
    }
}