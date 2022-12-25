using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode.Y2020
{
    internal class Day10 : GenericDay
    {
        private static Dictionary<int, long> memoization = new Dictionary<int, long>();

        public string Compute1(params string[] input)
        {
            var l = input.Select(x => int.Parse(x)).ToList();
            l.Sort();
            var prev = 0;
            var diff1 = 0;
            var diff3 = 1;
            for (int i = 0; i < l.Count; i++)
            {
                int diff = l[i] - prev;
                if (diff == 1) diff1++;
                else if (diff == 3) diff3++;
                prev = l[i];
            }
            return (diff1 * diff3).ToString();
        }

        public string Compute2(params string[] input)
        {
            var l = input.Select(x => int.Parse(x)).ToList();
            l.Sort();
            memoization = new Dictionary<int, long>();
            var finalRes = CountCombination2(l, 0);

            return finalRes.ToString();
        }

        private static long CountCombination2(List<int> input, int value)
        {
            if (memoization.ContainsKey(value))
            {
                return memoization[value];
            }

            var validValues = input.Where(x => 1 <= x - value && x - value <= 3).ToList();
            if (validValues.Count == 0)
            {
                memoization[value] = 1;
                return 1;
            }
            var sum = 0L;
            foreach (var validValue in validValues)
            {
                sum += CountCombination2(input, validValue);
            }
            memoization[value] = sum;
            return sum;
        }
    }
}