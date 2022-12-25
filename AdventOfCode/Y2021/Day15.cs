using Microsoft.VisualStudio.TestTools.UnitTesting;
using static A_star.A_star;

namespace AdventOfCode.Y2021
{
    internal class Day15 : GenericDay
    {
        //https://rosettacode.org/wiki/A*_search_algorithm#C.23
        protected override object Part1()
        {
            Assert.AreEqual(40, Compute2(Resources.Year2021.Day15Test.ToLines(), 1));

            var res = Compute2(Resources.Year2021.Day15.ToLines(), 1);
            Assert.AreEqual(435, res);
            return res;
        }

        protected override object Part2()
        {
            //Assert.AreEqual(315, Compute2(Resources.Year2021.Day15Test.ToLines(),5));
            //return 0;
            //throw new NotImplementedException();
            var res = Compute2(Resources.Year2021.Day15.ToLines(), 5);
            Assert.AreEqual(2842, res);

            return res;
        }

        private static long Compute(Cell[,] cells, bool debugMode)
        {
            Astar astar = new Astar(new Coordinates(0, 0), new Coordinates(cells.GetLength(0) - 1, cells.GetLength(0) - 1), cells, debugMode);

            var result = 0;
            foreach (var coordinate in astar.path)
            {
                if (coordinate.Value.col != 0 || coordinate.Value.row != 0)
                    result += astar.cells[coordinate.Value.row, coordinate.Value.col].cost;
            }
            return result;
        }

        private static long Compute2(string[] input, int scale, bool debugMode = false)
        {
            int sizeActual = input.Length;
            Cell[,] cellsActual = new Cell[sizeActual * scale, sizeActual * scale];
            // Initialization of the cells values
            for (int i = 0; i < sizeActual; i++)
            {
                for (int j = 0; j < sizeActual; j++)
                {
                    for (int ki = 0; ki < scale; ki++)
                    {
                        for (int kj = 0; kj < scale; kj++)
                        {
                            var newCost = int.Parse(input[i][j].ToString()) + ki + kj;
                            cellsActual[i + sizeActual * ki, j + sizeActual * kj] = new Cell(new Coordinates(i + sizeActual * ki, j + sizeActual * kj))
                            {
                                parent = new Coordinates(),

                                cost = (newCost - 1) % 9 + 1
                            };
                        }
                    }
                }
            }

            var res = Compute(cellsActual, debugMode);
            return res;
        }
    }
}