using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Y2015
{
    internal class Day17 : GenericDay
    {
        public string Compute1(string[] input, string args)
        {
            var volume = Convert.ToInt32(args);
            var containers = input.Select(x => int.Parse(x)).OrderByDescending(x => x).ToList();
            var res = new List<int>();
            Generate(containers, 0, volume, 0, 0, res);
            return res.Count.ToString();
        }

        public string Compute2(string[] input, string args)
        {
            var volume = Convert.ToInt32(args);
            var containers = input.Select(x => int.Parse(x)).OrderByDescending(x => x).ToList();
            var res = new List<int>();
            Generate(containers, 0, volume, 0, 0, res);
            var min = res.Min();
            return res.Count(x => x == min).ToString();
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