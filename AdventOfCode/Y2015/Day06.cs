using System;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode.Y2015
{
    internal class Day06 : GenericDay
    {
        public string Compute1(string[] input, string args)
        {
            Regex r = new Regex(@"(turn on|toggle|turn off) ([\d,]*) through ([\d,]*)");
            var map = new bool[1000, 1000];
            foreach (var l in input)
            {
                var g = r.Match(l).Groups;
                var start = g[2].Value.Split(",").Select(x => int.Parse(x)).ToArray();
                var end = g[3].Value.Split(",").Select(x => int.Parse(x)).ToArray();
                for (int i = start[0]; i <= end[0]; i++)
                {
                    for (int j = start[1]; j <= end[1]; j++)
                    {
                        if (g[1].Value == "turn on")
                        {
                            map[i, j] = true;
                        }
                        else if (g[1].Value == "toggle")
                        {
                            map[i, j] = !map[i, j];
                        }
                        else if (g[1].Value == "turn off")
                        {
                            map[i, j] = false;
                        }
                    }
                }
            }
            var res = 0;
            for (int i = 0; i < 1000; i++)
            {
                for (int j = 0; j < 1000; j++)
                {
                    if (map[i, j]) res++;
                }
            }
            return res.ToString();
        }

        public string Compute2(string[] input, string args)
        {
            Regex r = new Regex(@"(turn on|toggle|turn off) ([\d,]*) through ([\d,]*)");
            var map = new long[1000, 1000];
            foreach (var l in input)
            {
                var g = r.Match(l).Groups;
                var start = g[2].Value.Split(",").Select(x => int.Parse(x)).ToArray();
                var end = g[3].Value.Split(",").Select(x => int.Parse(x)).ToArray();
                for (int i = start[0]; i <= end[0]; i++)
                {
                    for (int j = start[1]; j <= end[1]; j++)
                    {
                        if (g[1].Value == "turn on")
                        {
                            map[i, j]++;
                        }
                        else if (g[1].Value == "toggle")
                        {
                            map[i, j] += 2;
                        }
                        else if (g[1].Value == "turn off")
                        {
                            map[i, j] = Math.Max(map[i, j] - 1, 0);
                        }
                    }
                }
            }
            long res = 0;
            for (int i = 0; i < 1000; i++)
            {
                for (int j = 0; j < 1000; j++)
                {
                    res += (map[i, j]);
                }
            }
            return res.ToString();
        }
    }
}