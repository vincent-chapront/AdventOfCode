using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode.Y2015
{
    internal class Day08 : GenericDay
    {
        public string Compute1(params string[] input)
        {
            var res = 0;
            foreach (var line in input)
            {
                (int nbChar, int memorySize) = MeasureString(line);
                res += memorySize - nbChar;
            }
            return res.ToString();
        }

        public string Compute2(params string[] input)
        {
            var res = 0;
            foreach (var line in input)
            {
                var encoded = Encode(line);
                res += encoded.Length - line.Length;
            }
            return res.ToString();
        }

        private static string Encode(string s)
        {
            var sb = new StringBuilder();
            sb.Append("\"");
            foreach (var c in s)
            {
                if (c == '\\' || c == '\"')
                {
                    sb.Append("\\" + c);
                }
                else
                {
                    sb.Append(c);
                }
            }
            sb.Append("\"");
            return sb.ToString();
        }

        private static (int nbChar, int memorySize) MeasureString(string s)
        {
            var memorySize = s.Length;
            var nbChar = 0;
            for (int i = 1; i < s.Length - 1; i++)
            {
                nbChar++;
                if (s[i] == '\\')
                {
                    if (s[i + 1] == '\"' || s[i + 1] == '\\')
                    {
                        i++;
                    }
                    else if (s[i + 1] == 'x')
                    {
                        i += 3;
                    }
                }
            }
            return (nbChar, memorySize);
        }
    }
}