using System.Collections.Generic;

namespace AdventOfCode.Y2015
{
    internal class Day07 : GenericDay
    {
        public string Compute1(string[] input, string args)
        {
            var res = Get(input, args);
            return res.ToString();
        }

        public string Compute2(string[] input, string args)
        {
            var init = Get(input, "a");
            var knownValues = new Dictionary<string, ushort> { { "b", init } };
            var res = Get(input, args, knownValues);
            return res.ToString();
        }

        private static ushort Get(string[] input, string searched)
        {
            var knownValues = new Dictionary<string, ushort>();
            return Get(input, searched, knownValues);
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

                    if (a[0].Contains("NOT"))
                    {
                        var src = a[0][4..];
                        var res = (ushort)(0xffff - Get(input, src, knownValues));
                        knownValues.Add(searched, res);

                        return res;
                    }
                    else if (a[0].Contains("AND"))
                    {
                        var src = a[0].Split(" AND ");
                        ushort a1 = Get(input, src[0], knownValues);
                        ushort b1 = Get(input, src[1], knownValues);
                        var res = (ushort)(a1 & b1);
                        knownValues.Add(searched, res);

                        return res;
                    }
                    else if (a[0].Contains("OR"))
                    {
                        var src = a[0].Split(" OR ");
                        var res = (ushort)(Get(input, src[0], knownValues) | Get(input, src[1], knownValues));
                        knownValues.Add(searched, res);

                        return res;
                    }
                    else if (a[0].Contains("LSHIFT"))
                    {
                        var src = a[0].Split(" LSHIFT ");
                        var res = (ushort)(Get(input, src[0], knownValues) << int.Parse(src[1]));
                        knownValues.Add(searched, res);

                        return res;
                    }
                    else if (a[0].Contains("RSHIFT"))
                    {
                        var src = a[0].Split(" RSHIFT ");
                        var res = (ushort)(Get(input, src[0], knownValues) >> int.Parse(src[1]));
                        knownValues.Add(searched, res);

                        return res;
                    }
                    else
                    {
                        if (ushort.TryParse(a[0], out ushort value))
                        {
                            var res = value;
                            knownValues.Add(searched, res);

                            return res;
                        }
                        else
                        {
                            var src = a[0];
                            var res = Get(input, src, knownValues);
                            knownValues.Add(searched, res);

                            return res;
                        }
                    }
                }
            }
            return 0;
        }
    }
}