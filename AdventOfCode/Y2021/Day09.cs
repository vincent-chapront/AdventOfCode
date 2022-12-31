using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode.Y2021
{
    internal class Day09 : GenericDay
    {
        public string Compute1(string[] input, string args)
        {
            var grid = input.ParseToGrid(x => int.Parse(x.ToString()));

            var res =
                grid.GetAllCoordinates()
                .Where(x => IsLowPoint(grid, x.Row, x.Col))
                .Select(x => grid[x.Row, x.Col])
                .Sum(x => x + 1);

            return res.ToString();
        }

        public string Compute2(string[] input, string args)
        {
            var grid = input.ParseToGrid(x => int.Parse(x.ToString()));

            var lowPoints =
                grid.GetAllCoordinates()
                .Where(x => IsLowPoint(grid, x.Row, x.Col))
                .ToList();

            List<int> sizeBassin = new List<int>();
            foreach (var lowPoint in lowPoints)
            {
                int size = 0;
                var queue = new Queue<(int row, int col)>();
                queue.Enqueue((lowPoint.Row, lowPoint.Col));
                var gridClone = grid.MyClone();

                while (queue.Count > 0)
                {
                    size++;
                    var currentPoint = queue.Dequeue();
                    var surroundings = GetSurroundingCoordinage(gridClone, currentPoint.row, currentPoint.col);

                    foreach (var surrounding in surroundings)
                    {
                        if (gridClone[surrounding.row, surrounding.col] != 9 && !queue.Contains(surrounding))
                        {
                            queue.Enqueue(surrounding);
                        }
                    }
                    gridClone[currentPoint.row, currentPoint.col] = 9;
                }
                sizeBassin.Add(size);
            }

            return sizeBassin.OrderByDescending(x => x).Take(3).Product().ToString();
        }

        private static int[] GetSurrounding(int[,] grid, int row, int col)
        {
            var r = new List<int>(4);
            if (row > 0) r.Add(grid[row - 1, col]);
            if (col > 0) r.Add(grid[row, col - 1]);
            if (row < grid.GetLength(0) - 1) r.Add(grid[row + 1, col]);
            if (col < grid.GetLength(1) - 1) r.Add(grid[row, col + 1]);

            return r.ToArray();
        }

        private static (int row, int col)[] GetSurroundingCoordinage(int[,] grid, int row, int col)
        {
            var r = new List<(int, int)>(4);
            if (row > 0) r.Add((row - 1, col));
            if (col > 0) r.Add((row, col - 1));
            if (row < grid.GetLength(0) - 1) r.Add((row + 1, col));
            if (col < grid.GetLength(1) - 1) r.Add((row, col + 1));

            return r.ToArray();
        }

        private static bool IsLowPoint(int[,] grid, int row, int column)
        {
            var val = grid[row, column];
            var sur = GetSurrounding(grid, row, column).ToList();
            var res = sur.All(x => x > val);
            return res;
        }
    }
}