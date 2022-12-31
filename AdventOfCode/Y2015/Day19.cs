using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Y2015
{
    internal class Day19 : GenericDay
    {
        public string Compute1(string[] input, string args)
        {
            var res = Process1(input.Take(input.Length - 2).ToArray(), input[^1]);
            return res.ToString();
        }

        public string Compute2(string[] input, string args)
        {
            var res = Process2(input.Take(input.Length - 2).ToArray(), input[^1]);
            return res.ToString();
        }

        private static long Process1(string[] input, string start)
        {
            var knownTransformations = input.Select(x => { var a = x.Split(" => "); return (from: a[0], to: a[1]); }).ToArray();

            var resultingMolecules = new List<string>();

            foreach (var (from, to) in knownTransformations)
            {
                int idx = -1;

                while ((idx = start.IndexOf(from, idx + 1)) > -1)
                {
                    var prefix = start[..idx];
                    var suffix = start[(idx + from.Length)..];

                    string resultingMolecule = prefix + to + suffix;
                    if (!resultingMolecules.Contains(resultingMolecule))
                    {
                        resultingMolecules.Add(resultingMolecule);
                    }
                }
            }

            return resultingMolecules.Count;
        }

        private static long Process2(string[] input, string target)
        {
            var knownTransformations = input.Select(x => { var a = x.Split(" => "); return (from: a[0], to: a[1]); }).OrderByDescending(x => x.to.Length).ThenBy(x => x.from.Length).ToArray();

            var foo = new List<(string target, int step)> { (target, 0) };

            while (foo.Count > 0 && foo[0].target != "e")
            {
                var bar = foo[0];
                foo.RemoveAt(0);

                foreach (var (from, to) in knownTransformations)
                {
                    int idx = -1;

                    while ((idx = bar.target.IndexOf(to, idx + 1)) > -1)
                    {
                        var prefix = bar.target[..idx];
                        var suffix = bar.target[(idx + to.Length)..];

                        string resultingMolecule = prefix + from + suffix;

                        if (!foo.Any(x => x.target == resultingMolecule))
                        {
                            foo.Add((resultingMolecule, bar.step + 1));
                        }
                    }
                }

                foo = foo.OrderBy(x => x.target.Length).ThenBy(x => x.step).ToList();
            }

            return foo.Count > 0 ? foo[0].step : -1;
        }
    }
}