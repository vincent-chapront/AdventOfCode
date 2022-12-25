using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode.Y2020
{
    internal class Day14 : GenericDay
    {
        public string Compute1(params string[] input)
        {
            ulong maskAnd = 0;
            ulong maskOr = 0;
            var memoryMap = new Dictionary<long, long>();
            foreach (var line in input)
            {
                var a = line.Split(" = ");
                var action = a[0];
                var value = a[1];
                if (action == "mask")
                {
                    maskAnd = Convert.ToUInt64(value.Replace('X', '1'), 2);
                    maskOr = Convert.ToUInt64(value.Replace('X', '0'), 2);
                }
                else
                {
                    var idx = int.Parse(action[4..^1]);
                    if (!memoryMap.ContainsKey(idx))
                    {
                        memoryMap.Add(idx, 0);
                    }

                    ulong maskedValue = (ulong.Parse(value) | maskOr) & maskAnd;
                    memoryMap[idx] = (long)maskedValue;
                }
            }
            return memoryMap.Sum(x => x.Value).ToString();
        }

        public string Compute2(params string[] input)
        {
            var memoryMap = new Dictionary<ulong, long>();
            var mask = "";
            foreach (var line in input)
            {
                var a = line.Split(" = ");
                var action = a[0];
                var value = a[1];
                if (action == "mask")
                {
                    mask = value;
                }
                else
                {
                    var rawIdx = int.Parse(action[4..^1]);

                    foreach (var maskedIdx in GenerateMaskedIdx(rawIdx, mask))
                    {
                        if (!memoryMap.ContainsKey(maskedIdx))
                        {
                            memoryMap.Add(maskedIdx, 0);
                        }

                        memoryMap[maskedIdx] = long.Parse(value);
                    }
                }
            }
            return memoryMap.Sum(x => x.Value).ToString();
        }

        private static IEnumerable<ulong> GenerateMaskedIdx(int idx, string mask)
        {
            string s = "123".ReplaceAtPosition(2, 'X');

            var sb = new StringBuilder();
            string idxString = Convert.ToString(idx, 2).PadLeft(mask.Length, '0');
            for (int i = 0; i < mask.Length; i++)
            {
                if (mask[i] == '0')
                {
                    sb.Append(idxString[i]);
                }
                else
                {
                    sb.Append(mask[i]);
                }
            }

            var indexX = new List<int>();
            int currentIndex = mask.IndexOf("X", 0);
            while (currentIndex > -1)
            {
                indexX.Add(currentIndex);
                currentIndex = mask.IndexOf("X", currentIndex + 1);
            }

            int n = (int)Math.Pow(2, indexX.Count);

            for (int i = 0; i < n; i++)
            {
                var cc = Convert.ToString(i, 2).PadLeft(indexX.Count, '0').Select(x => x).ToArray();
                var maskToSend = sb.ToString();
                for (int j = 0; j < indexX.Count; j++)
                {
                    maskToSend = maskToSend.ReplaceAtPosition(indexX[j], cc[j]);
                }
                var dd = string.Join("", cc);
                yield return Convert.ToUInt64(maskToSend, 2);
                ;
            }
        }
    }
}