using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode.Y2020
{
    internal class Day21 : GenericDay
    {
        public string Compute1(string[] input, string args)
        {
            Parse(input);

            throw new NotImplementedException();
        }
        public string Compute2(string[] input, string args)
        {
            throw new NotImplementedException();
        }
        private void Parse(string[] input)
        {
            foreach (var line in input)
            {
                var ingredients = line.Split(" (")[0].Split(" ");
                var allergens = line.Split(" (")[1].Replace("contains", "").Replace(" ", "").Replace(")", "").Split(",");
            }
        }
    }
}