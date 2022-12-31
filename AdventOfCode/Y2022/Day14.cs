using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace AdventOfCode.Y2022
{
    internal class Day14 : GenericDay
    {
        private enum DisplayModes
        {
            None,
            Console,
            Bmp
        }

        private enum Tile
        {
            Air,
            Sand,
            FallingSand,
            Rock
        }

        public string Compute1(string[] input, string args)
        {
            var grid2 = Parse(input);
            return DropSand(grid2, DisplayModes.None).ToString();
        }

        public string Compute2(string[] input, string args)
        {
            var grid2 = Parse(input);
            return DropSand2(grid2, DisplayModes.None).ToString();
        }

        private static void Display(Dictionary<(int x, int y), Tile> grid, int counter, DisplayModes display)
        {
            switch (display)
            {
                case DisplayModes.Console:
                    DisplayConsole(grid);
                    break;

                case DisplayModes.Bmp:
                    DisplayBmp(grid, counter);
                    break;
            }
        }

#pragma warning disable CA1416 // Validate platform compatibility
        private static void DisplayBmp(Dictionary<(int x, int y), Tile> grid, int counter)
        {
            (int minX, int maxX, int minY, int maxY) = GetMinMaxRock(grid);
            const int scaleX = 4;
            const int scaleY = 4;
            var bmp = new Bitmap((maxX - minX + 1) * scaleX, (maxY - minY + 1) * scaleY);
            Graphics g = Graphics.FromImage(bmp);
            g.Clear(Color.Black);
            foreach (var kvp in grid)
            {
                var i = (kvp.Key.x - minX);
                var j = (kvp.Key.y - minY);
                switch (kvp.Value)
                {
                    case Tile.Sand:
                        g.FillRectangle(Brushes.DarkOrange, i * scaleX, j * scaleY, scaleX, scaleY);
                        break;

                    case Tile.FallingSand:
                        g.FillRectangle(Brushes.Yellow, i * scaleX, j * scaleY, scaleX, scaleY);
                        break;

                    case Tile.Rock:
                        g.FillRectangle(Brushes.LightGray, i * scaleX, j * scaleY, scaleX, scaleY);
                        break;
                }
            }
            bmp.Save("img/Grid" + counter + ".png");
        }
#pragma warning restore CA1416 // Validate platform compatibility

        private static void DisplayConsole(Dictionary<(int x, int y), Tile> grid)
        {
            Console.WriteLine();
            (int minX, int maxX, _, int maxY) = GetMinMaxRock(grid);

            (var left, var top) = Console.GetCursorPosition();
            foreach (var kvp in grid)
            {
                var c = "";
                switch (kvp.Value)
                {
                    case Tile.Air:
                        c = ".";
                        break;

                    case Tile.Sand:
                        c = "+";
                        break;

                    case Tile.FallingSand:
                        c = "~";
                        break;

                    case Tile.Rock:
                        c = "#";
                        break;
                }

                Console.SetCursorPosition(left + kvp.Key.x - minX, top + kvp.Key.y);

                Console.Write(c);
            }
            Console.SetCursorPosition(0, top + maxY + 2);
            Console.WriteLine(new String('*', maxX - minX + 1));
            Console.WriteLine();
        }

        private static int DropSand(Dictionary<(int x, int y), Tile> grid, DisplayModes display)
        {
            int maxX = grid.Keys.Max(x => x.x);
            int minX = grid.Keys.Min(x => x.x);
            int maxY = grid.Keys.Max(x => x.y);

            var sandX = 500;
            var sandY = 0;
            grid.SafeSet((sandX, sandY), Tile.FallingSand);
            int sandCount = 0;
            int counter = 1;
            Display(grid, counter++, display);
            var stack = new Stack<(int x, int y)>();
            stack.Push((sandX, sandY));
            while (true)
            {
                var newSandX = sandX;
                var newSandY = sandY;

                if (sandX <= minX || sandX >= maxX || sandY >= maxY)
                {
                    return sandCount;
                }
                if (grid.SafeGet((sandX, sandY + 1), Tile.Air) == Tile.Air)
                {
                    stack.Push((sandX, sandY));
                    newSandY++;
                }
                else if (grid.SafeGet((sandX - 1, sandY + 1), Tile.Air) == Tile.Air)
                {
                    stack.Push((sandX, sandY));
                    newSandX--;
                    newSandY++;
                }
                else if (grid.SafeGet((sandX + 1, sandY + 1), Tile.Air) == Tile.Air)
                {
                    stack.Push((sandX, sandY));
                    newSandX++;
                    newSandY++;
                }
                else
                {
                    (var x, var y) = stack.Pop();
                    grid.SafeSet((sandX, sandY), Tile.Sand);
                    sandX = x;
                    sandY = y;
                    grid.SafeSet((sandX, sandY), Tile.FallingSand);
                    sandCount++;
                    Display(grid, counter++, display);
                    continue;
                }

                grid.SafeRemove((sandX, sandY));
                grid.SafeSet((newSandX, newSandY), Tile.FallingSand);
                Display(grid, counter++, display);
                sandX = newSandX;
                sandY = newSandY;
            }
        }

        private static int DropSand2(Dictionary<(int x, int y), Tile> grid, DisplayModes display)
        {
            (_, _, _, int maxY) = GetMinMaxRock(grid);

            var sandX = 500;
            var sandY = 0;
            grid.SafeSet((sandX, sandY), Tile.FallingSand);
            int sandCount = 0;
            int counter = 1;
            Display(grid, counter++, display);
            var stack = new Stack<(int x, int y)>();
            stack.Push((sandX, sandY));
            while (true)
            {
                var newSandX = sandX;
                var newSandY = sandY;

                if (sandY > maxY)
                {
                    (var x, var y) = stack.Pop();
                    grid.SafeSet((sandX, sandY), Tile.Sand);
                    sandX = x;
                    sandY = y;
                    grid.SafeSet((sandX, sandY), Tile.FallingSand);
                    sandCount++;
                    Display(grid, counter++, display);
                    continue;
                }
                if (grid.SafeGet((sandX, sandY + 1), Tile.Air) == Tile.Air)
                {
                    stack.Push((sandX, sandY));
                    newSandY++;
                }
                else if (grid.SafeGet((sandX - 1, sandY + 1), Tile.Air) == Tile.Air)
                {
                    stack.Push((sandX, sandY));
                    newSandX--;
                    newSandY++;
                }
                else if (grid.SafeGet((sandX + 1, sandY + 1), Tile.Air) == Tile.Air)
                {
                    stack.Push((sandX, sandY));
                    newSandX++;
                    newSandY++;
                }
                else if (stack.Count == 0)
                {
                    return sandCount;
                }
                else
                {
                    (var x, var y) = stack.Pop();
                    grid.SafeSet((sandX, sandY), Tile.Sand);
                    sandX = x;
                    sandY = y;
                    grid.SafeSet((sandX, sandY), Tile.FallingSand);
                    sandCount++;
                    Display(grid, counter++, display);
                    continue;
                }

                grid.SafeRemove((sandX, sandY));
                grid.SafeSet((newSandX, newSandY), Tile.FallingSand);
                Display(grid, counter++, display);
                sandX = newSandX;
                sandY = newSandY;
            }
        }

        private static (int minX, int maxX, int minY, int maxY) GetMinMaxRock(Dictionary<(int x, int y), Tile> grid)
        {
            return
                (
                    grid.Select(x => x.Key).Min(x => x.x),
                    grid.Select(x => x.Key).Max(x => x.x),
                    grid.Where(x => x.Value == Tile.Rock).Select(x => x.Key).Min(x => x.y),
                    grid.Where(x => x.Value == Tile.Rock).Select(x => x.Key).Max(x => x.y)
                );
        }

        private static Dictionary<(int x, int y), Tile> Parse(string[] input)
        {
            var walls = new List<List<(int x, int y)>>();
            foreach (var line in input)
            {
                var v =
                    line
                    .Split(" -> ")
                    .Select(x => x.Split(","))
                    .Select(a => (x: Convert.ToInt32(a[0]), y: Convert.ToInt32(a[1])))
                    .ToList();
                walls.Add(v);
            }

            var grid = new Dictionary<(int x, int y), Tile>();

            foreach (var wall in walls)
            {
                for (int wallCornerIndex = 0; wallCornerIndex < wall.Count - 1; wallCornerIndex++)
                {
                    var (aX, aY) = wall[wallCornerIndex];
                    var (bX, bY) = wall[wallCornerIndex + 1];
                    if (aX == bX)
                    {
                        if (aY < bY)
                        {
                            for (int j = aY; j <= bY; j++)
                            {
                                grid.SafeSet((aX, j), Tile.Rock);
                            }
                        }
                        else
                        {
                            for (int j = bY; j <= aY; j++)
                            {
                                grid.SafeSet((aX, j), Tile.Rock);
                            }
                        }
                    }
                    if (aY == bY)
                    {
                        if (aX < bX)
                        {
                            for (int j = aX; j <= bX; j++)
                            {
                                grid.SafeSet((j, aY), Tile.Rock);
                            }
                        }
                        else
                        {
                            for (int j = bX; j <= aX; j++)
                            {
                                grid.SafeSet((j, aY), Tile.Rock);
                            }
                        }
                    }
                }
            }

            return grid;
        }
    }
}