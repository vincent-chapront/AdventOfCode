using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode.Y2020
{
    internal class Day17 : GenericDay
    {
        public string Compute1(params string[] input)
        {
            var dimension = new List<Point3d>();
            for (int row = 0; row < input.Length; row++)
            {
                for (int col = 0; col < input[row].Length; col++)
                {
                    if (input[row][col] == '#')
                    {
                        dimension.Add(new Point3d(row - 1, col - 1, 0));
                    }
                }
            }

            for (int i = 0; i < 6; i++)
            {
                dimension = Iteration(dimension);
            }

            return dimension.Count.ToString();
        }

        public string Compute2(params string[] input)
        {
            var dimension = new List<Point4d>();
            for (int row = 0; row < input.Length; row++)
            {
                for (int col = 0; col < input[row].Length; col++)
                {
                    if (input[row][col] == '#')
                    {
                        dimension.Add(new Point4d(row - 1, col - 1, 0, 0));
                    }
                }
            }

            for (int i = 0; i < 6; i++)
            {
                dimension = Iteration(dimension);
            }

            return dimension.Count.ToString();
        }

        private static IEnumerable<Point3d> GetSurroundings(Point3d p)
        {
            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    for (int z = -1; z <= 1; z++)
                    {
                        if (x != 0 || y != 0 || z != 0)
                        {
                            yield return new Point3d(p.X + x, p.Y + y, p.Z + z);
                        }
                    }
                }
            }
        }

        private static IEnumerable<Point4d> GetSurroundings(Point4d p)
        {
            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    for (int z = -1; z <= 1; z++)
                    {
                        for (int w = -1; w <= 1; w++)
                        {
                            if (x != 0 || y != 0 || z != 0 || w != 0)
                            {
                                yield return new Point4d(p.X + x, p.Y + y, p.Z + z, p.W + w);
                            }
                        }
                    }
                }
            }
        }

        private static List<Point3d> Iteration(List<Point3d> dimension)
        {
            var toBeChecked = new List<Point3d>(dimension);
            foreach (var point in dimension)
            {
                var potential = GetSurroundings(point);
                foreach (var p in potential)
                {
                    if (!toBeChecked.Contains(p))
                    {
                        toBeChecked.Add(p);
                    }
                }
            }

            var newDimension = new List<Point3d>();
            foreach (var point in toBeChecked)
            {
                var surroundings = GetSurroundings(point).ToList();
                var count = surroundings.Where(x => dimension.Contains(x)).ToList();
                if (dimension.Contains(point))
                {
                    if (count.Count == 2 || count.Count == 3)
                    {
                        newDimension.Add(point);
                    }
                }
                else
                {
                    if (count.Count == 3)
                    {
                        newDimension.Add(point);
                    }
                }
            }

            return newDimension;
        }

        private static List<Point4d> Iteration(List<Point4d> dimension)
        {
            var toBeChecked = new List<Point4d>(dimension);
            foreach (var point in dimension)
            {
                var potential = GetSurroundings(point);
                foreach (var p in potential)
                {
                    if (!toBeChecked.Contains(p))
                    {
                        toBeChecked.Add(p);
                    }
                }
            }

            var newDimension = new List<Point4d>();
            foreach (var point in toBeChecked)
            {
                var surroundings = GetSurroundings(point).ToList();
                var count = surroundings.Where(x => dimension.Contains(x)).ToList();
                if (dimension.Contains(point))
                {
                    if (count.Count == 2 || count.Count == 3)
                    {
                        newDimension.Add(point);
                    }
                }
                else
                {
                    if (count.Count == 3)
                    {
                        newDimension.Add(point);
                    }
                }
            }

            return newDimension;
        }

        private class Point3d : IEquatable<Point3d>
        {
            public Point3d(int x, int y, int z)
            {
                X = x;
                Y = y;
                Z = z;
            }

            public int X { get; }
            public int Y { get; }
            public int Z { get; }

            public bool Equals(Point3d other)
            {
                return null != other && X == other.X && Y == other.Y && Z == other.Z;
            }

            public override bool Equals(object obj)
            {
                return Equals(obj as Point3d);
            }

            public override int GetHashCode()
            {
                return ("X" + X + "-Y" + Y + "-Z" + Z).GetHashCode();
            }

            public override string ToString()
            {
                return $"({X}, {Y}, {Z}";
            }
        }

        private class Point4d : IEquatable<Point4d>
        {
            public Point4d(int x, int y, int z, int w)
            {
                X = x;
                Y = y;
                Z = z;
                W = w;
            }

            public int W { get; }
            public int X { get; }
            public int Y { get; }
            public int Z { get; }

            public bool Equals(Point4d other)
            {
                return null != other && X == other.X && Y == other.Y && Z == other.Z && W == other.W;
            }

            public override bool Equals(object obj)
            {
                return Equals(obj as Point4d);
            }

            public override int GetHashCode()
            {
                return ("X" + X + "-Y" + Y + "-Z" + Z + "-W" + W).GetHashCode();
            }

            public override string ToString()
            {
                return $"({X}, {Y}, {Z}, {W})";
            }
        }
    }
}