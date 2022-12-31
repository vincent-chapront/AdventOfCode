using System;
using System.Collections.Generic;

namespace AdventOfCode.Y2016
{
    internal class Day11 : GenericDay
    {
        public List<(List<string>[], int)> GetPossibleNext(List<string>[] grid, int elevator)
        {
            List<(List<string>[], int)> res = new();
            if (grid[elevator].Count > 0)
            {
                if (elevator > 0)
                {
                    var newGrid = InitializeNewGrid(grid, elevator);
                    newGrid[elevator - 1].Add(grid[elevator][0]);
                    newGrid[elevator].RemoveAt(0);
                    res.Add((newGrid, elevator - 1));
                }
                if (elevator < grid.Length - 1)
                {
                    var newGrid = InitializeNewGrid(grid, elevator);
                    newGrid[elevator + 1].Add(grid[elevator][0]);
                    newGrid[elevator].RemoveAt(0);
                    res.Add((newGrid, elevator + 1));
                }
            }
            if (grid[elevator].Count > 1)
            {
                if (elevator > 0)
                {
                    var newGrid1 = InitializeNewGrid(grid, elevator);
                    newGrid1[elevator - 1].Add(grid[elevator][0]);
                    newGrid1[elevator - 1].Add(grid[elevator][1]);
                    newGrid1[elevator].RemoveAt(0);
                    newGrid1[elevator].RemoveAt(0);
                    res.Add((newGrid1, elevator - 1));

                    var newGrid2 = InitializeNewGrid(grid, elevator);
                    newGrid2[elevator - 1].Add(grid[elevator][1]);
                    newGrid2[elevator].RemoveAt(1);
                    res.Add((newGrid2, elevator - 1));
                }
                if (elevator < grid.Length - 1)
                {
                    var newGrid1 = InitializeNewGrid(grid, elevator);
                    newGrid1[elevator + 1].Add(grid[elevator][0]);
                    newGrid1[elevator + 1].Add(grid[elevator][1]);
                    newGrid1[elevator].RemoveAt(0);
                    newGrid1[elevator].RemoveAt(0);
                    res.Add((newGrid1, elevator + 1));

                    var newGrid2 = InitializeNewGrid(grid, elevator);
                    newGrid2[elevator + 1].Add(grid[elevator][1]);
                    newGrid2[elevator].RemoveAt(1);
                    res.Add((newGrid2, elevator + 1));
                }
            }

            return res;
            //var canBeMoved=
        }

        private static List<string>[] InitializeNewGrid(List<string>[] grid, int elevator)
        {
            var newGrid = new List<string>[grid.Length];

            /*for (int i = 0; i < elevator; i++)
            {
                newGrid[i] = new List<string>(grid[i]);
            }

            for (int i = elevator + 1; i < grid.Length; i++)
            {
                newGrid[i] = new List<string>(grid[i]);
            }*/

            for (int i = 0; i < grid.Length; i++)
            {
                newGrid[i] = new List<string>(grid[i]);
            }
            return newGrid;
        }

        public string Compute1(string[] input, string args)
        {
            var grid = new List<string>[]
            {
                new List<string>{ },
                new List<string>{"LG"},
                new List<string>{"HG"},
                new List<string>{"HM","LM" },
                new List<string>{ }
            };

            int elevator = 3;
            GetPossibleNext(grid, elevator);

            throw new NotImplementedException();
        }

        public string Compute2(string[] input, string args)
        {
            throw new NotImplementedException();
        }
    }
}