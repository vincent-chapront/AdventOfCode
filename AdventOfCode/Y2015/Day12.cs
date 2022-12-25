using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode.Y2015
{
    internal class Day12 : GenericDay
    {
        protected override object Part1()
        {
            Assert.AreEqual(6, Compute1("[1,2,3]"));
            Assert.AreEqual(6, Compute1("{\"a\":2,\"b\":4}"));
            Assert.AreEqual(3, Compute1("[[[3]]]"));
            Assert.AreEqual(3, Compute1("{\"a\":{\"b\":4},\"c\":-1}"));
            Assert.AreEqual(0, Compute1("{\"a\":[-1,1]}"));
            Assert.AreEqual(0, Compute1("[-1,{\"a\":1}]"));
            Assert.AreEqual(0, Compute1("[]"));
            Assert.AreEqual(0, Compute1("{}"));
            var res = Compute1(Resources.Year2015.Day12);
            Assert.AreEqual(111754, res);
            return res;
        }

        protected override object Part2()
        {
            Assert.AreEqual(456, Compute2("456"));
            Assert.AreEqual(-654, Compute2("-654"));
            Assert.AreEqual(0, Compute2("\"aze\""));
            Assert.AreEqual(4, Compute2("[1,\"foo\",3]"));
            Assert.AreEqual(10, Compute2("[1,\"foo\",[6,\"qsd\"],3]"));
            Assert.AreEqual(33, Compute2("[1,\"foo\",[6,[13,\"aze\"],\"qsd\"],3,[10,\"pp\"]]"));
            Assert.AreEqual(10, Compute2("{\"e\":[1,2,3,4]}"));
            Assert.AreEqual(15, Compute2("{\"e\":[1,2,3,4],\"f\":5}"));
            Assert.AreEqual(0, Compute2("{\"d\":\"red\",\"e\":[1,2,3,4],\"f\":5}"));
            var res = Compute2(Resources.Year2015.Day12);
            Assert.AreEqual(-1, res);
            return res;
        }

        public string Compute1(params string[] input)
        {
            Regex r = new Regex(@"([\-\d])+");
            var g = r.Matches(input[0]).Select(x => x.Value).Select(x => long.Parse(x)).Sum();
            return g.ToString();
        }

        public string Compute2(params string[] input)
        {
            return Compute2Parser.Start(input[0]).ToString();
        }

        private class Compute2Parser
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
                List<ValueAbstract> list = new List<ValueAbstract>();
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
                    res = res * 10 + (enumerator.Current - '0');
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
                List<ValuePair> list = new List<ValuePair>();
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

                var a = enumerator.Current;
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
                switch (c)
                {
                    case '-':
                    case '0':
                    case '1':
                    case '2':
                    case '3':
                    case '4':
                    case '5':
                    case '6':
                    case '7':
                    case '8':
                    case '9': return ReadInt(enumerator);
                    case '"': return ReadString(enumerator);
                    case '[': return ReadArray(enumerator);
                    case '{': return ReadObject(enumerator);
                    default: throw new NotImplementedException();
                }
            }

            private abstract class ValueAbstract
            { public abstract int Count { get; } }

            private class ValueArray : ValueAbstract
            {
                private ValueAbstract[] content;

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
                private ValuePair[] pairs;

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
                private string key;
                private ValueAbstract value;

                public ValuePair(string key, ValueAbstract value)
                {
                    this.key = key;
                    this.value = value;
                }

                public override int Count { get => value.Count; }
                public ValueAbstract Value => value;
            }

            private class ValueString : ValueAbstract
            {
                private string content;

                public ValueString(string content)
                {
                    this.content = content;
                }

                public string Content => content;
                public override int Count { get => 0; }
            }
        }
    }
}