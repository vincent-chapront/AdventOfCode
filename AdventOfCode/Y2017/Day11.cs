using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AdventOfCode.Y2017
{
    internal class Day11 : GenericDay
    {
        public string Compute1(params string[] input)
        {
            var ns = 0;
            var ew = 0;
            foreach (var step in input[0].Split(','))
            {
                if (step.Contains("n"))
                {
                    ns++;
                }
                else if (step.Contains("s"))
                {
                    ns--;
                }
                else if (step.Contains("e"))
                {
                    ew++;
                }
                else if (step.Contains("w"))
                {
                    ew--;
                }
            }
            return (Math.Abs(ns) + Math.Abs(ew)).ToString();
        }

        public string Compute2(params string[] input)
        {
            throw new NotImplementedException();
        }
    }
}