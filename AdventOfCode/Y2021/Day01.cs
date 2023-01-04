using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Y2021
{
    internal class Day01 : GenericDay
    {
        public string Compute1(string[] input, string args)
        {
            return Compute(input, 1);
        }

        public string Compute2(string[] input, string args)
        {
            return Compute(input, 3);
        }

        private static string Compute(string[] lines, int windowSize)
        {
            return CountIncreases(lines.Select(x => int.Parse(x)).ToArray(), windowSize).ToString();
        }

        private static int CountIncreases(int[] input, int windowSize)
        {
            int result = 0;
            var oldValue = int.MaxValue;
            foreach (var newValue in SlidingSum(input, windowSize))
            {
                if (oldValue < newValue)
                {
                    result++;
                }
                oldValue = newValue;
            }
            return result;
        }

        private static IEnumerable<int> SlidingSum(int[] input, int windowSize)
        {
            foreach (var v in SlidingWindow(input, windowSize))
            {
                yield return v.Sum();
            }
        }

        private static IEnumerable<IEnumerable<int>> SlidingWindow(int[] input, int windowSize)
        {
            for (int i = 0; i <= input.Length - windowSize; i++)
            {
                yield return input.Skip(i).Take(windowSize);
            }
        }
    }
}