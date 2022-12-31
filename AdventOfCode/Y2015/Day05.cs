using System.Linq;

namespace AdventOfCode.Y2015
{
    internal class Day05 : GenericDay
    {
        public string Compute1(string[] input, string args) => input.Count(IsNice1).ToString();

        public string Compute2(string[] input, string args) => input.Count(IsNice2).ToString();

        private static bool IsNice1(string input)
        {
            static bool IsVowel(char c) => c == 'a' || c == 'e' || c == 'i' || c == 'o' || c == 'u';
            char prevChar = input[0];
            int nbVowels = IsVowel(prevChar) ? 1 : 0;
            bool hasDouble = false;
            for (int i = 1; i < input.Length; i++)
            {
                if (input[i] == prevChar)
                {
                    hasDouble = true;
                }
                nbVowels += IsVowel(input[i]) ? 1 : 0;
                if (
                    (prevChar == 'a' && input[i] == 'b')
                    || (prevChar == 'c' && input[i] == 'd')
                    || (prevChar == 'p' && input[i] == 'q')
                    || (prevChar == 'x' && input[i] == 'y'))
                {
                    return false;
                }
                prevChar = input[i];
            }
            return nbVowels >= 3 && hasDouble;
        }

        private static bool IsNice2(string input)
        {
            var foundDuplicate = false;
            var foundRepeating = false;
            for (int i = 0; i < input.Length - 1 && (!foundDuplicate || !foundRepeating); i++)
            {
                if (!foundDuplicate)
                {
                    var s1 = input[i] + "" + input[i + 1];
                    for (int j = i + 2; j < input.Length - 1; j++)
                    {
                        var s2 = input[j] + "" + input[j + 1];
                        if (s1 == s2)
                        {
                            foundDuplicate = true;
                            break;
                        }
                    }
                }

                if (i < input.Length - 2)
                {
                    if (input[i] == input[i + 2])
                    {
                        foundRepeating = true;
                    }
                }
            }

            return foundDuplicate && foundRepeating;
        }
    }
}