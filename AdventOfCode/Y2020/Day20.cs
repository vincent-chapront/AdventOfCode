using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode.Y2020
{
    internal class Day20 : GenericDay
    {
        private static int count = 0;

        protected override object Part1()
        {
            throw new NotImplementedException();
            Console.Clear();
            Assert.AreEqual(20899048083289, Compute1(Resources.Year2020.Day20Test.ToLines()));
            //Assert.AreEqual(20899048083289, Compute1(Resources.Year2020.Day20.ToLines()));

            return 0L;
        }

        protected override object Part2()
        {
            throw new NotImplementedException();
        }

        private static bool Compute(List<Tile> tiles, Tile[,] image, Queue<Point2d> emptySpots, int sizeTile)
        {
            if (emptySpots.Count == 0)
            {
                return true;
            }
            var emptySpot = emptySpots.Dequeue();
            count++;
            if (count % 10000 == 00)
            {
                //Console.SetCursorPosition(0, 0);
                Console.WriteLine(count);
            }
            //Console.WriteLine(emptySpot.Row + " , " + emptySpot.Col);
            var validTiles = new List<Tile>(); // get validTiles

            foreach (var tile in tiles)
            {
                foreach (var orientation in tile.GetAllOrientation())
                {
                    if (!IsValid(image, sizeTile, emptySpot, tile))
                    {
                        continue;
                    }

                    image[emptySpot.Row, emptySpot.Col] = tile;
                    if (Compute(tiles.Where(x => x != tile).ToList(), image, emptySpots, sizeTile))
                    {
                        return true;
                    }
                }
            }
            image[emptySpot.Row, emptySpot.Col] = null;
            emptySpots.Enqueue(emptySpot);
            return false;
        }

        public string Compute1(params string[] input)
        {
            var tiles = ParseInput(input);
            var size = (int)Math.Sqrt(tiles.Count);
            var image = new Tile[size, size];
            var emptySpots = new Queue<Point2d>(image.GetAllCoordinates());

            Compute(tiles, image, emptySpots, tiles[0].Pixels.GetLength(0));

            return (image[0, 0].Id * image[image.GetLength(0) - 1, 0].Id * image[0, image.GetLength(1) - 1].Id * image[image.GetLength(0) - 1, image.GetLength(1) - 1].Id).ToString();
        }

        private static void Display(bool[,] input)
        {
            Console.WriteLine();
            for (int i = 0; i < input.GetLength(0); i++)
            {
                for (int j = 0; j < input.GetLength(1); j++)
                {
                    Console.Write(input[i, j] ? "#" : ".");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        private static bool IsValid(Tile[,] image, int sizeTile, Point2d emptySpot, Tile tile)
        {
            var sizeImage = image.GetLength(0);
            IEnumerable<int> range = Enumerable.Range(0, sizeTile - 1);
            if (emptySpot.Col > 1)
            {
                var leftTile = image[emptySpot.Row, emptySpot.Col - 1];
                if (leftTile != null)
                {
                    if (range.Any(x => leftTile.Pixels[x, sizeTile - 1] != tile.Pixels[x, 0]))
                    {
                        return false;
                    }
                }
            }
            if (emptySpot.Row > 1)
            {
                var topTile = image[emptySpot.Row - 1, emptySpot.Col];
                if (topTile != null)
                {
                    if (range.Any(x => topTile.Pixels[sizeTile - 1, x] != tile.Pixels[0, x]))
                    {
                        return false;
                    }
                }
            }
            if (emptySpot.Col < sizeImage - 2)
            {
                var rightTile = image[emptySpot.Row, emptySpot.Col + 1];
                if (rightTile != null)
                {
                    if (range.Any(x => rightTile.Pixels[x, 0] != tile.Pixels[x, sizeTile - 1]))
                    {
                        return false;
                    }
                }
            }
            if (emptySpot.Row < sizeImage - 2)
            {
                var bottomTile = image[emptySpot.Row + 1, emptySpot.Col];
                if (bottomTile != null)
                {
                    if (range.Any(x => bottomTile.Pixels[0, x] != tile.Pixels[sizeTile - 1, x]))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private static List<Tile> ParseInput(string[] input)
        {
            var tiles = new List<Tile>();
            for (int i = 0; i < input.Length; i++)
            {
                tiles.Add(ReadTile(input, ref i));
            }
            return tiles;
        }

        private static Tile ReadTile(string[] input, ref int i)
        {
            var tileNumber = int.Parse(input[i].Replace("Tile ", "").Replace(":", ""));
            i++;
            var l = input.Skip(i).TakeWhile(x => !string.IsNullOrWhiteSpace(x)).ToArray();
            i += l.Length;
            return new Tile(tileNumber, l);
        }

        private class Tile
        {
            public Tile(int tileNumber, string[] l)
            {
                Id = tileNumber;
                Pixels = l.ParseToGrid(x => x == '#');
            }

            public int Id { get; set; }
            public bool[,] Pixels { get; set; }

            public IEnumerable<bool[,]> GetAllOrientation()
            {
                yield return Pixels;

                Pixels = Rotate(Pixels);
                yield return Pixels;

                Pixels = Rotate(Pixels);
                yield return Pixels;

                Pixels = Rotate(Pixels);
                yield return Pixels;

                Pixels = Rotate(Pixels);
                Pixels = Flip(Pixels);
                yield return Pixels;

                Pixels = Rotate(Pixels);
                yield return Pixels;

                Pixels = Rotate(Pixels);
                yield return Pixels;

                Pixels = Rotate(Pixels);
                yield return Pixels;
            }

            public override string ToString()
            {
                return Id.ToString();
            }

            private static bool[,] Flip(bool[,] input)
            {
                var result = new bool[input.GetLength(0), input.GetLength(1)];
                foreach (var coordinate in input.GetAllCoordinates())
                {
                    result[coordinate.Row, coordinate.Col] = input[coordinate.Row, input.GetLength(1) - 1 - coordinate.Col];
                }
                return result;
            }

            private static bool[,] Rotate(bool[,] input)
            {
                var result = new bool[input.GetLength(0), input.GetLength(1)];
                foreach (var coordinate in input.GetAllCoordinates())
                {
                    result[coordinate.Row, coordinate.Col] = input[coordinate.Col, input.GetLength(1) - 1 - coordinate.Row];
                }
                return result;
            }
        }
    }
}