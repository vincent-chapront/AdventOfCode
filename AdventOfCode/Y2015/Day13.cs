using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode.Y2015
{
    internal class Day13 : GenericDay
    {
        public string Compute1(params string[] input)
        {
            var v = input.Select(Parse).ToList();

            return GetMaxHappiness(ref v).ToString();
        }

        public string Compute2(params string[] input)
        {
            var v = input.Select(Parse).ToList();
            var guests = v.Select(x => x.from).Distinct().ToList();
            foreach (var guest in guests)
            {
                v.Add((guest, "ME", 0));
                v.Add(("ME", guest, 0));
            }

            return GetMaxHappiness(ref v).ToString();
        }

        private static long GetMaxHappiness(ref List<(string from, string to, int value)> v)
        {
            List<List<string>> combinations = v.Select(x => x.from).Distinct().GetCombinations();
            for (int i = 0; i < v.Count - 1; i++)
            {
                var a = v[i];

                for (int j = i + 1; j < v.Count; j++)
                {
                    var b = v[j];

                    if (a.from == b.to && a.to == b.from)
                    {
                        v[i] = (a.from, a.to, a.value + b.value);
                        //v[i].value += b.value;
                        v.RemoveAt(j);
                        break;
                    }
                }
            }
            v = v.OrderByDescending(x => x.value).ToList();

            var maxHappiness = int.MinValue;
            foreach (var combination in combinations)
            {
                var totalHappiness = 0;
                for (int i = 0; i < combination.Count; i++)
                {
                    int j = (i + 1) % combination.Count;
                    totalHappiness +=
                        v
                        .Where(v =>
                            (v.from == combination[i] && v.to == combination[j])
                            || (v.from == combination[j] && v.to == combination[i])
                        )
                        .Select(x => x.value)
                        .Sum();
                    ;
                    ;
                }
                maxHappiness = Math.Max(maxHappiness, totalHappiness);
            }

            return maxHappiness;
        }

        private static (string from, string to, int value) Parse(string s)
        {
            var regex = new Regex(@"([A-Za-z]+) would (gain|lose) (\d+) happiness units by sitting next to ([A-Za-z]+).");

            var result = regex.Match(s);

            var value = (result.Groups[2].Value == "gain" ? 1 : -1) * int.Parse(result.Groups[3].Value);

            return (result.Groups[1].Value, result.Groups[4].Value, value);
        }
    }
}