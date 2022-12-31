using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode.Y2015
{
    internal class Day02 : GenericDay
    {
        //protected override object Part1()
        //{
        //    Assert.AreEqual(58, ComputePaper("2x3x4"));
        //    Assert.AreEqual(43, ComputePaper("1x1x10"));
        //    var res = Compute1(Resources.Year2015.Day02.ToLines());
        //    Assert.AreEqual(1606483, res);
        //    return res;
        //}

        //protected override object Part2()
        //{
        //    Assert.AreEqual(34, ComputeRibbon("2x3x4"));
        //    Assert.AreEqual(14, ComputeRibbon("1x1x10"));
        //    var res = Compute2(Resources.Year2015.Day02.ToLines());
        //    Assert.AreEqual(3842356, res);
        //    return res;
        //}

        public string Compute1(string[] input, string args)
        {
            return input.Select(x => ComputePaper(x)).Sum().ToString();
        }

        public string Compute2(string[] input, string args)
        {
            return input.Select(x => ComputeRibbon(x)).Sum().ToString();
        }

        private static long ComputePaper(string input)
        {
            var d = input.Split('x').Select(x => int.Parse(x)).ToArray();
            var area1 = d[0] * d[1];
            var area2 = d[1] * d[2];
            var area3 = d[2] * d[0];
            var min = Min(area1, area2, area3);

            var res = 2 * (area1 + area2 + area3) + min;
            return res;
        }

        private static long ComputeRibbon(string input)
        {
            var d = input.Split('x').Select(x => int.Parse(x)).ToArray();
            var perimeter1 = 2 * d[0] + 2 * d[1];
            var perimeter2 = 2 * d[1] + 2 * d[2];
            var perimeter3 = 2 * d[2] + 2 * d[0];

            var bow = d[0] * d[1] * d[2];

            return Min(perimeter1, perimeter2, perimeter3) + bow;
        }

        private static long Min(params long[] values)
        {
            return values.Min();
        }
    }
}