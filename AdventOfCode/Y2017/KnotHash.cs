using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Y2017
{
    public class KnotHash
    {
        public  static string CalcAsString(string input)
        {
            int[] hash = CalcAsRaw(input);

            return hash.Select(x => Convert.ToString(x, 16).PadLeft(2, '0')).Join("");
        }

        public static int[] CalcAsRaw(string input)
        {
            var lengths = input.Select(x => (int)x).ToList();
            lengths.AddRange(new List<int> { 17, 31, 73, 47, 23 });
            var sparseHash = Enumerable.Range(0, 256).ToArray();
            var skip = 0;
            var position = 0;
            for (int i = 0; i < 64; i++)
            {
                sparseHash = KnotHash.CalcKnotHashStep(lengths, sparseHash, ref position, ref skip);
            }

            var denseHash = new int[16];
            for (int i = 0; i < 16; i++)
            {
                var startidx = i * 16;
                var xor = 0;
                for (int j = startidx; j < startidx + 16; j++)
                {
                    xor = xor ^ sparseHash[j];
                }
                denseHash[i] = xor;
                ;
            }

            return denseHash;
        }

        public static int[] CalcStep(IEnumerable<int> lengths, int[] a)
        {
            var skip = 0;
            var position = 0;
            return KnotHash.CalcKnotHashStep(lengths, a, ref position, ref skip);
        }
        private static int[] CalcKnotHashStep(IEnumerable<int> lengths, int[] a, ref int position, ref int skip)
        {
            foreach (var length in lengths)
            {
                var mid = length / 2.0;
                for (int i = 0; i < mid; i++)
                {

                    var idx1 = (position + i) % a.Length;
                    var idx2 = (position + length - i - 1) % a.Length;
                    if (idx1 == idx2)
                    {
                        break;
                    }
                    var v = a[idx1];
                    a[idx1] = a[idx2];
                    a[idx2] = v;
                }
                position = (position + skip + length) % a.Length;
                skip++;
            }
            return a;
        }
    }
}