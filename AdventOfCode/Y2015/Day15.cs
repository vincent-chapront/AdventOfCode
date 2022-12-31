using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode.Y2015
{
    internal class Day15 : GenericDay
    {
        public string Compute1(string[] input, string args)
        {
            var ingredients = input.Select(x => new Ingredient(x)).ToList();
            var n = ingredients.Count;

            List<List<int>> r = new List<List<int>>();
            Generate(r, n);

            int max = int.MinValue;
            foreach (var qty in r)
            {
                int capacity = 0;
                int durability = 0;
                int flavor = 0;
                int texture = 0;
                int calories = 0;
                for (int i = 0; i < qty.Count; i++)
                {
                    capacity += ingredients[i].capacity * qty[i];
                    durability += ingredients[i].durability * qty[i];
                    flavor += ingredients[i].flavor * qty[i];
                    texture += ingredients[i].texture * qty[i];
                    calories += ingredients[i].calories * qty[i];
                }
                capacity = Math.Max(capacity, 0);
                durability = Math.Max(durability, 0);
                flavor = Math.Max(flavor, 0);
                texture = Math.Max(texture, 0);
                calories = Math.Max(calories, 0);

                var total = capacity * durability * flavor * texture;
                max = Math.Max(max, total);
            }
            return max.ToString();
        }

        public string Compute2(string[] input, string args)
        {
            var ingredients = input.Select(x => new Ingredient(x)).ToList();
            var n = ingredients.Count;

            List<List<int>> r = new List<List<int>>();
            Generate(r, n);

            int max = int.MinValue;
            foreach (var qty in r)
            {
                int capacity = 0;
                int durability = 0;
                int flavor = 0;
                int texture = 0;
                int calories = 0;
                for (int i = 0; i < qty.Count; i++)
                {
                    capacity += ingredients[i].capacity * qty[i];
                    durability += ingredients[i].durability * qty[i];
                    flavor += ingredients[i].flavor * qty[i];
                    texture += ingredients[i].texture * qty[i];
                    calories += ingredients[i].calories * qty[i];
                }
                capacity = Math.Max(capacity, 0);
                durability = Math.Max(durability, 0);
                flavor = Math.Max(flavor, 0);
                texture = Math.Max(texture, 0);
                calories = Math.Max(calories, 0);
                if (calories == 500)
                {
                    var total = capacity * durability * flavor * texture;
                    max = Math.Max(max, total);
                }
            }
            return max.ToString();
        }

        private static void Generate(List<List<int>> result, int n, params int[] currentValue)
        {
            if (n <= 0)
            {
                if (currentValue.Sum() == 100)
                {
                    result.Add(new List<int>(currentValue));
                }
                return;
            }
            foreach (var i in GeneratePossibleNumbers(currentValue.Sum()))
            {
                var newValues = new int[currentValue.Length + 1];
                for (int j = 0; j < newValues.Length - 1; j++)
                {
                    newValues[j] = currentValue[j];
                }
                newValues[newValues.Length - 1] = i;
                Generate(result, n - 1, newValues);
            }
        }

        private static IEnumerable<int> GeneratePossibleNumbers(int currentSum)
        {
            for (int i = 100 - currentSum; i >= 0; i--)
            {
                yield return i;
            }
        }

        private class Ingredient
        {
            public int calories;
            public int capacity;
            public int durability;
            public int flavor;
            public string name;
            public int texture;

            public Ingredient(string line)
            {
                // Cinnamon: capacity 2, durability 3, flavor -2, texture -1, calories 3
                string reg = @"([a-zA-Z]+): capacity ([\d\-]+), durability ([\d\-]+), flavor ([\d\-]+), texture ([\d\-]+), calories ([\d\-]+)";
                var match = Regex.Match(line, reg);
                name = match.Groups[1].ToString();
                this.capacity = int.Parse(match.Groups[2].ToString());
                this.durability = int.Parse(match.Groups[3].ToString());
                this.flavor = int.Parse(match.Groups[4].ToString());
                this.texture = int.Parse(match.Groups[5].ToString());
                this.calories = int.Parse(match.Groups[6].ToString());
            }
        }
    }
}