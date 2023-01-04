using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Y2020
{
    internal class Day02 : GenericDay
    {
        public string Compute1(string[] input, string args)
        {
            bool IsValid(Rule r)
            {
                var count = r.Password.Count(x => x == r.Letter);
                return r.Min <= count && count <= r.Max;
            }

            var rules = input.Select(x => new Rule(x)).ToList();
            return rules.Count(x => IsValid(x)).ToString();
        }

        public string Compute2(string[] input, string args)
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