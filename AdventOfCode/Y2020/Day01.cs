using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode.Y2020
{
    internal class Day01 : GenericDay
    {
    //    protected override object Part1()
    //    {
    //        Assert.AreEqual(514579, Compute1(Resources.Year2020.Day01Test.ToLines()));
    //        var res = Compute1(Resources.Year2020.Day01.ToLines());
    //        Assert.AreEqual(842016, res);
    //        return res;
    //    }

    //    protected override object Part2()
    //    {
    //        Assert.AreEqual(241861950, Compute2(Resources.Year2020.Day01Test.ToLines()));
    //        var res = Compute2(Resources.Year2020.Day01.ToLines());
    //        Assert.AreEqual(9199664, res);
    //        return res;
    //    }

        public string Compute1(string[] input, string args)
        {
            return Solve(input.Select(x => int.Parse(x)).ToArray(), 2);
        }

        public string Compute2(string[] input, string args)
        {
            return Solve(input.Select(x => int.Parse(x)).ToArray(), 3);
        }

        private static string Solve(int[] input, int Size)
        {
            foreach (var combinaison in input.CombinaisonDistinct(Size))
            {
                if (combinaison.Sum() == 2020)
                {
                    return combinaison.Product().ToString();
                }
            }
            return "ERROR";
        }
    }
}