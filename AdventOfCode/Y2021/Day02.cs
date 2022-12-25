using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode.Y2021
{
    internal class Day02 : GenericDay
    {
        //protected override object Part1()
        //{
        //    Assert.AreEqual(150, Compute1(Resources.Year2021.Day02Test.ToLines()));
        //    var res = this.Compute1(Resources.Year2021.Day02.ToLines());
        //    Assert.AreEqual(1648020, res);
        //    return res;
        //}

        //protected override object Part2()
        //{
        //    Assert.AreEqual(900, Compute2(Resources.Year2021.Day02Test.ToLines()));
        //    var res = this.Compute2(Resources.Year2021.Day02.ToLines());
        //    Assert.AreEqual(1759818555, res);
        //    return res;
        //}

        public string Compute1(params string[] input)
        {
            (int finalX, int finalDepth) =
            input.Select(x => new Step(x)).Aggregate((x: 0, depth: 0), (acc, step) =>
            {
                switch (step.Direction)
                {
                    case Step.EDirection.Down:
                        acc.x += step.Distance;
                        break;

                    case Step.EDirection.Up:
                        acc.x -= step.Distance;
                        break;

                    case Step.EDirection.Forward:
                        acc.depth += step.Distance;
                        break;

                    default:
                        break;
                }
                return acc;
            });

            return (finalX * finalDepth).ToString();
        }

        public string Compute2(params string[] input)
        {
            (int finalX, int finalDepth, int finalAim) =
            input.Select(x => new Step(x)).Aggregate((x: 0, depth: 0, aim: 0), (acc, step) =>
            {
                switch (step.Direction)
                {
                    case Step.EDirection.Down:
                        acc.aim += step.Distance;
                        break;

                    case Step.EDirection.Up:
                        acc.aim -= step.Distance;
                        break;

                    case Step.EDirection.Forward:
                        acc.x += step.Distance;
                        acc.depth += acc.aim * step.Distance;
                        break;

                    default:
                        break;
                }
                return acc;
            });
            return (finalX * finalDepth).ToString();
        }

        internal class Step
        {
            public Step(string step)
            {
                var s = step.Split(" ");
                Direction =
                    s[0] switch
                    {
                        "down" => EDirection.Down,
                        "forward" => EDirection.Forward,
                        "up" => EDirection.Up,
                        _ => throw new ArgumentException("direction")
                    };
                Distance = int.Parse(s[1]);
            }

            public enum EDirection
            {
                Down,
                Up,
                Forward,
            }

            public EDirection Direction { get; }
            public int Distance { get; }
        }
    }
}