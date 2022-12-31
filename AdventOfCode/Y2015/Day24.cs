using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode.Y2015
{
    internal class Day24 : GenericDay
    {
        public string Compute1(string[] input, string args)
        {
            var l = input.Select(x => int.Parse(x)).ToList();
            var target = l.Sum() / 3;
            var q = new Queue<int>(l);
            var done = false;
            var g1 = new List<int>();
            var g2 = new List<int>();
            var g3 = new List<int>();
            var res = new List<(List<int>, List<int>, List<int>)>();
            //Compute1Rec(target, g1, g2, g3, l, res);

            return Compute1Rec1(target, l.OrderByDescending(x => x).ToList()).ToString();
            var r = res.OrderBy(x => x.Item1.Count);

            /*while (!done)
            {
                var v = q.Dequeue();
                int sumG1 = g1.Sum();
                int sumG2 = g2.Sum();
                int sumG3 = g3.Sum();
                if (sumG1 + v <= target)
                {
                    g1.Add(v);
                }
                else if (sumG1==target)
                {
                    if (sumG2 + v <= target)
                    {
                        g2.Add(v);
                    }
                    else if (sumG2 == target)
                    {
                        if (sumG3 + v <= target)
                        {
                            g3.Add(v);
                        }
                        else if (sumG3 == target)
                        {
                            res.Add((new List<int>(g1), new List<int>(g2), new List<int>(g3)));
                        }
                    }
                }
            }*/

            return "ERROR";
        }

        private static long Compute1Rec1(int target, List<int> q)
        {
            var max = (int)Math.Pow(2, q.Count);
            var a =
                Enumerable.Range(1, max - 1)
                .Select(x =>
                    {
                        var b =
                            Convert.ToString(x, 2)
                            .PadLeft(q.Count, '0')
                            .ToArray()
                            .Select((val, idx) => (val, idx))
                            .Where(x => x.val == '1')
                            .Select(x => x.idx)
                            .ToList()
                            ;

                        var c =
                            q.Select((val, idx) => (val, idx))
                            .Where((val, idx) => b.Contains(idx))
                            .Select(x => x.val)
                            .ToList();

                        return c;
                    }
                )
                .Where(x => x.Sum() == target)
                .ToList();
            List<(List<int>, List<int>, List<int>)> res = new List<(List<int>, List<int>, List<int>)>();
            var b =
                FOO(a)
                .ToList();
            var minSizeG1 = b.Min(x => x[0].Count);
            var c = b.Where(x => x[0].Count == minSizeG1).ToList();
            return c.Min(x => x[0].Product());
            ;
        }

        private static IEnumerable<List<List<int>>> FOO(List<List<int>> set)
        {
            for (int i = 0; i < set.Count; i++)
            {
                for (int j = 0; j < set.Count; j++)
                {
                    for (int k = 0; k < set.Count; k++)
                    {
                        if (i != j && i != k && j != k
                            && !set[i].Any(x => set[j].Contains(x))
                            && !set[i].Any(x => set[k].Contains(x))
                            && !set[j].Any(x => set[k].Contains(x))
                            )
                        {
                            yield return new List<List<int>> { set[i], set[j], set[k] };
                        }
                    }
                }
            }
        }

        private static void Compute1Rec(int target, List<int> g1, List<int> g2, List<int> g3, List<int> q, List<(List<int>, List<int>, List<int>)> res)
        {
            if (q.Count == 0)
            {
                if (res.Count == 0)
                {
                    res.Add((g1, g2, g3));
                }
                else
                {
                    if (g1.Count < res[0].Item1.Count)
                    {
                        res.Clear();
                        res.Add((g1, g2, g3));
                    }
                    else if (g1.Count == res[0].Item1.Count)
                    {
                        res.Add((g1, g2, g3));
                    }
                }
                return;
            }
            bool added = true;
            for (int i = 0; i < q.Count; i++)
            {
                added = false;
                var v = q[i];
                int sumG1 = g1.Sum();
                int sumG2 = g2.Sum();
                int sumG3 = g3.Sum();
                if (sumG1 + v <= target)
                {
                    added = true;
                    g1.Add(v);
                    q.RemoveAt(i);
                    Compute1Rec(target, new List<int>(g1), new List<int>(g2), new List<int>(g3), q, res);
                    q.Insert(i, v);
                    g1.RemoveAt(g1.Count - 1);
                }
                else if (sumG2 + v <= target)
                {
                    added = true;
                    g2.Add(v);
                    q.RemoveAt(i);
                    Compute1Rec(target, new List<int>(g1), new List<int>(g2), new List<int>(g3), q, res);
                    q.Insert(i, v);
                    g2.RemoveAt(g2.Count - 1);
                }
                else if (sumG3 + v <= target)
                {
                    added = true;
                    g3.Add(v);
                    q.RemoveAt(i);
                    Compute1Rec(target, new List<int>(g1), new List<int>(g2), new List<int>(g3), q, res);
                    q.Insert(i, v);
                    g3.RemoveAt(g3.Count - 1);
                }
            }
        }

        public string Compute2(string[] input, string args)
        {
            throw new NotImplementedException();
        }
    }
}