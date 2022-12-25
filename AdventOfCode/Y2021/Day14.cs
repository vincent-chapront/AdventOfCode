using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode.Y2021
{
    internal class Day14 : GenericDay
    {
        protected override object Part1()
        {
            Assert.AreEqual(1, Compute(Resources.Year2021.Day14Test.ToLines(), 0));
            Assert.AreEqual(1, Compute(Resources.Year2021.Day14Test.ToLines(), 1));
            Assert.AreEqual(5, Compute(Resources.Year2021.Day14Test.ToLines(), 2));
            Assert.AreEqual(7, Compute(Resources.Year2021.Day14Test.ToLines(), 3));
            Assert.AreEqual(1588, Compute(Resources.Year2021.Day14Test.ToLines(), 10));

            var res = Compute(Resources.Year2021.Day14.ToLines(), 10);
            Assert.AreEqual(2621, res);

            return res;
        }

        protected override object Part2()
        {
            Assert.AreEqual(2188189693529, Compute(Resources.Year2021.Day14Test.ToLines(), 40));

            long res = Compute(Resources.Year2021.Day14.ToLines(), 40);
            Assert.AreEqual(2843834241366, res);

            return res;
        }

        private static long Compute(string[] input, int iterationNumber)
        {
            (Dictionary<char, long> dictionary, Dictionary<string, long> template, Dictionary<string, char> dictionaryInsertion) =
                Parse(input);

            for (int i = 0; i < iterationNumber; i++)
            {
                template = Iteration(template, dictionary, dictionaryInsertion);
            }

            return dictionary.Max(x => x.Value) - dictionary.Min(x => x.Value);
        }

        private static Dictionary<string, long> Iteration(Dictionary<string, long> template, Dictionary<char, long> dictionaryCount, Dictionary<string, char> dictionaryInsertion)
        {
            Dictionary<string, long> result = new Dictionary<string, long>();
            foreach (var kvp in template)
            {
                if (dictionaryInsertion.ContainsKey(kvp.Key))
                {
                    var insertion = dictionaryInsertion[kvp.Key];

                    result.AddOrIncrement($"{kvp.Key[0]}{insertion}", kvp.Value);
                    result.AddOrIncrement($"{insertion}{kvp.Key[1]}", kvp.Value);
                    dictionaryCount.AddOrIncrement(insertion, kvp.Value);
                }
                else
                {
                    result.AddOrIncrement($"{kvp.Key[0]}{kvp.Key[1]}");
                }
            }

            return result;
            /*var sb = new StringBuilder();
            sb.Append(template[0]);
            for (int i = 1; i < template.Length; i++)
            {
                string key = $"{template[i-1]}{template[i]}";
                if (dictionaryInsertion.ContainsKey(key))
                {
                    char newChar = dictionaryInsertion[key];
                    if (!dictionary.ContainsKey(newChar))
                    {
                        dictionary.Add(newChar, 0);
                    }
                    dictionary[newChar]++;
                    sb.Append(newChar);
                }
                else
                {
                }
                sb.Append(template[i]);
            }
            return sb.ToString();*/
        }

        private static string Iteration1(string template, Dictionary<char, int> dictionary, Dictionary<string, char> dictionaryInsertion)
        {
            var sb = new StringBuilder();
            sb.Append(template[0]);
            for (int i = 1; i < template.Length; i++)
            {
                string key = $"{template[i - 1]}{template[i]}";
                if (dictionaryInsertion.ContainsKey(key))
                {
                    char newChar = dictionaryInsertion[key];
                    if (!dictionary.ContainsKey(newChar))
                    {
                        dictionary.Add(newChar, 0);
                    }
                    dictionary[newChar]++;
                    sb.Append(newChar);
                }
                else
                {
                }
                sb.Append(template[i]);
            }
            return sb.ToString();
        }

        private static (Dictionary<char, long> dictionaryCount, Dictionary<string, long> template, Dictionary<string, char> dictionaryInsertion) Parse(string[] input)
        {
            var dictionary = new Dictionary<char, long>();
            Dictionary<string, long> template = new Dictionary<string, long>();
            var rawTemplate = input[0];
            var previousChar = rawTemplate[0];
            dictionary.Add(previousChar, 1);
            for (int i = 1; i < rawTemplate.Length; i++)
            {
                var item = rawTemplate[i];
                dictionary.AddOrIncrement(item);

                var pair = $"{previousChar}{item}";
                template.AddOrIncrement(pair);
                previousChar = item;
            }

            var dictionaryInsertion = new Dictionary<string, char>();
            var tempInsertions = input.Skip(2).Select(x => x.Split(" -> "));
            foreach (var item in tempInsertions)
            {
                dictionaryInsertion.Add(item[0], item[1][0]);
            }

            return (dictionary, template, dictionaryInsertion);
        }
    }
}