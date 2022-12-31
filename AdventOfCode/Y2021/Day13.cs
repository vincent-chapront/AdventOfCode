using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode.Y2021
{
    internal class Day13 : GenericDay
    {
        public string Compute1(string[] input, string args)
        {
            (List<Point2d> points, List<(string, int)> folds) = Parse(input);
            points = Fold(points, folds.Take(1).ToList());

            return points.Count.ToString();
        }

        public string Compute2(string[] input, string args)
        {
            (List<Point2d> points, List<(string, int)> folds) = Parse(input);
            points = Fold(points, folds.ToList());
            Display(points);
            return "DISPLAY";
        }

        private static void Display(List<Point2d> points)
        {
            Console.WriteLine();
            Console.WriteLine("---------------------------");
            var height = points.Max(x => x.Row);
            var top = Console.CursorTop;
            foreach (var p in points)
            {
                Console.SetCursorPosition(p.Col, p.Row + top + 1);
                Console.Write("#");
            }
            Console.SetCursorPosition(0, top + height + 3);
            Console.WriteLine("---------------------------");
        }

        private static List<Point2d> Fold(List<Point2d> points, List<(string, int)> folds)
        {
            foreach ((string axis, int value) in folds)
            {
                points = points.Select(x =>
                {
                    if (axis == "x") { return new Point2d(x.Row, value - Math.Abs(x.Col - value)); }
                    else
                    {
                        return new Point2d(value - Math.Abs(x.Row - value), x.Col);
                    }
                })
                .Distinct()
                .ToList();
            }

            return points;
        }

        private static (List<Point2d> points, List<(string, int)> folds) Parse(string[] input)
        {
            int i = 0;
            var points = new List<Point2d>();
            for (; !string.IsNullOrWhiteSpace(input[i]); i++)
            {
                var a = input[i].Split(",").Select(x => int.Parse(x)).ToArray();
                points.Add(new Point2d(a[1], a[0]));
            }

            i++;
            var folds = new List<(string axis, int value)>();
            for (; i < input.Length; i++)
            {
                var a = input[i].Replace("fold along ", "").Split("=");
                folds.Add((a[0], int.Parse(a[1])));
            }

            return (points, folds);
        }
    }
}