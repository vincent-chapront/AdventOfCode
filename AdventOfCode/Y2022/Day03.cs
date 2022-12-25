using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode.Y2022
{
    internal class Day03 : GenericDay
    {
        //protected override object Part1()
        //{
        //    Assert.AreEqual(157, Compute1(Resources.Year2022.Day03Test.ToLines()));
        //    var res = Compute1(Resources.Year2022.Day03.ToLines());
        //    Assert.AreEqual(8088, res);
        //    return res;
        //}

        //protected override object Part2()
        //{
        //    Assert.AreEqual(70, Compute2(Resources.Year2022.Day03Test.ToLines()));
        //    var res = Compute2(Resources.Year2022.Day03.ToLines());
        //    Assert.AreEqual(2522, res);
        //    return res;
        //}

        public string Compute1(string[] input)
        {
            var sum = 0;
            foreach(var line in input)
            {
                var left = line.Substring(0, line.Length / 2);
                var right = line.Substring(line.Length / 2);
                var a = left.Intersect(right).First();
                sum += CalcPriority(a);
            }
            return sum.ToString();
        }

        private static int CalcPriority(char c)
        {
            var priority = 0;
            if (char.IsLower(c))
            {
                priority = c - 'a' + 1;
            }
            else
            {
                priority = c - 'A' + 27;
            }

            return priority;
        }

        public string Compute2(string[] input)
        {
            var sum = 0;
            for (int i = 0; i < input.Length; i+=3)
            {
                var g1=input[i];
                var g2=input[i+1];
                var g3=input[i+2];
                var intersect1=g1.Intersect(g2).ToList();
                var intersect2=g2.Intersect(g3).ToList();
                var intersect3= intersect1.Intersect(intersect2).First();
                sum += CalcPriority(intersect3);
            }
            return sum.ToString();
        }
    }
}