using System;
using System.Linq;

namespace AdventOfCode.Y2021
{
    internal class Day02 : GenericDay
    {
        public string Compute1(string[] input, string args)
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

        public string Compute2(string[] input, string args)
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