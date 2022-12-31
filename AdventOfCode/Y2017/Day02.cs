using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace AdventOfCode.Y2017
{
    internal class Day02 : GenericDay
    {
        //protected override object Part1()
        //{
        //    Assert.AreEqual(18, Compute1(Resources.Year2017File.Day02Test.ToLines()));
        //    var res = Compute1(Resources.Year2017File.Day02.ToLines());
        //    Assert.AreEqual(41887, res);
        //    return res;
        //}

        //protected override object Part2()
        //{
        //    Assert.AreEqual(9, Compute2(Resources.Year2017File.Day02Test2.ToLines()));
        //    var res = Compute2(Resources.Year2017File.Day02.ToLines());
        //    Assert.AreEqual(226, res);
        //    return res;
        //}

        public string Compute1(string[] input, string args)
        {
            long res = 0;
            foreach (var line in input)
            {
                var min = int.MaxValue;
                var max = int.MinValue;
                var a = line.Split('\t');
                foreach (var n in line.Split('\t').Select(x => int.Parse(x)))
                {
                    if (n < min)
                    {
                        min = n;
                    }
                    if (n > max)
                    {
                        max = n;
                    }
                }
                res += max - min;
            }
            return res.ToString();
        }

        public string Compute2(string[] input, string args)
        {
            long res = 0;
            foreach (var line in input)
            {
                foreach (var pair in line.Split('\t').Select(x => int.Parse(x)).GetAllPairs())
                {
                    var min = pair.Item1;
                    var max = pair.Item2;
                    if (min > max)
                    {
                        var a = min;
                        min = max;
                        max = a;
                    }
                    if (max % min == 0)
                    {
                        res += max / min;
                        break;
                    }
                }
            }
            return res.ToString();
        }
    }
}