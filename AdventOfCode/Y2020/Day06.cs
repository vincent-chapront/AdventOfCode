using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode.Y2020
{
    internal class Day06 : GenericDay
    {
        public string Compute1(string[] input, string args)
        {
            return Parse(input).Select(x => x.Item1.Distinct().Count()).Sum().ToString();
        }

        public string Compute2(string[] input, string args)
        {
            return
                Parse(input)
                .Select(y =>
                    y.Item1.GroupBy(x => x).ToDictionary(x => x, x => x.Count())
                    .Count(x => x.Value == y.Item2)
                )
                .Sum().ToString();
        }

        private static List<(List<char>, int)> Parse(string[] input)
        {
            var l1 = new List<(List<char>, int)>();
            List<char> l = new List<char>();
            var numberMember = 0;
            foreach (var i in input)
            {
                if (!string.IsNullOrWhiteSpace(i))
                {
                    l.AddRange(i.ToArray());
                    numberMember++;
                }
                else
                {
                    l1.Add((l, numberMember));
                    l = new List<char>();
                    numberMember = 0;
                }
            }
            l1.Add((l, numberMember));
            return l1;
        }
    }
}