using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode.Y2021
{
    internal class Day10 : GenericDay
    {
        public string Compute1(string[] input, string args)
        {
            var r =
                input
                .Select(x => new LineResult(x))
                .Where(x => x.Status == LineResult.eResult.SyntaxError)
                .Select(x => x.CostError)
                .Sum();

            return r.ToString();
        }

        public string Compute2(string[] input, string args)
        {
            var r =
                input
                .Select(x => new LineResult(x))
                .Where(x => x.Status == LineResult.eResult.Incomplete)
                .Select(x => x.CostError)
                .OrderBy(x => x)
                .ToList();

            int medianIdx = (r.Count - 1) / 2;
            var res = r[medianIdx];

            return res.ToString();
        }

        private class LineResult
        {
            public LineResult(string input)
            {
                Line = input;
                Parse();
            }

            public enum eResult
            { Valid, SyntaxError, Incomplete }

            public long CostError
            {
                get
                {
                    if (Status == eResult.SyntaxError)
                    {
                        return LastInvalidChar switch { ')' => 3, ']' => 57, '}' => 1197, '>' => 25137, _ => throw new System.ArgumentException($"Case {LastInvalidChar} not handled") };
                    }
                    else if (Status == eResult.Incomplete)
                    {
                        var res = 0L;
                        foreach (var c in MissingEnd)
                        {
                            int val = c switch { ')' => 1, ']' => 2, '}' => 3, '>' => 4, _ => throw new System.ArgumentException($"Case {LastInvalidChar} not handled") };
                            res = 5 * res + val;
                        }
                        return res;
                    }
                    return 0;
                }
            }

            public char LastInvalidChar { get; internal set; }
            public string Line { get; }
            public string MissingEnd { get; internal set; }
            public eResult Status { get; internal set; }

            private void Parse()
            {
                char d;
                var q = new Stack<char>();
                foreach (var c in Line)
                {
                    switch (c)
                    {
                        case '(':
                        case '[':
                        case '{':
                        case '<':
                            q.Push(c);
                            break;

                        case ')':
                            d = q.Pop();
                            if (d != '(')
                            {
                                LastInvalidChar = c;
                                Status = eResult.SyntaxError;
                                return;
                            }
                            break;

                        case ']':
                            d = q.Pop();
                            if (d != '[')
                            {
                                LastInvalidChar = c;
                                Status = eResult.SyntaxError;
                                return;
                            }
                            break;

                        case '}':
                            d = q.Pop();
                            if (d != '{')
                            {
                                LastInvalidChar = c;
                                Status = eResult.SyntaxError;
                                return;
                            }
                            break;

                        case '>':
                            d = q.Pop();
                            if (d != '<')
                            {
                                LastInvalidChar = c;
                                Status = eResult.SyntaxError;
                                return;
                            }
                            break;

                        default:
                            break;
                    }
                }
                if (q.Count > 0)
                {
                    MissingEnd = "";
                    while (q.Count > 0)
                    {
                        d = q.Pop();
                        MissingEnd +=
                            d switch
                            {
                                '(' => ')',
                                '[' => ']',
                                '{' => '}',
                                '<' => '>',
                                _ => throw new System.ArgumentException($"Case {d} not handled")
                            };
                    }
                    Status = eResult.Incomplete;
                }
            }
        }
    }
}