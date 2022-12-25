using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Y2016
{
    internal class Day13 : GenericDay
    {
        protected override object Part1()
        {
            GenerateMaze(10);
            throw new NotImplementedException();
        }

        protected override object Part2()
        {
            throw new NotImplementedException();
        }

        private static long Compute1(int input)
        {
            var maze = new Dictionary<(int, int), bool> { { (1, 1), true } };
            var visited = new Dictionary<(int, int), int>();

            var toVisit = new Queue<(int x, int y, int distance)>();
            toVisit.Enqueue((1, 1, 1));
            while (toVisit.Count > 0)
            {
                var currentPosition = toVisit.Dequeue();

                if (currentPosition.x == 7 && currentPosition.y == 4)
                {
                    return currentPosition.distance;
                }
                if (currentPosition.x > 1)
                {
                    if (GetMazeSpot(maze, input, currentPosition.x - 1, currentPosition.y))
                    {
                        if (visited.ContainsKey((currentPosition.x - 1, currentPosition.y)))
                        {
                            visited[(currentPosition.x - 1, currentPosition.y)] = currentPosition.distance + 1;
                        }
                        else
                        {
                            visited.Add((currentPosition.x - 1, currentPosition.y), currentPosition.distance + 1);
                        }
                        toVisit.Enqueue((currentPosition.x - 1, currentPosition.y, currentPosition.distance + 1));
                    }
                }
                if (currentPosition.y > 1)
                {
                    if (GetMazeSpot(maze, input, currentPosition.x, currentPosition.y - 1))
                    {
                        if (visited.ContainsKey((currentPosition.x, currentPosition.y - 1)))
                        {
                            visited[(currentPosition.x, currentPosition.y - 1)] = currentPosition.distance + 1;
                        }
                        else
                        {
                            visited.Add((currentPosition.x, currentPosition.y - 1), currentPosition.distance + 1);
                        }
                        toVisit.Enqueue((currentPosition.x, currentPosition.y - 1, currentPosition.distance + 1));
                    }
                }
                if (GetMazeSpot(maze, input, currentPosition.x, currentPosition.y + 1))
                {
                    if (visited.ContainsKey((currentPosition.x, currentPosition.y + 1)))
                    {
                        visited[(currentPosition.x, currentPosition.y + 1)] = currentPosition.distance + 1;
                    }
                    else
                    {
                        visited.Add((currentPosition.x, currentPosition.y + 1), currentPosition.distance + 1);
                    }
                    toVisit.Enqueue((currentPosition.x, currentPosition.y + 1, currentPosition.distance + 1));
                }

                if (GetMazeSpot(maze, input, currentPosition.x + 1, currentPosition.y))
                {
                    if (visited.ContainsKey((currentPosition.x+ 1, currentPosition.y)))
                    {
                        visited[(currentPosition.x + 1, currentPosition.y)] = currentPosition.distance + 1;
                    }
                    else
                    {
                        visited.Add((currentPosition.x + 1, currentPosition.y), currentPosition.distance + 1);
                    }
                    toVisit.Enqueue((currentPosition.x + 1, currentPosition.y, currentPosition.distance + 1));
                }

                toVisit = new Queue<(int x, int y, int distance)>(toVisit.OrderBy(x => x.distance).ToList());
            }

            return 0;
        }

        public string Compute2(params string[] input)
        {
            throw new NotImplementedException();
        }

        private static int CountNumberBit1(int no)
        {
            int count = 0;
            if (no <= 0)
                throw new Exception("No. should be greater than 0");
            while (no > 0)
            {
                if (no % 2 != 0)
                    count++;
                no /= 2;
            }
            return count;
        }

        private static void GenerateMaze(int input)
        {
            Console.WriteLine();
            for (int y = 0; y < 10; y++)
            {
                for (int x = 0; x < 10; x++)
                {
                    var a = x * x + 3 * x + 2 * x * y + y + y * y + input;
                    if (CountNumberBit1(a) % 2 == 0)
                    {
                        Console.Write(".");
                    }
                    else
                    {
                        Console.Write("#");
                    }
                }
                Console.WriteLine();
            }
        }

        private static bool GetMazeSpot(Dictionary<(int, int), bool> maze, int input, int x, int y)
        {
            if (!maze.ContainsKey((x, y)))
            {
                var a = x * x + 3 * x + 2 * x * y + y + y * y + input;
                var isEmpty = CountNumberBit1(a) % 2 == 0;
                maze.Add((x, y), isEmpty);
            }

            return maze[(x, y)];
        }
    }
}