using System;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode.Y2016
{
    internal class Day08 : GenericDay
    {
        protected override object Part1()
        {
            Assert.AreEqual(6, Compute1(3, 7, Resources.Year2016.Day08Test.ToLines()));
            var res = Compute1(6,50, Resources.Year2016.Day08.ToLines());
            Assert.AreEqual(123, res);
            return res;
        }

        private static int Compute1(int nRow, int nCol, string[] input)
        {
            var grid = new bool[nRow, nCol];
            //Display(grid);

            foreach (var line in input)
            {
                //Console.WriteLine(line);
                grid = Next1(grid, line);
                //Display(grid);
            }

            var nbOn = 0;
            for (int row = 0; row < nRow; row++)
            {
                for (int col = 0; col < nCol; col++)
                {
                    nbOn += grid[row, col] ? 1 : 0;
                }
            }

            return nbOn;
        }

        protected override object Part2()
        {
            Display(Compute2(6, 50, Resources.Year2016.Day08.ToLines()));
            return "AFBUPZBJPS";
        }

        private static bool[,] Next1(bool[,] grid, string instruction)
        {

            int nRow = grid.GetLength(0);
            int nCol = grid.GetLength(1);
            var res = new bool[nRow, nCol];
            for (int row = 0; row < nRow; row++)
            {
                for (int col = 0; col < nCol; col++)
                {
                    res[row,col]=grid[row, col];
                }
            }

            if (instruction.StartsWith("rect"))
            {
                var a=instruction.Split(' ')[1].Split('x').Select(x=>int.Parse(x)).ToArray();
                var width = a[0];
                var height = a[1];
                for (int row = 0; row < height; row++)
                {
                    for (int col = 0; col < width; col++)
                    {
                        res[row, col] = true;
                    }
                }
            }
            else if(instruction.StartsWith("rotate"))
            {
                var regexRotate = new Regex(@"rotate ([a-z]+) \w=(\d*) by (\d*)");
                var r = regexRotate.Match(instruction);
                var idx = int.Parse(r.Groups[2].Value);
                var steps= int.Parse(r.Groups[3].Value);
                if (r.Groups[1].Value=="row")
                {
                    for (int step = 0; step < steps; step++)
                    {
                        var prev = res[idx, 0];
                        res[idx, 0] = res[idx, nCol - 1];
                        for (int col = nCol - 1; col > 1; col--)
                        {
                            res[idx, col] = res[idx, col - 1];
                        }
                        res[idx, 1] = prev;
                    }
                }
                else
                {
                    for (int step = 0; step < steps; step++)
                    {
                        var prev = res[0, idx];
                        res[0, idx] = res[nRow - 1, idx];
                        for (int row = nRow - 1; row > 0; row--)
                        {
                            res[row, idx] = res[row - 1, idx];
                        }
                        res[1, idx] = prev;
                    }
                }
                ;
            }
            return res;
        }

        private static void Display(bool[,] grid)
        {
            Console.WriteLine();
            for (int row = 0; row < grid.GetLength(0); row++)
            {
                for (int col = 0; col < grid.GetLength(1); col++)
                {
                    Console.Write(grid[row, col] ? "#" : ".");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
        private static bool[,] Compute2(int nRow, int nCol, string[] input)
        {
            var grid = new bool[nRow, nCol];
            //Display(grid);

            foreach (var line in input)
            {
                //Console.WriteLine(line);
                grid = Next1(grid, line);
                //Display(grid);
            }
            return grid;
        }
    }
}