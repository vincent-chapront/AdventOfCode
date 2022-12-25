using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode.Y2015
{
    internal class Day11 : GenericDay
    {
        protected override object Part1()
        {
            Assert.AreEqual(false, IsValid("hijklmmn"));
            Assert.AreEqual(false, IsValid("abbceffg"));
            Assert.AreEqual(false, IsValid("abbcegjk"));
            Assert.AreEqual("abd", Next("abc"));
            Assert.AreEqual("aca", Next("abz"));
            Assert.AreEqual("baa", Next("azz"));
            Assert.AreEqual("abcdffaa", NextValid("abcdefgh"));
            var res = NextValid(Resources.Year2015.Day11);
            Assert.AreEqual("vzbxxyzz", res);
            return res;
        }

        protected override object Part2()
        {
            var res = NextValid(Resources.Year2015.Day11);
            res = NextValid(res);
            Assert.AreEqual("vzcaabcc", res);
            return res;
        }

        private static bool IsValid(string s)
        {
            var hasSequence = false;
            var numberOfPair = 0;
            var lastPair = -1;
            for (int i = 0; i < s.Length; i++)
            {
                var c = s[i];
                if (c == 'i' || c == 'o' || c == 'l')
                {
                    return false;
                }
                if (i < s.Length - 1)
                {
                    if (c == s[i + 1])
                    {
                        if (lastPair != i - 1)
                        {
                            numberOfPair++;
                            lastPair = i;
                        }
                    }
                }

                if (i < s.Length - 2)
                {
                    if (c == s[i + 1] - 1 && s[i + 1] == s[i + 2] - 1)
                    {
                        hasSequence = true;
                    }
                }
            }

            return hasSequence && numberOfPair >= 2;
        }

        private static string Next(string s)
        {
            if (s[s.Length - 1] == 'z')
            {
                var retenu = 1;
                var sb = new StringBuilder();
                sb.Insert(0, 'a');
                for (int i = s.Length - 2; i >= 0; i--)
                {
                    char c;
                    if (s[i] == 'z' && retenu == 1)
                    {
                        c = 'a';
                        retenu = 1;
                    }
                    else
                    {
                        c = (char)(s[i] + retenu);
                        retenu = 0;
                    }
                    sb.Insert(0, c);
                }
                return sb.ToString();
            }
            else
            {
                return s.Substring(0, s.Length - 1) + (char)(s[s.Length - 1] + 1);
            }
        }

        private static string NextValid(string s)
        {
            int i = 0;
            var a = s.Substring(0, 5);
            do
            {
                i++;
                var b = Next(s);
                if (b.Substring(0, 5) != a)
                {
                    a = s.Substring(0, 5);
                }
                s = b;
            } while (!IsValid(s));
            return s;
        }
    }
}