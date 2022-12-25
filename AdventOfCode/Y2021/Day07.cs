using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode.Y2021
{
    internal class Day07 : GenericDay
    {

        public string Compute1(params string[] input)
        {
            var v = input[0].Split(",").Select(x => int.Parse(x)).ToList();
            v.Sort();

            var median = v[v.Count / 2];
            var result = int.MaxValue;
            for (int i = -1; i <= 1; i++)
            {
                var cost = v.Select(x => Math.Abs(x - median)).Sum();
                if (cost < result) result = cost;
            }

            return result.ToString();
        }

        public string Compute2(params string[] input)
        {
            static int ComputeCost2Unit(int a, int b)
            {
                int diff = Math.Abs(a - b);
                return (diff * (diff + 1)) / 2;
            }

            var v = input[0].Split(",").Select(x => int.Parse(x)).ToList();
            v.Sort();
            var result = int.MaxValue;

            for (int i = v[0]; i < v[v.Count - 1]; i++)
            {
                var cost = v.Select(x => ComputeCost2Unit(x, i)).Sum();
                if (cost < result) result = cost;
            }

            return result.ToString();
        }
    }
}