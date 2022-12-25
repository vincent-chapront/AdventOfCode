using System.Linq;
using System.Text;
using AdventOfCode.Properties;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode.Y2015
{
    internal class Day10 : GenericDay
    {
        public string Compute1(params string[] input)
        {
            return Compute(input[0], 40).ToString();
        }
        public string Compute2(params string[] input)
        {
            return Compute(input[0], 50).ToString();
        }

        private static long Compute(string s, int iteration)
        {
            for (int i = 0; i < iteration; i++)
            {
                s = Next(s);
            }
            return s.Length;
        }

        private static string Next(string s)
        {
            var sb = new StringBuilder();
            var prevChar = s[0];

            int count = 1;
            foreach (var c in s.Skip(1))
            {
                if (c == prevChar)
                {
                    count++;
                }
                else
                {
                    sb.Append(count).Append("").Append(prevChar);
                    prevChar = c;
                    count = 1;
                }
            }
            sb.Append(count).Append("").Append(prevChar);
            return sb.ToString();
        }
    }
}