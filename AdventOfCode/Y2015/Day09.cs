using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode.Y2015
{
    internal class Day09 : GenericDay
    {
        private static List<(List<string> node, int distance)> Compute(string[] input)
        {
            var v = input.Select(x => new Line(x)).ToList();
            var nodes = new List<string>();
            foreach (var line in v)
            {
                if (!nodes.Contains(line.From))
                {
                    nodes.Add(line.From);
                }
                if (!nodes.Contains(line.To))
                {
                    nodes.Add(line.To);
                }
            }

            var combinations = GetCombinations(nodes);

            var result = new List<(List<string> node, int distance)>();
            foreach (var combination in combinations)
            {
                var currentDistance = 0;
                for (int i = 0; i < combination.Count - 1; i++)
                {
                    var d = v.FirstOrDefault(x => (x.From == combination[i] && x.To == combination[i + 1]) || (x.From == combination[i + 1] && x.To == combination[i]));
                    ;
                    currentDistance += d.Distance;
                }
                result.Add((combination, currentDistance));
            }

            return result;
        }

        public string Compute1(params string[] input)
        {
            return Compute(input).Min(x => x.distance).ToString();
        }

        public string Compute2(params string[] input)
        {
            return Compute(input).Max(x => x.distance).ToString();
        }

        private static List<List<T>> GetCombinations<T>(List<T> elements, List<T> current = null)
        {
            if (current == null)
            {
                current = new List<T>();
            }

            if (elements.Count == 0)
            {
                return new List<List<T>> { new List<T>(current) };
            }

            var result = new List<List<T>>();
            for (int i = 0; i < elements.Count; i++)
            {
                current.Add(elements[i]);
                var subCombination = GetCombinations(elements.Where((x, idx) => idx != i).ToList(), current);
                result.AddRange(subCombination);
                current.RemoveAt(current.Count - 1);
            }

            return result;
        }

        private class Line
        {
            public Line(string text)
            {
                var v = text.Replace(" to ", ";").Replace(" = ", ";").Split(";");
                From = v[0];
                To = v[1];
                Distance = int.Parse(v[2]);
            }

            public int Distance { get; set; }
            public string From { get; set; }
            public string To { get; set; }

            public override string ToString()
            {
                return $"{From}->{To}:{Distance}";
            }
        }
    }
}