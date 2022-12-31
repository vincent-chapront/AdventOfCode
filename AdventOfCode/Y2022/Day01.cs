using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode.Y2022
{
    internal class Day01 : GenericDay
    {
        public string Compute1(string[] input, string args)
        {
            List<int> calories = GetCalories(input);
            return calories.Max().ToString();
        }

        public string Compute2(string[] input, string args)
        {
            List<int> calories = GetCalories(input);
            return calories.OrderByDescending(x => x).Take(3).Sum().ToString();
        }

        private static List<int> GetCalories(string[] input)
        {
            var calories = new List<int>();
            var current = 0;
            foreach (var line in input)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    calories.Add(current);
                    current = 0;
                }
                else
                {
                    current += int.Parse(line);
                }
            }
            calories.Add(current);

            return calories;
        }
    }

}