using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode.Y2020
{
    internal class Day09 : GenericDay
    {
        protected override object Part1()
        {
            Assert.AreEqual(127, Compute1(Resources.Year2020.Day09Test.ToLines(), 5));
            var res = Compute1(Resources.Year2020.Day09.ToLines(), 25);
            Assert.AreEqual(530627549, res);
            return res;
        }

        protected override object Part2()
        {
            Assert.AreEqual(62, Compute2(Resources.Year2020.Day09Test.ToLines(), 5));
            var res = Compute2(Resources.Year2020.Day09.ToLines(), 25);
            Assert.AreEqual(77730285, res);
            return res;
        }

        private static long Compute1(string[] input, int preambleSize)
        {
            //var preambleSize = isTest?5:25;
            var l = input.Select(x => long.Parse(x)).ToList();

            for (int i = preambleSize + 1; i < l.Count; i++)
            {
                bool isValid = false;
                for (int j = 1; j <= preambleSize && !isValid; j++)
                {
                    long diff = l[i] - l[i - j];
                    if (l.Skip(i - preambleSize + 1).Take(preambleSize).Contains(diff))
                    {
                        isValid = true;
                    }
                }
                if (!isValid)
                {
                    return l[i];
                }
            }
            return int.MaxValue;
        }

        private static long Compute2(string[] input, int preambleSize)
        {
            var search = Compute1(input, preambleSize);
            var l = input.Select(x => long.Parse(x)).ToList();

            var startIndex = 0;
            var startSize = 2;
            var found = false;
            while (!found)
            {
                var b = l.Skip(startIndex).Take(startSize);
                var a = b.Sum();
                if (a == search)
                {
                    found = true;
                    return b.Min() + b.Max();
                }
                if (a > search || startIndex + startSize > l.Count)
                {
                    startIndex++;
                    startSize = 2;
                }
                if (startIndex >= l.Count)
                {
                    return int.MaxValue;
                }
                startSize++;
            }
            return int.MaxValue;
        }
    }
}