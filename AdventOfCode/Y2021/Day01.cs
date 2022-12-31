using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
        //protected override object Part1()
        //{
        //    Assert.AreEqual(7, Compute(Resources.Year2021.Day01Test.ToLines(), 1));

        //    var res = Compute(Resources.Year2021.Day01.ToLines(), 1);
        //    Assert.AreEqual(1557, res);
        //    return res;
        //}

        //protected override object Part2()
        //{
        //    Assert.AreEqual(1608, Compute(Resources.Year2021.Day01.ToLines(), 3));

        //    long res = Compute(Resources.Year2021.Day01Test.ToLines(), 3);
        //    Assert.AreEqual(5, res);
        //    return res;
        //}

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