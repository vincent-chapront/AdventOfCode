using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AdventOfCode.Y2015
{
    internal class Day07 : GenericDay
    {
        protected override object Part1()
        {
            Assert.AreEqual(72, Compute1(Resources.Year2015.Day07Test.ToLines(), "d"));
            Assert.AreEqual(507, Compute1(Resources.Year2015.Day07Test.ToLines(), "e"));
            Assert.AreEqual(492, Compute1(Resources.Year2015.Day07Test.ToLines(), "f"));
            Assert.AreEqual(114, Compute1(Resources.Year2015.Day07Test.ToLines(), "g"));
            Assert.AreEqual(65412, Compute1(Resources.Year2015.Day07Test.ToLines(), "h"));
            Assert.AreEqual(65079, Compute1(Resources.Year2015.Day07Test.ToLines(), "i"));
            Assert.AreEqual(123, Compute1(Resources.Year2015.Day07Test.ToLines(), "x"));
            Assert.AreEqual(456, Compute1(Resources.Year2015.Day07Test.ToLines(), "y"));

            var res = Compute1(Resources.Year2015.Day07.ToLines(), "a");
            Assert.AreEqual(46065, res);
            return res;
        }

        protected override object Part2()
        {
            var res = Compute2(Resources.Year2015.Day07.ToLines(), "a");
            Assert.AreEqual(14134, res);
            return res;
        }

        private static ushort Compute1(string[] input, string searched)
        {
            var knownValues = new Dictionary<string, ushort>();
            var res = Get(input, searched, knownValues);
            return res;
        }

        private static ushort Compute2(string[] input, string searched)
        {
            var knownValues = new Dictionary<string, ushort>();
            knownValues.Add("b", Compute1(Resources.Year2015.Day07.ToLines(), "a"));
            var res = Get(input, searched, knownValues);
            return res;
        }

        public string Compute2(params string[] input)
        {
            throw new NotImplementedException();
        }

        private static ushort Get(string[] input, string searched, Dictionary<string, ushort> knownValues)
        {
            if (knownValues.ContainsKey(searched))
            {
                return knownValues[searched];
            }
            if (ushort.TryParse(searched, out ushort result))
            {
                return result;
            }
            foreach (var line in input)
            {
                if (line.EndsWith(" -> " + searched))
                {
                    var a = line.Split(" -> ");
                    bool b = false;
                    if (a[1] == "s" || a[1] == "u" || a[1] == "ao")
                    {
                        b = true;
                        ;
                    }
                    if (a[0].Contains("NOT"))
                    {
                        var src = a[0].Substring(4);
                        var res = (ushort)(0xffff - Get(input, src, knownValues));
                        knownValues.Add(searched, res);
                        if (b)
                        {
                            ;
                        }
                        return res;
                    }
                    else if (a[0].Contains("AND"))
                    {
                        var src = a[0].Split(" AND ");
                        ushort a1 = Get(input, src[0], knownValues);
                        ushort b1 = Get(input, src[1], knownValues);
                        var res = (ushort)(a1 & b1);
                        knownValues.Add(searched, res);
                        if (b)
                        {
                            ;
                        }
                        return res;
                    }
                    else if (a[0].Contains("OR"))
                    {
                        var src = a[0].Split(" OR ");
                        var res = (ushort)(Get(input, src[0], knownValues) | Get(input, src[1], knownValues));
                        knownValues.Add(searched, res);
                        if (b)
                        {
                            ;
                        }
                        return res;
                    }
                    else if (a[0].Contains("LSHIFT"))
                    {
                        var src = a[0].Split(" LSHIFT ");
                        var res = (ushort)(Get(input, src[0], knownValues) << int.Parse(src[1]));
                        knownValues.Add(searched, res);
                        if (b)
                        {
                            ;
                        }
                        return res;
                    }
                    else if (a[0].Contains("RSHIFT"))
                    {
                        var src = a[0].Split(" RSHIFT ");
                        var res = (ushort)(Get(input, src[0], knownValues) >> int.Parse(src[1]));
                        knownValues.Add(searched, res);
                        if (b)
                        {
                            ;
                        }
                        return res;
                    }
                    else
                    {
                        ushort value;
                        if (ushort.TryParse(a[0], out value))
                        {
                            var res = value;
                            knownValues.Add(searched, res);
                            if (b)
                            {
                                ;
                            }
                            return res;
                        }
                        else
                        {
                            var src = a[0];
                            var res = Get(input, src, knownValues);
                            knownValues.Add(searched, res);
                            if (b)
                            {
                                ;
                            }
                            return res;
                        }
                    }
                }
            }
            return 0;
        }

        private static Dictionary<string, ushort> Process(string[] input)
        {
            var d = new Dictionary<string, ushort>();

            var queue = new Queue<string>(input);
            while (queue.Count > 0)
            {
                var line = queue.Dequeue();

                var a = line.Split(" -> ");
                var dest = a[1];
                if (a[0].Contains("NOT"))
                {
                    var src = a[0].Substring(4);
                    if (!d.ContainsKey(src))
                    {
                        queue.Enqueue(line);
                        continue;
                    }
                    if (!d.ContainsKey(dest))
                    {
                        d.Add(dest, 0);
                    }
                    d[dest] = (ushort)(0xffff - d[src]);
                }
                else if (a[0].Contains("AND"))
                {
                    var src = a[0].Split(" AND ");
                    if (!d.ContainsKey(src[0]) || !d.ContainsKey(src[1]))
                    {
                        queue.Enqueue(line);
                        continue;
                    }
                    if (!d.ContainsKey(dest))
                    {
                        d.Add(dest, 0);
                    }
                    d[dest] = (ushort)(d[src[0]] & d[src[1]]);
                }
                else if (a[0].Contains("OR"))
                {
                    var src = a[0].Split(" OR ");
                    if (!d.ContainsKey(src[0]) || !d.ContainsKey(src[1]))
                    {
                        queue.Enqueue(line);
                        continue;
                    }
                    if (!d.ContainsKey(dest))
                    {
                        d.Add(dest, 0);
                    }
                    d[dest] = (ushort)(d[src[0]] | d[src[1]]);
                }
                else if (a[0].Contains("LSHIFT"))
                {
                    var src = a[0].Split(" LSHIFT ");
                    if (!d.ContainsKey(src[0]))
                    {
                        queue.Enqueue(line);
                        continue;
                    }
                    if (!d.ContainsKey(dest))
                    {
                        d.Add(dest, 0);
                    }
                    d[dest] = (ushort)(d[src[0]] << int.Parse(src[1]));
                }
                else if (a[0].Contains("RSHIFT"))
                {
                    var src = a[0].Split(" RSHIFT ");
                    if (!d.ContainsKey(src[0]))
                    {
                        queue.Enqueue(line);
                        continue;
                    }
                    if (!d.ContainsKey(dest))
                    {
                        d.Add(dest, 0);
                    }
                    d[dest] = (ushort)(d[src[0]] >> int.Parse(src[1]));
                }
                else
                {
                    if (!d.ContainsKey(dest))
                    {
                        d.Add(dest, 0);
                    }
                    ushort value;
                    if (ushort.TryParse(a[0], out value))
                    {
                        d[dest] = value;
                    }
                    else
                    {
                        var src = a[0];
                        if (!d.ContainsKey(src))
                        {
                            queue.Enqueue(line);
                            continue;
                        }
                        d[dest] = d[src];
                    }
                }
            }

            return d;
        }
    }
}