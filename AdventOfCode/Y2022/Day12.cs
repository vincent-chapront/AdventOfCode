using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Y2022
{
    internal class Day12 : GenericDay
    {
        public string Compute1(string[] input, string args)
        {
            var grid = input.ParseToGrid(x => x);
            Point2d start = null;
            Point2d end = null;
            foreach (var c in grid.GetAllCoordinates())
            {
                if (grid[c.Row, c.Col] == 'S')
                {
                    start = c;
                }
                if (grid[c.Row, c.Col] == 'E')
                {
                    end = c;
                }
            }

            var astar = new AStar(grid, start, end, Heuristics);
            var res = astar.GetPath();

            return (res.Count - 1).ToString();
        }

        public string Compute2(string[] input, string args)
        {
            var grid = input.ParseToGrid(x => x);
            var starts = new List<Point2d>();
            Point2d end = null;
            foreach (var c in grid.GetAllCoordinates())
            {
                if (grid[c.Row, c.Col] == 'S' || grid[c.Row, c.Col] == 'a')
                {
                    starts.Add(c);
                }
                if (grid[c.Row, c.Col] == 'E')
                {
                    end = c;
                }
            }

            var res = int.MaxValue;
            foreach (var start in starts)
            {
                var astar = new AStar(grid, start, end, Heuristics);
                List<Point2d> path = astar.GetPath();
                if (path == null)
                {
                    continue;
                }

                res = Math.Min(res, path.Count - 1);
            }
            return res.ToString();
        }

        private static bool IsAccessible(char from, char to)
        {
            if (from == 'S')
            {
                from = 'a';
            }
            else if (from == 'E')
            {
                from = 'z';
            }

            if (to == 'S')
            {
                to = 'a';
            }
            else if (to == 'E')
            {
                to = 'z';
            }

            return from >= (to - 1);
        }

        private int Heuristics(char[,] grid, Point2d from, Point2d to)
        {
            return
                IsAccessible(grid[from.Row, from.Col], grid[to.Row, to.Col])
                ? 1
                : int.MaxValue;
        }

        private class AStar
        {
            private readonly Point2d from;
            private readonly char[,] grid;
            private readonly Func<char[,], Point2d, Point2d, int> heuristics;
            private readonly Point2d to;

            public AStar(char[,] grid, Point2d from, Point2d to, Func<char[,], Point2d, Point2d, int> heuristics)
            {
                this.grid = grid;
                this.from = from;
                this.to = to;
                this.heuristics = heuristics;
            }

            public List<Point2d> GetPath()
            {
                var openSet = new List<Point2d>
                {
                    from
                };

                var cameFrom = new Dictionary<Point2d, Point2d>();

                var gScore = new Dictionary<Point2d, int>
                {
                    [from] = 0
                };

                var fScore = new Dictionary<Point2d, int>
                {
                    [from] = int.MaxValue
                };

                while (openSet.Count > 0)
                {
                    var (current, _, idx) = openSet.Select((x, idx) => (x, fScore.SafeGet(x, int.MaxValue), idx)).OrderBy(x => x.Item2).First();
                    if (current.Row == to.Row && current.Col == to.Col)
                    {
                        return ReconstructPath(cameFrom, current);
                    }
                    openSet.RemoveAt(idx);
                    foreach (var neighbor in NeighborsCells(grid, current))
                    {
                        var h = heuristics(grid, current, neighbor);
                        if (h == int.MaxValue)
                        {
                            continue;
                        }

                        var tentative_gScore = gScore[current] + h;

                        if (tentative_gScore < gScore.SafeGet(neighbor, int.MaxValue))
                        {
                            cameFrom.SafeSet(neighbor, current);
                            gScore.SafeSet(neighbor, tentative_gScore);
                            fScore.SafeSet(neighbor, tentative_gScore);
                            var isPresent = false;
                            foreach (var open in openSet)
                            {
                                if (open.Row == neighbor.Row && open.Col == neighbor.Col)
                                {
                                    isPresent = true;
                                    break;
                                }
                            }
                            if (!isPresent)
                            {
                                openSet.Add(neighbor);
                            }
                        }
                    }
                }

                return null;
            }

            private static IEnumerable<Point2d> NeighborsCells<T>(T[,] grid, Point2d c)
            {
                if (c.Row > 0)
                {
                    yield return new Point2d(c.Row - 1, c.Col);
                }

                if (c.Row + 1 < grid.GetLength(0))
                {
                    yield return new Point2d(c.Row + 1, c.Col);
                }

                if (c.Col > 0)
                {
                    yield return new Point2d(c.Row, c.Col - 1);
                }

                if (c.Col + 1 < grid.GetLength(1))
                {
                    yield return new Point2d(c.Row, c.Col + 1);
                }
            }

            private static List<Point2d> ReconstructPath(Dictionary<Point2d, Point2d> cameFrom, Point2d current)
            {
                var res = new List<Point2d>
                {
                    current
                };
                while (cameFrom.ContainsKey(current))
                {
                    res.Insert(0, cameFrom[current]);
                    current = cameFrom[current];
                }
                return res;
            }
        }
    }
}