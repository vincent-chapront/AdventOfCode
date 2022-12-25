using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode.Y2020
{
    internal class Day12 : GenericDay
    {
        public string Compute1(params string[] input)
        {
            int x;
            int y;

            static (int x, int y) Move(string[] input)
            {
                var x = 0;
                var y = 0;
                var direction = 0;
                foreach (var line in input)
                {
                    var action = line[0];
                    var value = int.Parse(line.Substring(1));
                    switch (action)
                    {
                        case 'N': y -= value; break;
                        case 'S': y += value; break;
                        case 'E': x += value; break;
                        case 'W': x -= value; break;
                        case 'L':
                            direction += value;
                            direction %= 360;
                            break;

                        case 'R':
                            direction += 360 - value;
                            direction %= 360;
                            break;

                        case 'F':
                            switch (direction)
                            {
                                case 0: x += value; break;
                                case 90: y -= value; break;
                                case 180: x -= value; break;
                                case 270: y += value; break;
                                default:
                                    break;
                            }
                            break;

                        default:
                            break;
                    }
                }

                return (x, y);
            }

            (x, y) = Move(new string[] { "N5", "S5" });
            Assert.AreEqual((x, y), (0, 0));

            (x, y) = Move(new string[] { "S5", "N5" });
            Assert.AreEqual((x, y), (0, 0));

            (x, y) = Move(new string[] { "E5", "W5" });
            Assert.AreEqual((x, y), (0, 0));

            (x, y) = Move(new string[] { "W5", "E5" });
            Assert.AreEqual((x, y), (0, 0));

            (x, y) = Move(new string[] { "E5", "R180", "F5" });
            Assert.AreEqual((x, y), (0, 0));

            (x, y) = Move(input);
            return (Math.Abs(x) + Math.Abs(y)).ToString();
        }

        public string Compute2(params string[] input)
        {
            int xShip = 0;
            int yShip = 0;
            int xWaypoint = 10;
            int yWaypoint = -1;

            foreach (var line in input)
            {
                var action = line[0];
                var value = int.Parse(line.Substring(1));
                var oldY = yWaypoint;
                var oldX = xWaypoint;
                switch (action)
                {
                    case 'N': yWaypoint -= value; break;
                    case 'S': yWaypoint += value; break;
                    case 'E': xWaypoint += value; break;
                    case 'W': xWaypoint -= value; break;
                    case 'L':
                        switch (value)
                        {
                            case 90:
                                xWaypoint = oldY;
                                yWaypoint = -oldX;
                                break;

                            case 180:
                                xWaypoint = -oldX;
                                yWaypoint = -oldY;
                                break;

                            case 270:
                                xWaypoint = -oldY;
                                yWaypoint = oldX;
                                break;

                            default:
                                break;
                        }
                        break;

                    case 'R':
                        switch (value)
                        {
                            case 90:
                                xWaypoint = -oldY;
                                yWaypoint = oldX;
                                break;

                            case 180:
                                xWaypoint = -oldX;
                                yWaypoint = -oldY;
                                break;

                            case 270:
                                xWaypoint = oldY;
                                yWaypoint = -oldX;
                                break;

                            default:
                                break;
                        }
                        break;

                    case 'F':
                        xShip += value * xWaypoint;
                        yShip += value * yWaypoint;
                        break;

                    default:
                        break;
                }
            }

            return (Math.Abs(xShip) + Math.Abs(yShip)).ToString();
        }
    }
}