using System;

namespace AdventOfCode
{
    public class Point2dXY: IEquatable<Point2d>
    {
        public int Y;
        public int X;

        public Point2dXY(int x, int y)
        {
            X = x;
            Y = y;
        }

        public bool Equals(Point2d other)
        {
            return Y == other.Col && X == other.Row;
        }

        public override string ToString()
        {
            return $"({X},{Y})";
        }
        public override bool Equals(object obj)
        {
            var other = obj as Point2dXY;
            if (other == null)
            {
                return false;
            }
            return Y == other.Y && X == other.X;
        }
        public override int GetHashCode()
        {
            return $"{X}-{Y}".GetHashCode();
        }
    }

}