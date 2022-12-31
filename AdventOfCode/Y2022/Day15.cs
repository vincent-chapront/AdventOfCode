using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Y2022
{
    internal class Day15 : GenericDay
    {
        public string Compute1(string[] input, string args)
        {
            var targetY = Convert.ToInt32(args);
            var interval = new Interval<int>(int.MaxValue, int.MinValue);

            List<(Point2dXY sensor, Point2dXY beacon)> pairs = Parse(input);
            foreach (var (sensor, beacon) in pairs)
            {
                var manhattanDistance = Math.Abs(beacon.X - sensor.X) + Math.Abs(beacon.Y - sensor.Y);

                var yDiff = Math.Abs(sensor.Y - targetY);
                var a = manhattanDistance - yDiff;
                if (a > 0)
                {
                    interval.start = Math.Min(interval.start, sensor.X - a);
                    interval.end = Math.Max(interval.end, sensor.X + a);
                }
            }

            var sensors = pairs.Select(x => x.sensor).Distinct().ToList();
            var beacons = pairs.Select(x => x.beacon).Distinct().ToList();
            return (interval.end - interval.start + 1 - sensors.Count(x => x.Y == targetY) - beacons.Count(x => x.Y == targetY)).ToString();
        }

        public string Compute2(string[] input, string args)
        {
            var maxDimension = Convert.ToInt32(args);
            var dico = new Dictionary<int, List<Interval<int>>>();
            for (int i = 0; i <= maxDimension; i++)
            {
                dico.Add(i, new List<Interval<int>>());
            }

            List<(Point2dXY sensor, Point2dXY beacon)> pairs = Parse(input);
            foreach (var (sensor, beacon) in pairs)
            {
                var manhattanDistance = Math.Abs(beacon.X - sensor.X) + Math.Abs(beacon.Y - sensor.Y);
                for (int i = 0; i <= maxDimension; i++)
                {
                    var yDiff = Math.Abs(sensor.Y - i);
                    var a = manhattanDistance - yDiff;
                    if (a > 0)
                    {
                        dico[i].Add(new Interval<int>(sensor.X - a, sensor.X + a));
                    }
                }
            }

            var d = dico.Select((x, idx) => (MergeOverlappingIntervals.mergeIntervals(x.Value.ToArray()), idx)).Where(x => x.Item1.Length > 1).ToList();
            var y = d[0].idx;
            var x = d[0].Item1[0].end + 1;
            return ((x * 4_000_000L) + y).ToString();
        }

        private static List<(Point2dXY sensor, Point2dXY beacon)> Parse(string[] input)
        {
            var regexParser = new Regex(@"Sensor at x=([\-\d]+), y=([\-\d]+): closest beacon is at x=([\-\d]+), y=([\-\d]+)");
            var res = new List<(Point2dXY sensor, Point2dXY beacon)>();
            foreach (var line in input)
            {
                var r = regexParser.Match(line);

                var sX = Convert.ToInt32(r.Groups[1].ToString());
                var sY = Convert.ToInt32(r.Groups[2].ToString());

                var bX = Convert.ToInt32(r.Groups[3].ToString());
                var bY = Convert.ToInt32(r.Groups[4].ToString());
                res.Add((new Point2dXY(sX, sY), new Point2dXY(bX, bY)));
            }
            return res;
        }
    }

    public class MergeOverlappingIntervals
    {
        // The main function that takes a set of intervals, merges
        // overlapping intervals and prints the result
        public static Interval<int>[] mergeIntervals(Interval<int>[] arr2)
        {
            // Test if the given set has at least one interval
            if (arr2.Length == 0)
            {
                return arr2;
            }

            Array.Sort(arr2, new sortHelper());
            var arr = arr2.Select(x => new Interval<double>(x.start - 0.6, x.end + 0.6)).ToList();
            // Create an empty stack of intervals
            var stack = new Stack<Interval<double>>();

            // Push the first interval to stack
            stack.Push(arr[0]);

            // Start from the next interval and merge if necessary
            for (int i = 1; i < arr.Count; i++)
            {
                // get interval from stack top
                var top = stack.Peek();

                // if current interval is not overlapping with stack top,
                // Push it to the stack
                if (top.end < arr[i].start)
                {
                    stack.Push(arr[i]);
                }

                // Otherwise update the ending time of top if ending of current
                // interval is more
                else if (top.end < arr[i].end)
                {
                    top.end = arr[i].end;
                    stack.Pop();
                    stack.Push(top);
                }
            }

            var res = stack.Select(x => new Interval<int>((int)Math.Ceiling(x.start), (int)Math.Floor(x.end))).ToArray();
            Array.Sort(res, new sortHelper());
            return res;
        }

        // sort the intervals in increasing order of start time
        private class sortHelper : IComparer<Interval<int>>
        {
            public int Compare(Interval<int> x, Interval<int> y)
            {
                if (x.start == y.start)
                {
                    return x.end - y.end;
                }
                return x.start - y.start;
            }
        }
    }
}