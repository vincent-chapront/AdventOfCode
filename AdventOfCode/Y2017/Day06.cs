using AdventOfCode.Properties;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Y2017
{
    internal class Day06 : GenericDay
    {
        public string Compute1(string[] input, string args)
        {
            return Compute(input[0]).step.ToString();
        }
        public string Compute2(string[] input, string args)
        {
            return Compute(input[0]).size.ToString();
        }
        private static (long step, long size) Compute(string input)
        {
            var map = input.Split('\t').Select(x=>int.Parse(x)).ToArray();
            var knownConfig = new List<string>();
            var step = 0;
            knownConfig.Add(string.Join("-", map));
            while (true)
            {
                step++;
                var idxMax = -1;
                var max = int.MinValue;
                for (int i = 0; i < map.Length; i++)
                {
                    if(map[i]>max)
                    {
                        idxMax = i;
                        max = map[i];
                    }
                }

                map[idxMax] = 0;
                var currentIdx = idxMax;
                for (int i = 0; i < max; i++)
                {
                    currentIdx = (currentIdx + 1) % map.Length;
                    map[currentIdx]++;
                }
                var key = string.Join("-", map);
                if (knownConfig.Contains(key))
                {
                    var idx = knownConfig.IndexOf(key);
                    var size = knownConfig.Count - idx;
                    return (step,size);
                }
                knownConfig.Add(key);
            }
        }
    }
}