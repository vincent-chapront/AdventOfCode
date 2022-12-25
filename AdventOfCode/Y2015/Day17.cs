using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode.Y2015
{
    internal class Day17 : GenericDay
    {
        protected override object Part1()
        {
            Assert.AreEqual(4, Compute1(Resources.Year2015.Day17Test.ToLines(), 25));
            var res = Compute1(Resources.Year2015.Day17.ToLines(), 150);
            Assert.AreEqual(1304, res);
            return res;
        }

        protected override object Part2()
        {
            Assert.AreEqual(3, Compute2(Resources.Year2015.Day17Test.ToLines(), 25));
            var res = Compute2(Resources.Year2015.Day17.ToLines(), 150);
            Assert.AreEqual(18, res);
            return res;
        }

        private static long Compute1(string[] input, int volume)
        {
            var containers = input.Select(x => int.Parse(x)).OrderByDescending(x => x).ToList();
            List<int> res = new List<int>();
            Generate(containers, 0, volume, 0, 0, res);
            return res.Count;
        }

        private static long Compute2(string[] input, int volume)
        {
            var containers = input.Select(x => int.Parse(x)).OrderByDescending(x => x).ToList();
            List<int> res = new List<int>();
            Generate(containers, 0, volume, 0, 0, res);
            var min = res.Min();
            return res.Count(x => x == min);
        }

        private static void Generate(List<int> containers, int n, int targetVolume, int currentVolume, int numberOfContainerUsed, List<int> res)
        {
            if (n == containers.Count)
            {
                if (currentVolume == targetVolume)
                {
                    res.Add(numberOfContainerUsed);
                }
                return;
            }
            Generate(containers, n + 1, targetVolume, currentVolume + containers[n], numberOfContainerUsed + 1, res);
            Generate(containers, n + 1, targetVolume, currentVolume, numberOfContainerUsed, res);
        }
    }
}