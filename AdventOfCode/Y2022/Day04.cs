using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode.Y2022
{
    internal class Day04 : GenericDay
    {
        //protected override object Part1()
        //{
        //    Assert.AreEqual(2, Compute1(Resources.Year2022.Day04Test.ToLines()));
        //    var res = Compute1(Resources.Year2022.Day04.ToLines());
        //    Assert.AreEqual(485, res);
        //    return res;
        //}

        //protected override object Part2()
        //{
        //    Assert.AreEqual(4, Compute2(Resources.Year2022.Day04Test.ToLines()));
        //    var res = Compute2(Resources.Year2022.Day04.ToLines());
        //    Assert.AreEqual(857, res);
        //    return res;
        //}

        public string Compute1(string[] input)
        {
            var count = 0;
            foreach(var line in input)
            {
                var a = line.Split(',').Select(x => x.Split('-').Select(x=>int.Parse(x)).ToList()).ToList();
                if (
                    (a[0][0] <= a[1][0] && a[0][1] >= a[1][1])
                    || (a[1][0] <= a[0][0] && a[1][1] >= a[0][1])
                    )
                {
                    count++;
                }
            }
            return count.ToString();
        }

        public string Compute2(string[] input)
        {
            var count = 0;
            foreach (var line in input)
            {
                var a = line.Split(',').Select(x => x.Split('-').Select(x => int.Parse(x)).ToList()).ToList();
                if (
                    (a[0][0] <= a[1][0] && a[0][1] >= a[1][0])
                    ||(a[0][0] <= a[1][1] && a[0][1] >= a[1][1])
                    || (a[1][0] <= a[0][0] && a[1][1] >= a[0][1])
                    )
                {
                    count++;
                }
            }
            return count.ToString();
        }
    }
}