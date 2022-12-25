using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode.Y2021
{
    internal class Day18 : GenericDay
    {
        private enum eSide
        { Left, Right }

        protected override object Part1()
        {
            SnailNumber n;
            n = Parse("[[1,2],[[3,4],5]]");
            Assert.AreEqual("[[1,2],[[3,4],5]]", n.ToString());
            Assert.AreEqual(143, n.GetMagnitude());
            n = Parse("[[[[0,7],4],[[7,8],[6,0]]],[8,1]]");
            Assert.AreEqual("[[[[0,7],4],[[7,8],[6,0]]],[8,1]]", n.ToString());
            Assert.AreEqual(1384, n.GetMagnitude());

            var a = Parse("[1,2]");
            var b = Parse("[[3,4],5]");
            n = a + b;
            Assert.AreEqual("[[1,2],[[3,4],5]]", n.ToString());

            n = Parse("[[6,[5,[4,[3,2]]]],1]");
            n = SnailNumber.Reduce(n);
            //Assert.AreEqual("[[[[0,9],2],3],4]", n.ToString());

            n = Parse("[[[[[4,3],4],4],[7,[[8,4],9]]],[1,1]]");
            var p = SnailNumber.FindToBeExploded(n);
            return 0;
        }

        protected override object Part2()
        {
            throw new NotImplementedException();
        }

        private static SnailNumber Parse(string input)
        {
            int i = 1;
            return ParseRec(input, ref i);
            SnailNumber ParseRec(string input, ref int idx)
            {
                var c = input[idx++];
                SnailNumber left = '0' <= c && c <= '9' ? new SnailValue(c - '0') : ParseRec(input, ref idx);
                idx++;
                c = input[idx++];
                SnailNumber right = '0' <= c && c <= '9' ? new SnailValue(c - '0') : ParseRec(input, ref idx);
                idx++;
                return new SnailPair(left, right);
            }
        }

        private int GetLeftMost(SnailNumber number)
        {
            while (true)
            {
                if (number is SnailPair pair)
                {
                    number = pair.Left;
                }
                else
                {
                    var v = number as SnailValue;
                    return v.Value;
                }
            }
        }

        private int GetRightMost(SnailNumber number)
        {
            while (true)
            {
                if (number is SnailPair pair)
                {
                    number = pair.Left;
                }
                else
                {
                    var v = number as SnailValue;
                    return v.Value;
                }
            }
        }

        private abstract class SnailNumber
        {
            public long Level
            {
                get
                {
                    if (Parent == null)
                    {
                        return 0;
                    }

                    return 1 + Parent.Level;
                }
            }

            public SnailNumber Parent { get; set; }

            public eSide Side { get; set; }

            public static SnailNumber operator +(SnailNumber a, SnailNumber b)
                => a.Add(b);

            public SnailNumber Add(SnailNumber other)
            {
                return new SnailPair(this, other);
            }

            public abstract long GetMagnitude();

            internal static SnailPair FindToBeExploded(SnailNumber n)
            {
                var p = n as SnailPair;
                if (p == null)
                {
                    return null;
                }

                if (n.Level >= 4)
                {
                    return p;
                }

                var res = FindToBeExploded(p.Left);
                if (res != null)
                {
                    return res;
                }
                else return FindToBeExploded(p.Right);
            }

            internal static SnailNumber Reduce(SnailNumber n)
            {
                bool hasChanged = true;
                while (hasChanged)
                {
                    hasChanged = false;

                    var toBeExploded = FindToBeExploded(n);
                    if (toBeExploded != null)
                    {
                        var parent = toBeExploded.Parent as SnailPair;

                        var l = parent.Left;
                        while (l != null && l is SnailPair p)
                        {
                            l = (p.Parent as SnailPair)?.Left;
                        }
                        if (l != null)
                        {
                            var p = l as SnailValue;
                            p.Value += ((SnailValue)toBeExploded.Left).Value;
                        }

                        var r = parent.Right;
                        while (r != null && r is SnailPair p)
                        {
                            r = (p.Parent as SnailPair)?.Right;
                        }
                        if (r != null)
                        {
                            var p = r as SnailValue;
                            p.Value += ((SnailValue)toBeExploded.Right).Value;
                        }
                        hasChanged = true;
                    }

                    /*while (true)
                    {
                        if (n is SnailPair pair)
                        {
                            var a = pair.Left;
                            if (a.Level >= 4)
                            {
                                //explode
                            }
                        }
                    }
                    if (hasChanged)
                    {
                        continue;
                    }*/
                }

                return n;
            }
        }

        private class SnailPair : SnailNumber
        {
            public SnailPair(SnailNumber left, SnailNumber right)
            {
                Left = left;
                Left.Side = eSide.Left;
                Left.Parent = this;

                Right = right;
                Right.Side = eSide.Right;
                Right.Parent = this;
            }

            public SnailNumber Left { get; set; }
            public SnailNumber Right { get; set; }

            public override long GetMagnitude()
            {
                return 3 * Left.GetMagnitude() + 2 * Right.GetMagnitude();
            }

            public override string ToString()
            {
                return "[" + Left.ToString() + "," + Right.ToString() + "]";
            }
        }

        private class SnailValue : SnailNumber
        {
            public SnailValue(int value)
            {
                Value = value;
            }

            public int Value { get; set; }

            public override long GetMagnitude()
            {
                return Value;
            }

            public override string ToString()
            {
                return Value.ToString();
            }
        }
    }
}