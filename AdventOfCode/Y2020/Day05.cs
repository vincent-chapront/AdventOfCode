using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode.Y2020
{
    internal class Day05 : GenericDay
    {
        private static IEnumerable<int> Compute(string[] input)
        {
            Func<string, (int row, int col)> parse = s =>
            {
                int minRow = 0;
                int maxRow = 127;
                foreach (var c in s.Substring(0, 7))
                {
                    (minRow, maxRow) = SubDivide(minRow, maxRow, c);
                }
                int minCol = 0;
                int maxCol = 7;
                foreach (var c in s.Substring(7, 3))
                {
                    (minCol, maxCol) = SubDivide(minCol, maxCol, c);
                }
                return (minRow, minCol);
            };
            Func<int, int, int> computeId = (int row, int col) =>
            {
                return row * 8 + col;
            };
            return input.Select(parse).Select(x => computeId(x.row, x.col));
        }

        public string Compute1(string[] input, string args)
        {
            return Compute(input).Max().ToString();
        }

        public string Compute2(string[] input, string args)
        {
            var filledIds = Compute(input).ToList();
            filledIds.Sort();
            for (int i = 1; i < filledIds.Count - 1; i++)
            {
                if (filledIds[i] != filledIds[i - 1] + 1 || filledIds[i] != filledIds[i + 1] - 1)
                {
                    return (filledIds[i + 1] - 1).ToString();
                }
            }
            return "ERROR";
        }

        private static (int min, int max) SubDivide(int min, int max, char c)
        {
            if (c == 'F' || c == 'L')
            {
                max = (min + max) / 2;
            }
            else
            {
                min = ((min + max) / 2) + 1;
            }
            return (min, max);
        }
    }
}