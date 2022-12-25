using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Y2015
{
    internal class Day19 : GenericDay
    {
        protected override object Part1()
        {
            Assert.AreEqual(4, Compute1(Resources.Year2015.Day19Test.ToLines(), "HOH"));
            Assert.AreEqual(3, Compute1(Resources.Year2015.Day19Test.ToLines(), "H2O"));
            Assert.AreEqual(7, Compute1(Resources.Year2015.Day19Test.ToLines(), "HOHOHO"));
            var res = Compute1(Resources.Year2015.Day19.ToLines());
            Assert.AreEqual(200, res);
            return res;
        }

        protected override object Part2()
        {
            Assert.AreEqual(3, Compute2(Resources.Year2015.Day19Test.ToLines(), "HOH"));
            Assert.AreEqual(6, Compute2(Resources.Year2015.Day19Test.ToLines(), "HOHOHO"));
            var res = Compute2(Resources.Year2015.Day19.ToLines());
            Assert.AreEqual(-1, res);
            return res;
        }

        public string Compute1(params string[] input)
        {
            if (!string.IsNullOrWhiteSpace(input[^2]))
            {
                throw new InvalidOperationException("Invalid input: empy line expected at Length-2");
            }
            return Compute1(input.Take(input.Length - 2).ToArray(), input[^1]).ToString();
        }

        private static long Compute1(string[] input, string start)
        {
            var knownTransformations = input.Select(x => { var a = x.Split(" => "); return (from: a[0], to: a[1]); }).ToArray();

            var resultingMolecules = new List<string>();

            foreach (var (from, to) in knownTransformations)
            {
                int idx = -1;

                while ((idx = start.IndexOf(from, idx + 1)) > -1)
                {
                    var prefix = start.Substring(0, idx);
                    var suffix = start.Substring(idx + from.Length);

                    string resultingMolecule = prefix + to + suffix;
                    if (!resultingMolecules.Contains(resultingMolecule))
                    {
                        resultingMolecules.Add(resultingMolecule);
                    }
                }
            }

            return resultingMolecules.Count;
        }

        public string Compute2(params string[] input)
        {
            if (!string.IsNullOrWhiteSpace(input[^2]))
            {
                throw new InvalidOperationException("Invalid input: empy line expected at Length-2");
            }
            return Compute2(input.Take(input.Length - 2).ToArray(), input[^1]).ToString();
        }

        private static long Compute2(string[] input, string target)
        {
            var knownTransformations = input.Select(x => { var a = x.Split(" => "); return (from: a[0], to: a[1]); }).OrderByDescending(x => x.to.Length).ThenBy(x => x.from.Length).ToArray();

            var foo = new List<(string target, int step)>();

            foo.Add((target, 0));

            while (foo.Count > 0 && foo[0].target != "e")
            {
                var bar = foo[0];
                foo.RemoveAt(0);

                foreach (var (from, to) in knownTransformations)
                {
                    int idx = -1;

                    while ((idx = bar.target.IndexOf(to, idx + 1)) > -1)
                    {
                        var prefix = bar.target.Substring(0, idx);
                        var suffix = bar.target.Substring(idx + to.Length);

                        string resultingMolecule = prefix + from + suffix;

                        if (foo.Any(x => x.target == resultingMolecule))
                        {
                        }
                        else
                        {
                            foo.Add((resultingMolecule, bar.Item2 + 1));
                        }
                    }
                }

                foo = foo.OrderBy(x => x.target.Length).ThenBy(x => x.Item2).ToList();
            }

            return foo.Count > 0 ? foo[0].Item2 : -1;
        }
    }
}