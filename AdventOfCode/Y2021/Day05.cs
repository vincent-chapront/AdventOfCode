using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Y2021
{
    internal class Day05 : GenericDay
    {
        public string Compute1(string[] input, string args)
        {
            var lines = input.Select(x => new Line(x)).Where(x => x.IsOrthogonal()).ToList();
            var dico = new Dictionary<(int x, int y), int>();
            foreach (var line in lines)
            {
                var points = line.GetPoints();

                foreach (var point in points)
                {
                    if (!dico.ContainsKey((point.X, point.Y)))
                    {
                        dico.Add((point.X, point.Y), 0);
                    }
                    dico[(point.X, point.Y)]++;
                }
            }
            //Draw(dico);

            return dico.Count(x => x.Value > 1).ToString();
        }

        public string Compute2(string[] input, string args)
        {
            var lines = input.Select(x => new Line(x)).ToList();
            var dico = new Dictionary<(int x, int y), int>();
            foreach (var line in lines)
            {
                var points = line.GetPoints();

                foreach (var point in points)
                {
                    if (!dico.ContainsKey((point.X, point.Y)))
                    {
                        dico.Add((point.X, point.Y), 0);
                    }
                    dico[(point.X, point.Y)]++;
                }
            }

            //Draw(dico);

            return dico.Count(x => x.Value > 1).ToString();
        }

        private static void Draw(Dictionary<(int x, int y), int> dico)
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (dico.ContainsKey((j, i)))
                    {
                        System.Console.Write(dico[(j, i)]);
                    }
                    else
                    {
                        System.Console.Write(".");
                    }
                }
                System.Console.WriteLine();
            }
        }

        private class Line
        {
            public Line(string s)
            {
                var points = s.Split(" -> ").Select(x => new Point(x)).ToArray();
                Start = points[0];
                End = points[1];
            }

            public Point End { get; }
            public Point Start { get; }

            public List<Point> GetPoints()
            {
                if (IsVertical())
                {
                    return GetVerticalPoints(this);
                }
                if (IsHorizontal())
                {
                    return GeHorizontalPoints(this);
                }
                if (IsDiagonalAsc())
                {
                    return GetDiagonalAscPoints(this);
                }
                if (IsDiagonalDesc())
                {
                    return GetDiagonalDescPoints(this);
                }
                return new List<Point>();
            }

            public bool IsOrthogonal()
            {
                return IsVertical() || IsHorizontal();
            }

            public override string ToString()
            {
                return $"{Start} -> {End}";
            }

            private static List<Point> GeHorizontalPoints(Line line)
            {
                var points = new List<Point>();
                Point start;
                Point end;
                if (line.Start.X < line.End.X)
                {
                    start = line.Start;
                    end = line.End;
                }
                else
                {
                    start = line.End;
                    end = line.Start;
                }
                for (int x = start.X; x <= end.X; x++)
                {
                    points.Add(new Point(x, start.Y));
                }
                return points;
            }

            private static List<Point> GetDiagonalAscPoints(Line line)
            {
                var points = new List<Point>();
                Point start;
                Point end;
                if (line.Start.X < line.End.X)
                {
                    start = line.Start;
                    end = line.End;
                }
                else
                {
                    start = line.End;
                    end = line.Start;
                }
                for (int i = 0; i <= end.X - start.X; i++)
                {
                    points.Add(new Point(start.X + i, start.Y - i));
                }
                return points;
            }

            private static List<Point> GetDiagonalDescPoints(Line line)
            {
                var points = new List<Point>();
                Point start;
                Point end;
                if (line.Start.Y < line.End.Y)
                {
                    start = line.Start;
                    end = line.End;
                }
                else
                {
                    start = line.End;
                    end = line.Start;
                }
                for (int i = 0; i <= end.X - start.X; i++)
                {
                    points.Add(new Point(start.X + i, start.Y + i));
                }
                return points;
            }

            private static List<Point> GetVerticalPoints(Line line)
            {
                var points = new List<Point>();
                Point start;
                Point end;
                if (line.Start.Y < line.End.Y)
                {
                    start = line.Start;
                    end = line.End;
                }
                else
                {
                    start = line.End;
                    end = line.Start;
                }
                for (int y = start.Y; y <= end.Y; y++)
                {
                    points.Add(new Point(start.X, y));
                }
                return points;
            }

            private bool IsDiagonalAsc()
            {
                return (Start.X < End.X && Start.Y > End.Y) || (Start.X > End.X && Start.Y < End.Y);
            }

            private bool IsDiagonalDesc()
            {
                return (Start.X > End.X && Start.Y > End.Y) || (Start.X < End.X && Start.Y < End.Y);
            }

            private bool IsHorizontal()
            {
                return Start.Y == End.Y;
            }

            private bool IsVertical()
            {
                return Start.X == End.X;
            }
        }

        private class Point
        {
            public Point(string point)
            {
                var xy = point.Split(",").Select(x => int.Parse(x)).ToArray();
                X = xy[0];
                Y = xy[1];
            }

            public Point(int x, int y)
            {
                X = x;
                Y = y;
            }

            public int X { get; }
            public int Y { get; }

            public override string ToString()
            {
                return $"({X},{Y})";
            }
        }
    }
}