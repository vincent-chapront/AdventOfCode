using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Text;

namespace AdventOfCode.Y2017
{
    internal class Day19 : GenericDay
    {
        public string Compute1(params string[] input)
        {
            return Compute(input).Item1;
        }
        public string Compute2(params string[] input)
        {
            return Compute(input).Item2.ToString();
        }

        private static (string, int) Compute(string[] input)
        {
            var map = input.Select(x => x.ToArray()).ToArray();
            var currentPoint = new Point2d(0, input[0].IndexOf("|"));

            bool done = false;
            var result = new StringBuilder();

            var direction = new Point2d(1, 0);
            int count = 0;
            while (!done)
            {
                count++;
                if (
                    currentPoint.Row < 0
                    || currentPoint.Row >= input.Length
                    || currentPoint.Col < 0
                    || currentPoint.Row >= input[currentPoint.Row].Length
                    || input[currentPoint.Row][currentPoint.Col] == ' '
                    )
                {
                    done = true;
                    break;
                }
                var currentChar = input[currentPoint.Row][currentPoint.Col];

                if ('A' <= currentChar && currentChar <= 'Z')
                {
                    result.Append(currentChar);
                }
                else
                {
                    if (currentChar == '+')
                    {
                        if (direction.Row == 1 || direction.Row == -1)
                        {
                            if (currentPoint.Col + 1 < input[currentPoint.Row].Length && input[currentPoint.Row][currentPoint.Col + 1] != ' ')
                            {
                                direction = new Point2d(0, 1);
                            }
                            else
                            {
                                direction = new Point2d(0, -1);
                            }
                        }
                        else
                        {
                            if (currentPoint.Row + 1 < input.Length && input[currentPoint.Row + 1][currentPoint.Col] != ' ')
                            {
                                direction = new Point2d(1, 0);
                            }
                            else
                            {
                                direction = new Point2d(-1, 0);
                            }
                        }
                    }
                }
                currentPoint = new Point2d(currentPoint.Row + direction.Row, currentPoint.Col + direction.Col);
            }

            return (result.ToString(), count - 1);
        }
    }
}