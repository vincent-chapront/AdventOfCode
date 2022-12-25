using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode.Y2022
{
    internal class Day06 : GenericDay
    {
        //protected override object Part1()
        //{
        //    Assert.AreEqual(7, Compute1("mjqjpqmgbljsphdztnvjfqwrcgsmlb"));
        //    Assert.AreEqual(5, Compute1("bvwbjplbgvbhsrlpgdmjqwftvncz"));
        //    Assert.AreEqual(6, Compute1("nppdvjthqldpwncqszvftbrmjlhg"));
        //    Assert.AreEqual(10, Compute1("nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg"));
        //    Assert.AreEqual(11, Compute1("zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw"));
        //    var res = Compute1(Resources.Year2022.Day06);
        //    Assert.AreEqual(1343, res);
        //    return res;
        //}

        //protected override object Part2()
        //{
        //    Assert.AreEqual(19, Compute2("mjqjpqmgbljsphdztnvjfqwrcgsmlb"));
        //    Assert.AreEqual(23, Compute2("bvwbjplbgvbhsrlpgdmjqwftvncz"));
        //    Assert.AreEqual(23, Compute2("nppdvjthqldpwncqszvftbrmjlhg"));
        //    Assert.AreEqual(29, Compute2("nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg"));
        //    Assert.AreEqual(26, Compute2("zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw"));
        //    var res = Compute2(Resources.Year2022.Day06);
        //    Assert.AreEqual(2193, res);
        //    return res;
        //}

        private static long Compute(string input, int n)
        {
            var prevChar = input.Substring(0, n - 1).ToList();
            int i;
            for (i = n - 1; i < input.Length; i++)
            {
                prevChar.Add(input[i]);
                if (prevChar.Distinct().Count() == prevChar.Count)
                {
                    break;
                }
                prevChar.RemoveAt(0);
            }
            return i + 1;
        }

        public string Compute1(params string[] input)
        {
            return Compute(input[0], 4).ToString();
        }

        public string Compute2(params string[] input)
        {
            return Compute(input[0], 14).ToString();
        }
    }
}