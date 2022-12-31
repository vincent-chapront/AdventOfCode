namespace AdventOfCode
{
    public class Interval<T>
    {
        public T start, end;
        public Interval(T start, T end)
        {
            this.start = start;
            this.end = end;
        }
        public override string ToString()
        {
            return start + " <> " + end;
        }
    }
}