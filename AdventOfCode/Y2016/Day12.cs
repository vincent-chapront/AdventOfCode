using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode.Y2016
{
    internal class Day12 : GenericDay
    {
        private static long Compute(string[] input, Dictionary<string, int> registry)
        {
            var pointer = 0;
            while (pointer < input.Length)
            {
                var line = input[pointer];
                var parts = line.Split(' ');
                var incPointer = 1;
                if (parts[0] == "cpy")
                {
                    if (int.TryParse(parts[1], out int x))
                    {
                        registry[parts[2]] = x;
                    }
                    else
                    {
                        registry[parts[2]] = registry[parts[1]];
                    }
                }
                else if (parts[0] == "inc")
                {
                    registry[parts[1]]++;
                }
                else if (parts[0] == "dec")
                {
                    registry[parts[1]]--;
                }
                else
                {
                    if (int.TryParse(parts[1], out int x))
                    {
                        if (x != 0)
                        {
                            incPointer = int.Parse(parts[2]);
                        }
                    }
                    else
                    {
                        if (registry[parts[1]] != 0)
                        {
                            incPointer = int.Parse(parts[2]);
                        }
                    }
                }
                pointer += incPointer;
            }
            return registry["a"];
        }

        public string Compute1(params string[] input)
        {
            var registry = new Dictionary<string, int>
            {
                {"a",0},
                {"b",0},
                {"c",0},
                {"d",0}
            };
            return Compute(input, registry).ToString();
        }

        public string Compute2(params string[] input)
        {
            var registry = new Dictionary<string, int>
            {
                {"a",0},
                {"b",0},
                {"c",1},
                {"d",0}
            };
            return Compute(input, registry).ToString();
        }
    }
}