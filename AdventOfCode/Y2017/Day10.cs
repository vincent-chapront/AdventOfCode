using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Y2017
{
    internal class Day10 : GenericDay
    {
        protected override object Part1()
        {
            Assert.AreEqual(12, Compute1(5, "3,4,1,5"));
            var res = Compute1(256, Resources.Year2017File.Day10);
            return res;
        }

        protected override object Part2()
        {
            Assert.AreEqual("3efbe78a8d82f29979031a4aa0b16a9d", Compute2("1,2,3"));
            Assert.AreEqual("a2582a3a0e66e6e86e3812dcb672a272", Compute2(""));
            Assert.AreEqual("33efeb34ea91902bb2f59c9920caa6cd", Compute2("AoC 2017"));
            Assert.AreEqual("63960835bcdc130f0b66d7ff4f6a5a8e", Compute2("1,2,4"));
            var res = Compute2(Resources.Year2017File.Day10);
            Assert.AreEqual("899124dac21012ebc32e2f4d11eaec55", res);
            return res;
        }

        private static long Compute1(int n, string input)
        {
            var lengths = input.Split(",").Select(int.Parse);
            var a = Enumerable.Range(0, n).ToArray();
            a = KnotHash.CalcStep(lengths, a);
            return a[0] * a[1];
        }

        

        private static string Compute2(string input)
        {
            return KnotHash.CalcAsString(input);
        }

    }
}