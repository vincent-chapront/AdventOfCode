using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode.Y2021
{
    internal class Day20 : GenericDay
    {
        public string Compute1(params string[] input)
        {
            return Compute(input, 2).ToString();
        }
        public string Compute2(params string[] input)
        {
            return Compute(input, 50).ToString();
        }
        private static long Compute(string[] input, int maxIteration)
        {
            var enhancer = input[0].Select(x => x == '#' ? 1 : 0).ToArray();

            var grid = input.Skip(2).ToArray().ParseToGrid(x => x == '#' ? 1 : 0);

            var knownPixel = new Dictionary<(int r, int c, int l), int>();
            foreach (var p in grid.GetAllCoordinates())
            {
                knownPixel.Add((p.Row, p.Col, 0), grid[p.Row, p.Col]);
            }

            var res = 0;

            for (int r = -maxIteration; r < maxIteration + grid.GetLength(0); r++)
            {
                for (int c = -maxIteration; c < maxIteration + grid.GetLength(0); c++)
                {
                    if (GetPixel(r, c, maxIteration, knownPixel, enhancer) == 1)
                    {
                        res++;
                    }
                }
            }

            return res;
        }

        private static int GetPixel(int row, int col, int iteration, Dictionary<(int r, int c, int l), int> knownPixel, int[] enhancer)
        {
            if (knownPixel.ContainsKey((row, col, iteration)))
            {
                return knownPixel[(row, col, iteration)];
            }
            if (iteration == 0) return 0;
            var b =
                (GetPixel(row - 1, col - 1, iteration - 1, knownPixel, enhancer) << 8) +
                (GetPixel(row - 1, col + 0, iteration - 1, knownPixel, enhancer) << 7) +
                (GetPixel(row - 1, col + 1, iteration - 1, knownPixel, enhancer) << 6) +
                (GetPixel(row + 0, col - 1, iteration - 1, knownPixel, enhancer) << 5) +
                (GetPixel(row + 0, col + 0, iteration - 1, knownPixel, enhancer) << 4) +
                (GetPixel(row + 0, col + 1, iteration - 1, knownPixel, enhancer) << 3) +
                (GetPixel(row + 1, col - 1, iteration - 1, knownPixel, enhancer) << 2) +
                (GetPixel(row + 1, col + 0, iteration - 1, knownPixel, enhancer) << 1) +
                (GetPixel(row + 1, col + 1, iteration - 1, knownPixel, enhancer) << 0);
            knownPixel.Add((row, col, iteration), enhancer[b]);
            return enhancer[b];
        }
    }
}