using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Y2017
{
    internal class Day14 : GenericDay
    {
        public string Compute1(params string[] input)
        {
            var res = 0;
            for (int i = 0; i <= 127; i++)
            {
                var hash = KnotHash.CalcAsRaw(input[0] + "-" + i);
                res += hash.Select(x => Convert.ToString(x, 2)).Join("").Count(x => x == '1');
            }
            return res.ToString();
        }

        public string Compute2(params string[] input)
        {
            int[,] map = new int[128, 128];
            for (int i = 0; i <= 127; i++)
            {
                var hash = KnotHash.CalcAsRaw(input[0] + "-" + i);
                for (int j = 0; j < hash.Length; j++)
                {
                    map[i, j] = hash[j];
                }
            }
            var nbGroups = 0;
            Console.WriteLine();
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    Console.Write(map[i, j] == 1 ? "#" : ".");
                }
                Console.WriteLine();
            }

            Console.ReadKey();
            foreach (var coordinate in map.GetAllCoordinates())
            {
                if (map[coordinate.Row, coordinate.Col] == 1)
                {
                    var q = new Queue<Point2d>();
                    q.Enqueue(coordinate);
                    while (q.Count > 0)
                    {
                        var c = q.Dequeue();
                        var ns = GetAllNeighboors(c);
                        foreach (var n in ns)
                        {
                            if (0 <= n.Col && n.Col <= map.GetLength(0)
                               && 0 <= n.Row && n.Row <= map.GetLength(1))
                            {
                                if (q.Contains(n))
                                {
                                    continue;
                                }
                                if (map[n.Row, n.Col] == 1)
                                {
                                    q.Enqueue(n);
                                    map[n.Row, n.Col] = 10;
                                }
                            }
                        }
                    }
                    nbGroups++;
                    //foreach(var neighbbour in GetAllNeighboors(coordinate))
                    //{
                    //    if(0<=neighbbour.Col && neighbbour.Col<=map.GetLength(0)
                    //        && 0 <= neighbbour.Row && neighbbour.Row <= map.GetLength(1))
                    //    {
                    //        if(map[neighbbour.Row, neighbbour.Col]==1)
                    //        {
                    //            map[neighbbour.Row, neighbbour.Col] = 10;
                    //        }
                    //    }
                    //}
                }
            }
            return nbGroups.ToString();
        }

        private static IEnumerable<Point2d> GetAllNeighboors(Point2d p)
        {
            throw new Exception();
            for (int i = -1; i < 1; i++)
            {
                for (int j = -1; j < 1; j++)
                {
                    if (i != 0 || j != 0)
                        yield return new Point2d(p.Row + i, p.Col + j);
                }
            }
        }
    }
}