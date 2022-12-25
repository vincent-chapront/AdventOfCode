using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace AdventOfCode.Y2017
{
    internal class Day05 : GenericDay
    {
        public string Compute1(params string[] input)
        {
            var program = input.Select(int.Parse).ToArray();
            var step = 0;
            var idx = 0;
            while (0 <= idx && idx < input.Length)
            {
                var jump = program[idx];
                program[idx]++;
                idx += jump;
                step++;
            }
            return step.ToString();
        }

        public string Compute2(params string[] input)
        {
            throw new NotImplementedException();
        }
    }
}