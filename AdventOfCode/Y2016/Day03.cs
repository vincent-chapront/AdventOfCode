using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode.Y2016
{
    internal class Day03 : GenericDay
    {
        public string Compute1(string[] input, string args)
        {
            return input.Select(x => Parse(x)).Count(IsValid).ToString();
        }

        public string Compute2(string[] input, string args)
        {
            var res = 0;
            for (int i = 0; i < input.Length; i += 3)
            {
                var line1 = Parse(input[i]);
                var line2 = Parse(input[i + 1]);
                var line3 = Parse(input[i + 2]);
                if (IsValid((line1.a, line2.a, line3.a)))
                {
                    res++;
                }
                if (IsValid((line1.b, line2.b, line3.b)))
                {
                    res++;
                }
                if (IsValid((line1.c, line2.c, line3.c)))
                {
                    res++;
                }
            }
            return res.ToString();
        }

        private static bool IsValid((int a, int b, int c) sides)
        {
            if (sides.a + sides.b > sides.c
                && sides.b + sides.c > sides.a
                && sides.c + sides.a > sides.b)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static (int a, int b, int c) Parse(string s)
        {
            //var a = s.Replace(" ", "");
            var sideLength = s.Split(' ').Where(x => !string.IsNullOrWhiteSpace(x)).Select(x => int.Parse(x)).ToArray();
            return (sideLength[0], sideLength[1], sideLength[2]);
        }
    }
}