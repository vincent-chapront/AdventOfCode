using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode.Y2016
{
    internal class Day06 : GenericDay
    {
        public string Compute1(string[] input, string args)
        {
            var res = "";
            for (int position = 0; position < input[0].Length; position++)
            {
                var a = new char[input.Length];
                for (int lineIndex = 0; lineIndex < input.Length; lineIndex++)
                {
                    string line = input[lineIndex];
                    a[lineIndex] = line[position];
                }
                var b =a.GroupBy(x => x).Select(x => (x.Key, x.Count())).OrderByDescending(x=>x.Item2).First().Key;
                res += b;
                ;
            }
            return res;
        }

        public string Compute2(string[] input, string args)
        {
            var res = "";
            for (int position = 0; position < input[0].Length; position++)
            {
                var a = new char[input.Length];
                for (int lineIndex = 0; lineIndex < input.Length; lineIndex++)
                {
                    string line = input[lineIndex];
                    a[lineIndex] = line[position];
                }
                var b = a.GroupBy(x => x).Select(x => (x.Key, x.Count())).OrderBy(x => x.Item2).First().Key;
                res += b;
                ;
            }
            return res;
        }
    }
}