using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode.Y2020
{
    internal class Day02 : GenericDay
    {
        //protected override object Part1()
        //{
        //    Assert.AreEqual(2, Compute1(Resources.Year2020.Day02Test.ToLines()));
        //    var res = Compute1(Resources.Year2020.Day02.ToLines());
        //    Assert.AreEqual(493, res);
        //    return res;
        //}

        //protected override object Part2()
        //{
        //    Assert.AreEqual(1, Compute2(Resources.Year2020.Day02Test.ToLines()));
        //    var res = Compute2(Resources.Year2020.Day02.ToLines());
        //    Assert.AreEqual(593, res);
        //    return res;
        //}

        public string Compute1(params string[] input)
        {
            bool IsValid(Rule r)
            {
                var count = r.Password.Count(x => x == r.Letter);
                return r.Min <= count && count <= r.Max;
            }

            var rules = input.Select(x => new Rule(x)).ToList();
            return rules.Count(x => IsValid(x)).ToString();
        }

        public string Compute2(params string[] input)
        {
            bool IsValid(Rule r)
            {
                return r.Password[r.Min - 1] == r.Letter ^ r.Password[r.Max - 1] == r.Letter;
            }

            var rules = input.Select(x => new Rule(x)).ToList();
            return rules.Count(x => IsValid(x)).ToString();
        }

        private class Rule
        {
            public Rule(string rule)
            {
                var r = new Regex(@"(\d+)-(\d+) (\w): (\w+)");
                var matches = r.Matches(rule);
                Min = int.Parse(matches[0].Groups[1].Value);
                Max = int.Parse(matches[0].Groups[2].Value);
                Letter = char.Parse(matches[0].Groups[3].Value);
                Password = matches[0].Groups[4].Value;
            }

            public char Letter { get; }
            public int Max { get; }
            public int Min { get; }
            public string Password { get; }
        }
    }
}