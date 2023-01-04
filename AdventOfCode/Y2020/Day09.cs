using System;
using System.Linq;
using AdventOfCode.Properties;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode.Y2020
{
    internal class Day09 : GenericDay
    {
        public string Compute1(string[] input, string args)
        {
            var preambleSize = Convert.ToInt32(args);
            return Process1(input, preambleSize).ToString();
        }
        public string Compute2(string[] input, string args)
        {
            var preambleSize = Convert.ToInt32(args);
            return Process2(input, preambleSize).ToString();
        }

        private static long Process1(string[] input, int preambleSize)
        {
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

        private static long Process2(string[] input, int preambleSize)
        {
            var search = Process1(input, preambleSize);
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