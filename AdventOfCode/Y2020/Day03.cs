using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode.Y2020
{
    internal class Day03 : GenericDay
    {
        public string Compute1(string[] input, string args)
        {
            return CountCollisions(input, 3, 1).ToString();
        }

        public string Compute2(string[] input, string args)
        {
            var c1 = CountCollisions(input, 1, 1);
            var c2 = CountCollisions(input, 3, 1);
            var c3 = CountCollisions(input, 5, 1);
            var c4 = CountCollisions(input, 7, 1);
            var c5 = CountCollisions(input, 1, 2);
            return (c1 * c2 * c3 * c4 * c5).ToString();
        }

        private static long CountCollisions(string[] input, int sX, int sY)
        {
            int x = 0;
            int y = 0;
            long result = 0;
            int width = input[0].Length;
            while (y < input.Length)
            {
                if (input[y][x] == '#')
                {
                    result++;
                }
                x = (x + sX) % width;
                y += sY;
            }
            return result;
        }
    }
}