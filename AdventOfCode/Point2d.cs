namespace AdventOfCode
{
    public class Point2d
    {
        public int Col;
        public int Row;

        public Point2d(int row, int col)
        {
            Row = row;
            Col = col;
        }

        public override string ToString()
        {
            return $"({Row},{Col})";
        }
    }
}