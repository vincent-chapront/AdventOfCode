using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode.Y2021
{
    internal class Day11 : GenericDay
    {
        public string Compute1(params string[] input)
        {
            var map = new int[input.Length, input[0].Length];
            foreach (var point in GetAllCoordinate(map.GetLength(0), map.GetLength(1)))
            {
                map[point.Row, point.Col] = int.Parse(input[point.Row][point.Col].ToString());
            }

            int numberOfFlash = 0;
            for (int i = 1; i <= 100; i++)
            {
                int numberOfFlashPerIteration;
                (map, numberOfFlashPerIteration) = Iteration(map);
                numberOfFlash += numberOfFlashPerIteration;
            }
            return numberOfFlash.ToString();
        }

        public string Compute2(params string[] input)
        {
            var map = new int[input.Length, input[0].Length];
            foreach (var point in GetAllCoordinate(map.GetLength(0), map.GetLength(1)))
            {
                map[point.Row, point.Col] = int.Parse(input[point.Row][point.Col].ToString());
            }
            int size = map.GetLength(0) * map.GetLength(1);

            int numberOfFlashPerIteration = 0;
            for (int i = 1; numberOfFlashPerIteration != size; i++)
            {
                (map, numberOfFlashPerIteration) = Iteration(map);
                if (numberOfFlashPerIteration == size)
                {
                    return i.ToString();
                }
            }
            return "ERROR";
        }

        private static IEnumerable<Point2d> GetAllCoordinate<T>(T[,] map)
        {
            return GetAllCoordinate(map.GetLength(0), map.GetLength(1));
        }

        private static IEnumerable<Point2d> GetAllCoordinate(int maxRow, int maxCol)
        {
            for (int row = 0; row < maxRow; row++)
            {
                for (int col = 0; col < maxCol; col++)
                {
                    yield return new Point2d(row, col);
                }
            }
        }

        private static IEnumerable<Point2d> GetSurrounding(Point2d current, int maxRow, int maxCol)
        {
            for (int row = -1; row <= 1; row++)
            {
                for (int col = -1; col <= 1; col++)
                {
                    if (row != 0 || col != 0)
                    {
                        var newRow = current.Row + row;
                        var newCol = current.Col + col;
                        if (0 <= newRow && newRow < maxRow
                            && 0 <= newCol && newCol < maxCol
                            )
                            yield return new Point2d(newRow, newCol);
                    }
                }
            }
        }

        private static (int[,], int numberOfFlash) Iteration(int[,] map)
        {
            var numberOfFlashPerIteration = 0;
            int maxRow = map.GetLength(0);
            int maxCol = map.GetLength(1);

            var s = GetSurrounding(new Point2d(1, 5), maxRow, maxCol).ToList();
            var newMap = new int[maxRow, maxCol];

            var flashed = new Queue<Point2d>();

            foreach (var point in GetAllCoordinate(map))
            {
                newMap[point.Row, point.Col] = map[point.Row, point.Col] + 1;
                if (newMap[point.Row, point.Col] == 10)
                {
                    numberOfFlashPerIteration++;
                    flashed.Enqueue(new Point2d(point.Row, point.Col));
                }
            }

            while (flashed.Count > 0)
            {
                var current = flashed.Dequeue();

                foreach (var point in GetSurrounding(current, maxRow, maxCol))
                {
                    newMap[point.Row, point.Col] = newMap[point.Row, point.Col] + 1;
                    if (newMap[point.Row, point.Col] == 10)
                    {
                        numberOfFlashPerIteration++;
                        flashed.Enqueue(point);
                    }
                }
            }

            foreach (var point in GetAllCoordinate(map))
            {
                if (newMap[point.Row, point.Col] > 9)
                {
                    newMap[point.Row, point.Col] = 0;
                }
            }
            return (newMap, numberOfFlashPerIteration);
        }

        /*static void Display(int[,] map)
        {
            foreach (var point in GetAllCoordinate(map.GetLength(0), map.GetLength(1)))
            {
                WriteNumber(point, map);
            }
        }

        private static void WriteNumber(Point2d point, int[,] map)
        {
            Console.SetCursorPosition(point.Col, point.Row + 1);
            var number = map[point.Row, point.Col];
            if (number > 9)
            {
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.White;
                number = 0;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Black;
            }
            Console.Write(number);
        }*/
    }
}