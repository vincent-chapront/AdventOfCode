using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode.Y2020
{
    internal class Day11 : GenericDay
    {
        public string Compute1(string[] input, string args)
        {
            var grid = new char[input.Length + 2, input[0].Length + 2];

            for (int i = 1; i < grid.GetLength(0) - 1; i++)
            {
                for (int j = 1; j < grid.GetLength(1) - 1; j++)
                {
                    grid[i, j] = input[i - 1][j - 1];
                }
            }
            bool hasChanged = true;
            while (hasChanged)
            {
                (hasChanged, grid) = Update1(grid);
                //Display(grid);
                //Console.ReadKey(); ;
            }

            int result = 0;
            for (int i = 1; i < grid.GetLength(0) - 1; i++)
            {
                for (int j = 1; j < grid.GetLength(1) - 1; j++)
                {
                    if (grid[i, j] == '#')
                    {
                        result++;
                    }
                }
            }

            return result.ToString();
        }

        public string Compute2(string[] input, string args)
        {
            var grid = new char[input.Length + 2, input[0].Length + 2];

            for (int i = 1; i < grid.GetLength(0) - 1; i++)
            {
                for (int j = 1; j < grid.GetLength(1) - 1; j++)
                {
                    grid[i, j] = input[i - 1][j - 1];
                }
            }
            bool hasChanged = true;
            while (hasChanged)
            {
                (hasChanged, grid) = Update2(grid);
                //Display(grid);
                //Console.ReadKey();
            }

            int result = 0;
            for (int i = 1; i < grid.GetLength(0) - 1; i++)
            {
                for (int j = 1; j < grid.GetLength(1) - 1; j++)
                {
                    if (grid[i, j] == '#')
                    {
                        result++;
                    }
                }
            }

            return result.ToString();
        }

        private static void Display(char[,] input)
        {
            Console.WriteLine();
            for (int i = 1; i < input.GetLength(0) - 1; i++)
            {
                for (int j = 1; j < input.GetLength(1) - 1; j++)
                {
                    Console.Write(input[i, j]);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        private static char GetFirstSeatInDirection(char[,] input, int x, int y, int deltaI, int deltaJ)
        {
            int i = deltaI;
            int j = deltaJ;
            do
            {
                if (input[x + i, y + j] == '#' || input[x + i, y + j] == 'L')
                {
                    return (input[x + i, y + j]);
                }
                i += deltaI;
                j += deltaJ;
            } while (x + i >= 0 && y + j >= 0 && x + i < input.GetLength(0) && y + j < input.GetLength(1));
            return '.';
        }

        private static char[] GetSurrounding1(char[,] input, int x, int y)
        {
            return new char[]
            {
                input[x-1,y-1],
                input[x-1,y-0],
                input[x-1,y+1],
                input[x-0,y-1],
                input[x-0,y+1],
                input[x+1,y-1],
                input[x+1,y-0],
                input[x+1,y+1],
            };
        }

        private static List<char> GetSurrounding2(char[,] input, int x, int y)
        {
            List<char> result = new List<char>();

            result.Add(GetFirstSeatInDirection(input, x, y, -1, 0));
            result.Add(GetFirstSeatInDirection(input, x, y, 1, 0));
            result.Add(GetFirstSeatInDirection(input, x, y, 0, -1));
            result.Add(GetFirstSeatInDirection(input, x, y, 0, 1));
            result.Add(GetFirstSeatInDirection(input, x, y, 1, -1));
            result.Add(GetFirstSeatInDirection(input, x, y, 1, 1));
            result.Add(GetFirstSeatInDirection(input, x, y, -1, -1));
            result.Add(GetFirstSeatInDirection(input, x, y, -1, 1));

            return result;
        }

        private static (bool hasChanged, char[,] output) Update1(char[,] input)
        {
            char[,] output = new char[input.GetLength(0), input.GetLength(1)];
            bool hasChanged = false;

            for (int i = 1; i < input.GetLength(0) - 1; i++)
            {
                for (int j = 1; j < input.GetLength(1) - 1; j++)
                {
                    var surrounding = GetSurrounding1(input, i, j);
                    if (input[i, j] == 'L' && !surrounding.Any(x => x == '#'))
                    {
                        hasChanged = true;
                        output[i, j] = '#';
                    }
                    else if (input[i, j] == '#' && surrounding.Count(x => x == '#') >= 4)
                    {
                        hasChanged = true;
                        output[i, j] = 'L';
                    }
                    else
                    {
                        output[i, j] = input[i, j];
                    }
                }
            }

            return (hasChanged, output);
        }

        private static (bool hasChanged, char[,] output) Update2(char[,] input)
        {
            char[,] output = new char[input.GetLength(0), input.GetLength(1)];
            bool hasChanged = false;

            for (int i = 1; i < input.GetLength(0) - 1; i++)
            {
                for (int j = 1; j < input.GetLength(1) - 1; j++)
                {
                    var surrounding = GetSurrounding2(input, i, j);
                    if (input[i, j] == 'L' && !surrounding.Any(x => x == '#'))
                    {
                        hasChanged = true;
                        output[i, j] = '#';
                    }
                    else if (input[i, j] == '#' && surrounding.Count(x => x == '#') >= 5)
                    {
                        hasChanged = true;
                        output[i, j] = 'L';
                    }
                    else
                    {
                        output[i, j] = input[i, j];
                    }
                }
            }

            return (hasChanged, output);
        }
    }
}