using System;

namespace AdventOfCode
{
    public class Point2d: IEquatable<Point2d>
    {
        public int Col;
        public int Row;

        public Point2d(int row, int col)
        {
            Row = row;
            Col = col;
        }

        public bool Equals(Point2d other)
        {
            return Col == other.Col && Row == other.Row;
        }

        public override string ToString()
        {
            return $"({Row},{Col})";
        }
        public override bool Equals(object obj)
        {
            var other = obj as Point2d;
            if (other == null)
            {
                return false;
            }
            return Col == other.Col && Row == other.Row;
        }
        public override int GetHashCode()
        {
            return $"{Col}-{Row}".GetHashCode();
        }
    }

}