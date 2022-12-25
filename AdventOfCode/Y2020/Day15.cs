using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode.Y2020
{
    internal class Day15 : GenericDay
    {
        public string Compute1(params string[] input)
        {
            return Run(input, 2020).ToString();
        }

        public string Compute2(params string[] input)
        {
            return Run(input, 30000000).ToString();
        }

        private static long Run(string[] input, int iteration)
        {
            long lastValue = 0;

            var dico = new Dictionary<long, (long lastPosition, long previousPos)>();
            List<int> l = input[0].Split(",").Select(x => int.Parse(x)).ToList();
            for (int i = 0; i < l.Count; i++)
            {
                dico.Add(l[i], (i + 1, -1));
                lastValue = l[i];
            }

            for (int i = l.Count + 1; i <= iteration; i++)
            {
                var v = dico[lastValue];
                long newValue = 0;
                if (v.previousPos != -1)
                {
                    newValue = v.lastPosition - v.previousPos;
                }
                if (!dico.ContainsKey(newValue))
                {
                    dico.Add(newValue, (-1, -1));
                }
                lastValue = newValue;
                dico[newValue] = (i, dico[newValue].lastPosition);
            }

            return lastValue;
        }
    }
}