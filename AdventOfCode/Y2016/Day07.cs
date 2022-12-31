using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode.Y2016
{
    internal class Day07 : GenericDay
    {
        private static List<string> patterns = GetPatterns();

        private static bool IsSupportTls(string ip)
        {
            var a = ip[0];
            var b = ip[0];
            var c = ip[1];
            var d = ip[2];
            var isOutside = true;

            var haveOutside = false;

            foreach (var v in ip.Skip(3))
            {
                a = b;
                b = c;
                c = d;
                d = v;
                if (a == d && b == c && a != b)
                {
                    if (isOutside)
                    {
                        haveOutside = true;
                    }
                    else
                    {
                        return false;
                    }
                }
                if (v == '[' || v == ']')
                {
                    isOutside = !isOutside;
                }
            }
            return haveOutside;
        }

        public string Compute1(string[] input, string args)
        {
            var res = input.Where(x => IsSupportTls(x)).Count();
            return res.ToString();
        }

        private static bool IsSupportSsl(string s)
        {
            bool isOutside = true;
            char a = s[0];
            char b = s[0];
            char c = s[1];
            var patternOutside = new List<string>();
            var patternInside = new List<string>();

            foreach (var v in s.Skip(2))
            {
                a = b;
                b = c;
                c = v;

                if (v == '[' || v == ']')
                {
                    isOutside = !isOutside;
                }
                if (a == c && a != b)
                {
                    var pattern = "" + a + b + c;
                    var reversed = "" + b + a + b;
                    if (isOutside)
                    {
                        if (patternInside.Contains(reversed))
                        {
                            return true;
                        }
                        patternOutside.Add("" + a + b + c);
                    }
                    else
                    {
                        if (patternOutside.Contains(reversed))
                        {
                            return true;
                        }
                        patternInside.Add("" + a + b + c);
                    }
                }
                ;
            }
            return false;
        }

        public string Compute2(string[] input, string args)
        {
            var res = input.Where(x => IsSupportSsl(x)).Count();
            return res.ToString();
        }

        private static List<string> GetPatterns()
        {
            List<string> res = new List<string>();
            for (int i = 0; i < 26; i++)
            {
                var a = (char)('a' + i);
                for (int j = 0; j < 26; j++)
                {
                    if (i == j) { continue; }
                    var b = (char)('a' + j);
                    res.Add("" + a + b + b + a);
                }
            }
            return res;
        }
    }
}