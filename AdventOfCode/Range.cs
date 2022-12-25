namespace AdventOfCode
{
    public class Range
    {
        public Range(int value1, int value2)
        {
            if (value1 < value2)
            {
                Min = value1;
                Max = value2;
            }
            else
            {
                Min = value2;
                Max = value1;
            }
        }

        public int Max { get; }
        public int Min { get; }

        public bool IsInRange(int value)
        {
            return Min <= value && Max >= value;
        }

        public override string ToString()
        {
            return $"{Min};{Max}";
        }
    }
}