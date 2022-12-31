using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode.Y2020
{
    internal class Day16 : GenericDay
    {
        public string Compute1(string[] input, string args)
        {
            (List<Rule> rules, int[] myTicket, List<int[]> otherTickets) = ParseInput(input);

            var res = 0;
            foreach (var otherTicket in otherTickets)
            {
                foreach (var value in otherTicket)
                {
                    if (!rules.Any(x => x.IsInRange(value)))
                    {
                        res += value;
                    }
                }
            }

            return res.ToString();
        }

        public string Compute2(string[] input, string args)
        {
            (List<Rule> rules, int[] myTicket, List<int[]> otherTickets) = ParseInput(input);

            bool IsInvalid(int[] ticket)
            {
                foreach (var value in ticket)
                {
                    if (!rules.Any(x => x.IsInRange(value)))
                    {
                        return false;
                    }
                }
                return true;
            }

            var validTickets = otherTickets.Where(IsInvalid).ToArray();

            var possibles = new List<List<Rule>>();
            for (int i = 0; i < myTicket.Length; i++)
            {
                possibles.Add(rules.ToList());
            }

            // first pass to exclude from each index, every rule that obviously do not match the current value
            foreach (var validTicket in validTickets)
            {
                for (int i = 0; i < validTicket.Length; i++)
                {
                    int j = 0;
                    while (j < possibles[i].Count)
                    {
                        if (!possibles[i][j].IsInRange(validTicket[i]))
                        {
                            possibles[i].RemoveAt(j);
                        }
                        else
                        {
                            j++;
                        }
                    }
                }
            }

            //while there are still some unknown
            while (!possibles.All(x => x.Count == 1))
            {
                var found = possibles.Where(x => x.Count == 1).SelectMany(x => x.Select(y => y.Name)).ToList(); // we get every known rule: index for which there is only one possible rule

                // we filter the possibles by excluding every known rule from index which still has multiple possible rule
                possibles = possibles.Select(x =>
                {
                    if (x.Count == 1) return x;
                    return x.Where(x => !found.Contains(x.Name)).ToList();
                }).ToList();
            }

            var indexRulesDeparture = possibles.Select((elm, idx) => (elm: elm.First(), idx)).Where(x => x.elm.Name.StartsWith("departure")).Select(x => x.idx);

            var res = 1L;
            foreach (var idx in indexRulesDeparture)
            {
                res *= myTicket[idx];
            }

            return res.ToString();
        }

        private static (List<Rule> rules, int[] myTicket, List<int[]> otherTickets) ParseInput(string[] input)
        {
            var lines = input.AsEnumerable().GetEnumerator();
            var rules = new List<Rule>();
            while (lines.MoveNext() && lines.Current != "")
            {
                rules.Add(new Rule(lines.Current));
            }
            lines.MoveNext();
            lines.MoveNext();
            var myTicket = lines.Current.Split(",").Select(x => int.Parse(x)).ToArray();
            lines.MoveNext();
            lines.MoveNext();
            var otherTickets = new List<int[]>();
            while (lines.MoveNext() && lines.Current != "")
            {
                otherTickets.Add(lines.Current.Split(",").Select(x => int.Parse(x)).ToArray());
            }

            return (rules, myTicket, otherTickets);
        }

        private class Rule
        {
            private static readonly Regex regexRule = new Regex(@"([\w ]+): (\d+)-(\d+) or (\d+)-(\d+)");

            public Rule(string input)
            {
                var groups = regexRule.Matches(input)[0].Groups;
                Name = groups[1].Value;
                Ranges = new Range[2];
                Ranges[0] = new Range(int.Parse(groups[2].Value), int.Parse(groups[3].Value));
                Ranges[1] = new Range(int.Parse(groups[4].Value), int.Parse(groups[5].Value));
            }

            public string Name { get; }

            public Range[] Ranges { get; }

            public bool IsInRange(int i)
            {
                return Ranges.Any(x => x.IsInRange(i));
            }

            public override string ToString()
            {
                return Name + ": " + string.Join(" or ", Ranges.AsEnumerable());
            }
        }
    }
}