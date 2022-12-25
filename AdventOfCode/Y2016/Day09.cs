using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode.Y2016
{
    internal class Day09 : GenericDay
    {
        public string Compute1(params string[] input)
        {
            return Compute(input[0], false).ToString();
        }

        public string Compute2(params string[] input)
        {
            return Compute(input[0], true).ToString();
        }

        private static long Compute(string input, bool isRecursive)
        {
            long size = input.Length;
            var idxStart = input.IndexOf('(');
            while (idxStart >= 0)
            {
                var idxEnd = input.IndexOf(')', idxStart + 1);
                var idxSeparator = input.IndexOf('x', idxStart + 1);
                var l = int.Parse(input.Substring(idxStart + 1, idxSeparator - idxStart - 1));
                var m = int.Parse(input.Substring(idxSeparator + 1, idxEnd - idxSeparator - 1));

                long subSize = isRecursive ? Compute(input.Substring(idxEnd + 1, l), true) : l;

                size = size - (idxEnd - idxStart + 1) - l + subSize * m;
                idxStart = input.IndexOf('(', idxEnd + l);
            }
            return size;
        }
    }
}