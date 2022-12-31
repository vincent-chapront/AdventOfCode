using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;

namespace AdventOfCode.Y2017
{
    internal class Day08 : GenericDay
    {
        public string Compute1(string[] input, string args)
        {
            return Compute(input).endMax.ToString();
        }
        public string Compute2(string[] input, string args)
        {
            return Compute(input).totalMax.ToString();
        }
        private static (long endMax, long totalMax) Compute(string[] input)
        {
            //b inc 5 if a > 1
            var dico = new Dictionary<string, int>();
            var totalMax = int.MinValue;
            foreach (var line in input)
            {
                var tokens = line.Split(" ");
                var dest = tokens[0];
                if (!dico.ContainsKey(dest))
                {
                    dico.Add(dest, 0);
                }
                var sign = tokens[1] == "inc" ? 1 : -1;
                var modifier = sign * int.Parse(tokens[2]);

                var compareSource = tokens[4];
                if (!dico.ContainsKey(compareSource))
                {
                    dico.Add(compareSource, 0);
                }
                var compareValue = int.Parse(tokens[6]);
                switch (tokens[5])
                {
                    case ">":
                        if (dico[compareSource] > compareValue)
                            dico[dest] += modifier;
                        break;
                    case ">=":
                        if (dico[compareSource] >= compareValue)
                            dico[dest] += modifier;
                        break;
                    case "<":
                        if (dico[compareSource] < compareValue)
                            dico[dest] += modifier;
                        break;
                    case "<=":
                        if (dico[compareSource] <= compareValue)
                            dico[dest] += modifier;
                        break;
                    case "==":
                        if (dico[compareSource] == compareValue)
                            dico[dest] += modifier;
                        break;
                    case "!=":
                        if (dico[compareSource] != compareValue)
                            dico[dest] += modifier;
                        break;
                    default:
                        break;
                }
                if (dico[dest] > totalMax)
                {
                    totalMax = dico[dest];
                }
            }
            return (dico.Select(x=>x.Value).Max(), totalMax);
        }
    }
}