using System;

namespace AdventOfCode
{
    public struct Point3d
    {
        public int X;
        public int Y;
        public int Z;

        public Point3d(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public override bool Equals(object obj)
        {
            return obj is Point3d d &&
                   X == d.X &&
                   Y == d.Y &&
                   Z == d.Z;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y, Z);
        }

        public override string ToString()
        {
            return $"({X},{Y},{Z})";
        }
    }
}