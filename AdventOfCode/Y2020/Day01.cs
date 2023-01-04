using System.Linq;

namespace AdventOfCode.Y2020
{
    internal class Day01 : GenericDay
    {
        public string Compute1(string[] input, string args)
        {
            return Solve(input.Select(x => int.Parse(x)).ToArray(), 2);
        }

        public string Compute2(string[] input, string args)
        {
            return Solve(input.Select(x => int.Parse(x)).ToArray(), 3);
        }

        private static string Solve(int[] input, int Size)
        {
            foreach (var combinaison in input.CombinaisonDistinct(Size))
            {
                if (combinaison.Sum() == 2020)
                {
                    return combinaison.Product().ToString();
                }
            }
            return "ERROR";
        }
    }
}