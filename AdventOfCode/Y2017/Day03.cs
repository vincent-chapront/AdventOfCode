using System;

namespace AdventOfCode.Y2017
{
    internal class Day03 : GenericDay
    {
        public string Compute1(params string[] input)
        {
            var searched = int.Parse(input[0]);

            (int x, int y) = GetCoordinates(searched);
            return (Math.Abs(x) + Math.Abs(y)).ToString();
        }

        private static (int x, int y) GetCoordinates(int searched)
        {
            var x = 0;
            var y = 0;
            bool found = false;
            var idx = 0;
            while (!found)
            {
                int cornerValue = (int)Math.Pow(2 * idx + 1, 2);
                if (searched == cornerValue)
                {
                    x = idx;
                    y = idx;
                    found = true;
                    break;
                }
                else if (searched < Math.Pow(2 * (idx + 1) + 1, 2))
                {
                    var inc = (idx + 1) * 2;
                    if (searched < cornerValue + inc)
                    {
                        var diff = (cornerValue + 1 * inc) - searched;

                        y = idx + 1 - diff;
                        x = idx + 1;
                    }
                    else if (searched < cornerValue + 2 * inc)
                    {
                        var diff = (cornerValue + 2 * inc) - searched;

                        y = idx + 1;
                        x = -idx - 1 + diff;
                    }
                    else if (searched < cornerValue + 3 * inc)
                    {
                        var diff = (cornerValue + 3 * inc) - searched;

                        y = -idx - 1 + diff;
                        x = idx + 1;
                    }
                    else if (searched < cornerValue + 4 * inc)
                    {
                        var diff = (cornerValue + 4 * inc) - searched;

                        y = idx + 1;
                        x = idx + 1 - diff;
                        ;
                    }
                    found = true;
                    break;
                }
                idx++;
            }
            return (x, y);
        }

        public string Compute2(params string[] input)
        {
            throw new NotImplementedException();
        }
    }
}