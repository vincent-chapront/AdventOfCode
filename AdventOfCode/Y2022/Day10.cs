using System;
using System.Collections.Generic;

namespace AdventOfCode.Y2022
{
    internal class Day10 : GenericDay
    {
        public string Compute1(string[] input)
        {
            int cycle = 1;
            var x = 1;
            var res = 0;

            Action Check = () =>
            {
                int c = cycle + 1;
                if ((c + 20) % 40 == 0)
                {
                    int strength = c * x;
                    res += strength;
                }
            };

            foreach (var line in input)
            {
                if (line == "noop")
                {
                    Check();
                }
                else if (line.StartsWith("addx"))
                {
                    var val = Convert.ToInt32(line.Split(" ")[1]);
                    Check();
                    cycle++;
                    x += val;
                    Check();
                }
                    cycle++;
            }
            return res.ToString();
        }

        public string Compute2(string[] input)
        {
            int cycle = 1;
            var x = 1;
            Console.WriteLine();
            var currentRow = "";
            List<string> rows = new List<string>();
            Action Check = () =>
            {
                var c = (cycle - 1) % 40;
                if (x - 1 <= c && c <= x + 1)
                {
                    currentRow += "▓";
                }
                else
                {
                    currentRow += " ";
                }
                if (cycle % 40 == 0)
                {
                    Console.WriteLine(currentRow);
                    rows.Add(currentRow);
                    currentRow = "";
                }
            };

            foreach (var line in input)
            {
                if (line == "noop")
                {
                    Check();
                }
                else if (line.StartsWith("addx"))
                {
                    var val = Convert.ToInt32(line.Split(" ")[1]);

                    Check();

                    cycle++;

                    Check();
                    x += val;
                }

                cycle++;
            }
            Console.WriteLine();
            var res = string.Join("-", rows);
            return res;
        }
    }
}