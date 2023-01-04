using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Y2021
{
    internal class Day06 : GenericDay
    {
        public string Compute1(string[] input, string args)
        {
            return Compute(input, 80).ToString();
        }

        public string Compute2(string[] input, string args)
        {
            return Compute(input, 256).ToString();
        }

        private static long Compute(string[] input, int iterationNumbers)
        {
            var initialAges = input[0].Split(",").Select(x => int.Parse(x)).ToList();
            return CountFishes(initialAges, iterationNumbers);
        }

        private static long CountFishes(List<int> ages, int days)
        {
            var numberPerAge = new long[9];
            foreach (var a in ages)
            {
                numberPerAge[a]++;
            }

            for (int i = 0; i < days; i++)
            {
                var buffer = new long[9];
                buffer[0] += numberPerAge[1];
                buffer[1] += numberPerAge[2];
                buffer[2] += numberPerAge[3];
                buffer[3] += numberPerAge[4];
                buffer[4] += numberPerAge[5];
                buffer[5] += numberPerAge[6];
                buffer[6] += numberPerAge[0] + numberPerAge[7];
                buffer[7] += numberPerAge[8];
                buffer[8] += numberPerAge[0];
                numberPerAge = buffer;
            }
            return numberPerAge.Sum();
        }
    }
}