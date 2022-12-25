using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace AdventOfCode.Y2017
{
    internal class Day13 : GenericDay
    {
        public string Compute1(params string[] input)
        {
            var p = input.Select(x => { var a = x.Split(": "); return (depth: int.Parse(a[0]), range: int.Parse(a[1])); }).ToArray();
            var res = 0;
            foreach (var item in p)
            {
                if (item.depth == 0) continue;
                if (item.range == 2)
                {
                    if (item.depth % item.range == 0)
                    {
                        res += item.range * item.depth;
                    }
                }
                if (item.depth % ((item.range - 1) * 2) == 0)
                {
                    res += item.range * item.depth;
                }
            }
            return res.ToString();
        }

        public string Compute2(params string[] input)
        {
            var p = input.Select(x => { var a = x.Split(": "); return (depth: int.Parse(a[0]), range: int.Parse(a[1])); }).ToArray();
            for (int i = 0; i < 100; i++)
            {
                if (i == 4)
                {
                    ;
                }
                var failed = false;

                Console.WriteLine();
                foreach (var item in p)
                {
                    if (item.depth == 0) continue;
                    if (item.range == 2)
                    {
                        if ((item.depth + i) % item.range == 0)
                        {
                            failed = true;
                            break;
                        }
                    }
                    if ((item.depth + i) % ((item.range - 1) * 2) == 0)
                    {
                        failed = true;
                        break;
                    }
                }
                if (!failed)
                {
                    return i.ToString();
                }
            }
            return "ERROR";
        }
    }
}