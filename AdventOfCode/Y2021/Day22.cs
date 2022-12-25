using System;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode.Y2021
{
    internal class Day22 : GenericDay
    {
        private static Regex regexParse = new Regex(@"(on|off) x=([\-\d]+)\.\.([\-\d]+),y=([\-\d]+)\.\.([\-\d]+),z=([\-\d]+)\.\.([\-\d]+)");

        protected override object Part1()
        {
            Assert.AreEqual(39, Compute1(Resources.Year2021.Day22Test2.ToLines()));
            Assert.AreEqual(590784, Compute1(Resources.Year2021.Day22Test1.ToLines()));
            var res = Compute1(Resources.Year2021.Day22.ToLines());
            return res;
        }

        protected override object Part2()
        {
            throw new NotImplementedException();
        }

        public string Compute1(params string[] input)
        {
            var validRange = new Range(-50, 50);
            var ranges = input.Select(x => Parse(x)).ToList();

            /*var onPoint = new List<Point3d>();
            foreach(var r in ranges)
            {
                for (int i = r.RangeX.Min; i <= r.RangeX.Max && validRange.IsInRange(i); i++)
                {
                    for (int j = r.RangeY.Min; j <= r.RangeY.Max && validRange.IsInRange(j); j++)
                    {
                        for (int k = r.RangeZ.Min; k <= r.RangeZ.Max && validRange.IsInRange(k); k++)
                        {
                            var p = new Point3d(i, j, k);
                            if (r.IsOn)
                            {
                                if (!onPoint.Contains(p))
                                {
                                    onPoint.Add(p);
                                }
                            }
                            else
                            {
                                if (onPoint.Contains(p))
                                {
                                    onPoint.Remove(p);
                                }
                            }
                        }
                    }
                }
            }*/

            return "ERROR";
        }

        private static Ranges Parse(string line)
        {
            var m = regexParse.Match(line).Groups;
            return new Ranges(
                m[1].Value == "on",
                new Range(int.Parse(m[2].Value), int.Parse(m[3].Value)),
                new Range(int.Parse(m[4].Value), int.Parse(m[5].Value)),
                new Range(int.Parse(m[6].Value), int.Parse(m[7].Value))

                );
        }

        private class Ranges
        {
            public bool IsOn;
            public Range RangeX;
            public Range RangeY;
            public Range RangeZ;

            public Ranges(bool isOn, Range rangeX, Range rangeY, Range rangeZ)
            {
                IsOn = isOn;
                RangeX = rangeX;
                RangeY = rangeY;
                RangeZ = rangeZ;
            }

            public long Number =>
                (RangeX.Max - RangeX.Min + 1L)
                * (RangeY.Max - RangeY.Min + 1L)
                * (RangeZ.Max - RangeZ.Min + 1L);

            public override string ToString()
            {
                return (IsOn ? "On" : "Off") + " " + RangeX + " - " + RangeY + " - " + RangeZ;
            }
        }
    }
}