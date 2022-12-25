using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public static class EnumerableHelpers
    {
        public static IEnumerable<IEnumerable<T>> CombinaisonDistinct<T>(this T[] input, int size)
        {
            foreach (var combinaison in CombinaisonDistinctIndex(input.Length, size))
            {
                yield return combinaison.Select(x => input[x]);
            }
        }

        /* public static IEnumerable<(int row, int column)> GetAllCoordinates<T>(this T[,] grid)
         {
             for (int row = 0; row < grid.GetLength(0); row++)
             {
                 for (int column = 0; column < grid.GetLength(1); column++)
                 {
                     yield return (row, column);
                 }
             }
         }*/

        public static IEnumerable<Point2d> GetAllCoordinates<T>(this T[,] grid)
        {
            for (int row = 0; row < grid.GetLength(0); row++)
            {
                for (int column = 0; column < grid.GetLength(1); column++)
                {
                    yield return new Point2d(row, column);
                }
            }
        }

        public static IEnumerable<(int, int)> GetAllPairs(this IEnumerable<int> input)
        {
            foreach (var item1 in input.Select((elm, idx) => (elm, idx)))
            {
                foreach (var item2 in input.Skip(item1.idx + 1))
                {
                    yield return (item1.elm, item2);
                }
            }
        }

        public static List<List<T>> GetCombinations<T>(this IEnumerable<T> elements, List<T> current = null)
        {
            if (current == null)
            {
                current = new List<T>();
            }
            if (!elements.Any())
            {
                return new List<List<T>> { new List<T>(current) };
            }

            var result = new List<List<T>>();
            int i = 0;
            foreach (var element in elements)
            {
                current.Add(element);
                var subCombination = GetCombinations(elements.Where((x, idx) => idx != i).ToList(), current);
                result.AddRange(subCombination);
                current.RemoveAt(current.Count - 1);
                i++;
            }

            return result;
        }

        public static string Join<T>(this IEnumerable<T> input, char separator)
        {
            return string.Join(separator, input);
        }

        public static string Join<T>(this IEnumerable<T> input, string separator)
        {
            return string.Join(separator, input);
        }

        public static T[,] MyClone<T>(this T[,] input)
        {
            var grid = new T[input.GetLength(0), input.GetLength(1)];

            foreach (var point in grid.GetAllCoordinates())
            {
                grid[point.Row, point.Col] = input[point.Row, point.Col];
            }

            return grid;
        }

        public static T[,] ParseToGrid<T>(this string[] input, Func<char, T> convertor)
        {
            var grid = new T[input.Length, input[0].Length];

            foreach (var point in grid.GetAllCoordinates())
            {
                grid[point.Row, point.Col] = convertor(input[point.Row][point.Col]);
            }

            return grid;
        }

        public static long Product<T>(this IEnumerable<T> input, Func<T,int> selector)
        {
            long result = 1;
            foreach (var value in input)
            {
                result *= selector(value);
            }
            return result;
        }

        public static long Product<T>(this IEnumerable<T> input, Func<T, long> selector)
        {
            long result = 1;
            foreach (var value in input)
            {
                result *= selector(value);
            }
            return result;
        }

        public static long Product(this IEnumerable<int> input)
        {
            long result = 1;
            foreach (var value in input)
            {
                result *= value;
            }
            return result;
        }

        public static long Product(this IEnumerable<long> input)
        {
            long result = 1;
            foreach (var value in input)
            {
                result *= value;
            }
            return result;
        }

        private static IEnumerable<List<int>> CombinaisonDistinctIndex(int length, int size)
        {
            return CombinaisonIndex(length, size).Where(x => x.Distinct().Count() == x.Count);
        }

        private static IEnumerable<List<int>> CombinaisonIndex(int length, int size)
        {
            for (int i = 0; i < length; i++)
            {
                if (size == 1)
                {
                    yield return new List<int> { i };
                }
                else
                {
                    foreach (var v in CombinaisonIndex(length, size - 1))
                    {
                        v.Insert(0, i);
                        yield return v;
                    }
                }
            }
        }
    }
}