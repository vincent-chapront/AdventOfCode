using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Y2022
{
    internal class Day09 : GenericDay
    {
        public string Compute1(string[] input, string args)
        {
            return Compute(input, 2);
        }

        public string Compute2(string[] input, string args)
        {
            return Compute(input, 10);
        }

        private static string Compute(string[] input, int size)
        {
            var knots = new Knot[size];
            knots[knots.Length - 1] = new Knot { Position = new Point2d(0, 0) };
            for (int i = knots.Length - 2; i >= 0; i--)
            {
                knots[i] = new Knot { Position = new Point2d(0, 0), Next = knots[i + 1] };
            }

            var visitedByTail = new List<string>();
            visitedByTail.Add(knots.Last().Position.ToString());

            foreach (var line in input)
            {
                var a = line.Split(" ");
                var dir = a[0];
                var distance = Convert.ToInt32(a[1]);
                for (int i = 0; i < distance; i++)
                {
                    if (dir == "R")
                    {
                        knots[0].Position.Col++;
                    }
                    else if (dir == "L")
                    {
                        knots[0].Position.Col--;
                    }
                    else if (dir == "U")
                    {
                        knots[0].Position.Row++;
                    }
                    else if (dir == "D")
                    {
                        knots[0].Position.Row--;
                    }
                    var current = knots[0];
                    var next = current.Next;
                    while (next != null)
                    {
                        var h = current.Position;
                        var t = next.Position;
                        if (h.Row - 1 <= t.Row && t.Row <= h.Row + 1
                            && h.Col - 1 <= t.Col && t.Col <= h.Col + 1)
                        {
                            current = next;
                            next = current.Next;
                            continue;
                        }

                        if (h.Col == t.Col)
                        {
                            if (h.Row > t.Row)
                            {
                                t.Row++;
                            }
                            else
                            {
                                t.Row--;
                            }
                        }
                        else if (h.Row == t.Row)
                        {
                            if (h.Col > t.Col)
                            {
                                t.Col++;
                            }
                            else
                            {
                                t.Col--;
                            }
                        }
                        else
                        {
                            if (h.Row > t.Row)
                            {
                                t.Row++;
                            }
                            else
                            {
                                t.Row--;
                            }
                            if (h.Col > t.Col)
                            {
                                t.Col++;
                            }
                            else
                            {
                                t.Col--;
                            }
                        }
                        current = next;
                        next = current.Next;
                    }
                    if (!visitedByTail.Contains(knots.Last().Position.ToString()))
                    {
                        visitedByTail.Add(knots.Last().Position.ToString());
                    }
                }
            }
            return visitedByTail.Count.ToString();
        }

        private class Knot
        {
            public Knot Next { get; set; }
            public Point2d Position { get; set; }
        }
    }
}