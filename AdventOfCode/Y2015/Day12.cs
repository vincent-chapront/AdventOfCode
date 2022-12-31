using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Y2015
{
    internal class Day12 : GenericDay
    {
        public string Compute1(string[] input, string args)
        {
            var r = new Regex(@"([\-\d])+");
            var g = r.Matches(input[0]).Select(x => x.Value).Select(x => long.Parse(x)).Sum();
            return g.ToString();
        }

        public string Compute2(string[] input, string args)
        {
            return Compute2Parser.Start(input[0]).ToString();
        }

        private static class Compute2Parser
        {
            public static int Start(string input)
            {
                var enumerator = input.Replace(" ", "").AsEnumerable().GetEnumerator();
                enumerator.MoveNext();

                return ReadValue(enumerator).Count;
            }

            private static ValueArray ReadArray(IEnumerator<char> enumerator)
            {
                var a = enumerator.Current;
                var list = new List<ValueAbstract>();
                while (a != ']')
                {
                    enumerator.MoveNext();
                    var v = ReadValue(enumerator);
                    list.Add(v);
                    a = enumerator.Current;
                }
                enumerator.MoveNext();
                return new ValueArray(list.ToArray());
            }

            private static ValueInt ReadInt(IEnumerator<char> enumerator)
            {
                var res = 0;
                var sign = 1;
                if (enumerator.Current == '-')
                {
                    sign = -1;
                    enumerator.MoveNext();
                }
                while (char.IsDigit(enumerator.Current))
                {
                    res = (res * 10) + (enumerator.Current - '0');
                    if (!enumerator.MoveNext())
                    {
                        break;
                    }
                }
                return new ValueInt(res * sign);
            }

            private static ValueObject ReadObject(IEnumerator<char> enumerator)
            {
                var a = enumerator.Current;
                var list = new List<ValuePair>();
                while (a != '}')
                {
                    enumerator.MoveNext();
                    var v = ReadPair(enumerator);
                    list.Add(v);
                    a = enumerator.Current;
                }
                enumerator.MoveNext();
                return new ValueObject(list.ToArray());
            }

            private static ValuePair ReadPair(IEnumerator<char> enumerator)
            {
                var key = ReadString(enumerator);

                enumerator.MoveNext();

                var r = ReadValue(enumerator);
                return new ValuePair(key.Content, r);
            }

            private static ValueString ReadString(IEnumerator<char> enumerator)
            {
                var result = "";
                var a = enumerator.Current;
                enumerator.MoveNext();
                if (a == '"')
                {
                    do
                    {
                        a = enumerator.Current;
                        if (a != '"')
                        {
                            result += a;
                        }
                        enumerator.MoveNext();
                    } while (a != '"');
                }
                return new ValueString(result);
            }

            private static ValueAbstract ReadValue(IEnumerator<char> enumerator)
            {
                var c = enumerator.Current;
                return c switch
                {
                    '-' or '0' or '1' or '2' or '3' or '4' or '5' or '6' or '7' or '8' or '9' => ReadInt(enumerator),
                    '"' => ReadString(enumerator),
                    '[' => ReadArray(enumerator),
                    '{' => ReadObject(enumerator),
                    _ => throw new NotImplementedException(),
                };
            }

            private abstract class ValueAbstract
            {
                public abstract int Count { get; }
            }

            private class ValueArray : ValueAbstract
            {
                private readonly ValueAbstract[] content;

                public ValueArray(ValueAbstract[] content)
                {
                    this.content = content;
                }

                public override int Count { get => content.Sum(x => x.Count); }
            }

            private class ValueInt : ValueAbstract
            {
                private readonly int content;

                public ValueInt(int content)
                {
                    this.content = content;
                }

                public override int Count { get => content; }
            }

            private class ValueObject : ValueAbstract
            {
                private readonly ValuePair[] pairs;

                public ValueObject(ValuePair[] pairs)
                {
                    this.pairs = pairs;
                }

                public override int Count
                {
                    get
                    {
                        if (pairs.Any(x => (x.Value is ValueString vs) && vs.Content == "red"))
                        {
                            return 0;
                        }
                        return pairs.Sum(x => x.Count);
                    }
                }
            }

            private class ValuePair : ValueAbstract
            {
                public ValuePair(string key, ValueAbstract value)
                {
                    Key = key;
                    Value = value;
                }

                public override int Count { get => Value.Count; }
                public string Key { get; }
                public ValueAbstract Value { get; }
            }

            private class ValueString : ValueAbstract
            {
                public ValueString(string content)
                {
                    Content = content;
                }

                public string Content { get; }
                public override int Count { get => 0; }
            }
        }
    }
}