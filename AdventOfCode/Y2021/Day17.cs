using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Y2021
{
    internal class Day17 : GenericDay
    {
        private static Regex parser = new Regex(@"target area: x=([\-\d]*)\.\.([\-\d]*), y=([\-\d]*)\.\.([\-\d]*)");

        private static (Range rangeX, Range rangeY) Parse(string input)
        {
            var gr = parser.Match(input).Groups;
            return (new Range(int.Parse(gr[1].Value), int.Parse(gr[2].Value)), new Range(int.Parse(gr[3].Value), int.Parse(gr[4].Value)))
            ;
        }

        public string Compute1(params string[] input)
        {
            var (rangeX, rangeY) = Parse(input[0]);
            var y = Math.Abs(rangeY.Min + 1);
            var res = ((y + 1) * y) / 2;
            return res.ToString();
        }

        public string Compute2(params string[] input)
        {
            var (rangeX, rangeY) = Parse(input[0]);
            var yMax = Math.Abs(rangeY.Min);
            var res = 0;
            var possibleShot = new List<(int x, int y)>();
            //for (var velocityY =rangeY.Min; velocityY <= yMax; velocityY++)
            for (var velocityY = 4/*rangeY.Min*/; velocityY <= yMax; velocityY++)
            {
                //var velocityY = 0;
                var nbStep = 0;
                if (velocityY > 0)
                {
                    nbStep = velocityY * 2 + 1;
                }
                var v = 0;
                if (velocityY <= 0) v = -Math.Abs(velocityY) + 1;
                else v = -Math.Abs(velocityY + 1);

                for (int y = 0; y >= rangeY.Min; y += v)
                {
                    if (rangeY.IsInRange(y))
                    {
                        //var acc = (nbStep) / 2.0;
                        //var xmin = (int)Math.Floor((rangeX.Min / (nbStep+1)) + acc);
                        //var xmax = (int)Math.Ceiling((rangeX.Max / (nbStep+1)) + acc);
                        var acc = (nbStep - 1) / 2.0;
                        var xmin = (int)Math.Floor((rangeX.Min / nbStep) + acc);
                        var xmax = (int)Math.Ceiling((rangeX.Max / nbStep) + acc);
                        for (int x = xmin; x <= xmax; x++)
                        {
                            var sum = ((nbStep * (2 * x - nbStep + 1)) / 2);
                            if (rangeX.IsInRange(sum))
                            {
                                if (!possibleShot.Any(a => a.x == x && a.y == velocityY))
                                {
                                    possibleShot.Add((x, velocityY));
                                }
                            }
                        }
                        ;
                    }
                    nbStep += 1;
                    v--;
                }
                //nbStep += 1;
            }
            //var y = Math.Abs(rangeY.Min + 1);
            //var res = ((y + 1) * y) / 2;
            var a = possibleShot.OrderBy(a => a.y).ThenBy(a => a.x).ToList();
            return a.Count.ToString();
        }
    }
}