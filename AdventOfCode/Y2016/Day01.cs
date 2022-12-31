using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode.Y2016
{
    internal class Day01 : GenericDay
    {

        private enum Directions
        {
            North,
            South,
            East,
            West
        }

        public string Compute1(string[] input, string args)
        {
            var visited = GetVisited(input[0]);
            return (Math.Abs(visited[visited.Count - 1].Item1) + Math.Abs(visited[visited.Count - 1].Item2)).ToString();
            
        }

        public string Compute2(string[] input, string args)
        {
            var direction = Directions.North;
            var steps = input[0].Split(", ");
            var x = 0;
            var y = 0;
            var visited = new List<(int, int)>();
            foreach (var step in steps)
            {
                direction = direction switch
                {
                    Directions.North => step.StartsWith('R') ? Directions.East : Directions.West,
                    Directions.South => step.StartsWith('R') ? Directions.West : Directions.East,
                    Directions.East => step.StartsWith('R') ? Directions.South : Directions.North,
                    Directions.West => step.StartsWith('R') ? Directions.North : Directions.South,
                    _ => throw new NotSupportedException(),
                };

                var length = int.Parse(step.Substring(1));
                for (int i = 0; i < length; i++)
                {
                    switch (direction)
                    {
                        case Directions.North:
                            y++;
                            break;

                        case Directions.South:
                            y--;
                            break;

                        case Directions.East:
                            x++;
                            break;

                        case Directions.West:
                            x--;
                            break;
                    }
                    foreach (var v in visited)
                    {
                        if (v.Item1 == x && v.Item2 == y)
                        {
                            return (Math.Abs(x) + Math.Abs(y)).ToString();
                        }
                    }
                    visited.Add((x, y));
                }
            }

            return "ERROR";
        }

        private static List<(int, int)> GetVisited(string input)
        {
            var direction = Directions.North;
            var steps = input.Split(", ");
            var x = 0;
            var y = 0;
            var visited = new List<(int, int)>();
            foreach (var step in steps)
            {
                direction = direction switch
                {
                    Directions.North => step.StartsWith('R') ? Directions.East : Directions.West,
                    Directions.South => step.StartsWith('R') ? Directions.West : Directions.East,
                    Directions.East => step.StartsWith('R') ? Directions.South : Directions.North,
                    Directions.West => step.StartsWith('R') ? Directions.North : Directions.South,
                    _ => throw new NotSupportedException(),
                };

                var length = int.Parse(step.Substring(1));
                for (int i = 0; i < length; i++)
                {
                    switch (direction)
                    {
                        case Directions.North:
                            y++;
                            break;

                        case Directions.South:
                            y--;
                            break;

                        case Directions.East:
                            x++;
                            break;

                        case Directions.West:
                            x--;
                            break;
                    }
                    visited.Add((x, y));
                }
            }

            return visited;
        }
    }
}