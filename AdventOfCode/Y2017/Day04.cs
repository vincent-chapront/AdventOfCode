using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace AdventOfCode.Y2017
{
    internal class Day04 : GenericDay
    {
        private static bool IsValid1(string s)
        {
            var words = s.Split(' ');
            for (int i = 0; i < words.Length; i++)
            {
                for (int j = i + 1; j < words.Length; j++)
                {
                    if (words[i] == words[j])
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private static bool IsValid2(string s)
        {
            var words = s.Split(' ');
            for (int i = 0; i < words.Length; i++)
            {
                for (int j = i + 1; j < words.Length; j++)
                {
                    if (words[i] == words[j])
                    {
                        return false;
                    }
                    var word1Ordered = words[i].ToCharArray().OrderBy(x => x).ToArray();
                    var word2Ordered = words[j].ToCharArray().OrderBy(x => x).ToArray();
                    if (word1Ordered.Length == word2Ordered.Length)
                    {
                        bool isAnagram = IsAnagram(word1Ordered, word2Ordered);
                        if (isAnagram)
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        private static bool IsAnagram(char[] word1Ordered, char[] word2Ordered)
        {
            var isAnagram = true;
            for (int k = 0; k < word2Ordered.Length; k++)
            {
                if (word1Ordered[k] != word2Ordered[k])
                {
                    isAnagram = false;
                }
            }

            return isAnagram;
        }

        public string Compute1(string[] input, string args)
        {
            return input.Count(IsValid1).ToString();
        }

        public string Compute2(string[] input, string args)
        {
            return input.Count(IsValid2).ToString();
        }
    }
}