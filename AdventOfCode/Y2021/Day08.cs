using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode.Y2021
{
    internal class Day08 : GenericDay
    {
        private enum eSegment
        { a, b, c, d, e, f, g }

        public string Compute1(string[] input, string args)
        {
            var searchDigits = new int[] { 2, 4, 3, 7 };
            var res = input.Select(x => new Config(x)).Sum(x => x.Output.Count(y => searchDigits.Contains(y.Segments.Count)));
            return res.ToString();
        }

        public string Compute2(string[] input, string args)
        {
            var dico = new Dictionary<eSegment, eSegment>();
            var configs = input.Select(x => new Config(x)).ToList();
            var segments = new eSegment[] { eSegment.a, eSegment.b, eSegment.c, eSegment.d, eSegment.e, eSegment.f, eSegment.g };

            var c = segments.CombinaisonDistinct(7).Select(x => x.ToList()).ToList();

            bool CanBe1(List<eSegment> l, Config config)
            {
                var digit = config.Patterns.First(x => x.Segments.Count == 2);
                return digit.Segments.Contains(l[2]) && digit.Segments.Contains(l[5]);
            }

            bool CanBe2(List<eSegment> l, Config config)
            {
                var digits = config.Patterns.Where(x => x.Segments.Count == 5);
                return digits.Any(x => x.Segments.Contains(l[0]) && x.Segments.Contains(l[2]) && x.Segments.Contains(l[3]) && x.Segments.Contains(l[4]) && x.Segments.Contains(l[6]));
            }

            bool CanBe3(List<eSegment> l, Config config)
            {
                var digits = config.Patterns.Where(x => x.Segments.Count == 5);
                return digits.Any(x => x.Segments.Contains(l[0]) && x.Segments.Contains(l[2]) && x.Segments.Contains(l[3]) && x.Segments.Contains(l[5]) && x.Segments.Contains(l[6]));
            }

            bool CanBe4(List<eSegment> l, Config config)
            {
                var digit = config.Patterns.First(x => x.Segments.Count == 4);
                return digit.Segments.Contains(l[1]) && digit.Segments.Contains(l[3]) && digit.Segments.Contains(l[2]) && digit.Segments.Contains(l[5]);
            }

            bool CanBe5(List<eSegment> l, Config config)
            {
                var digits = config.Patterns.Where(x => x.Segments.Count == 5);
                return digits.Any(x => x.Segments.Contains(l[0]) && x.Segments.Contains(l[1]) && x.Segments.Contains(l[3]) && x.Segments.Contains(l[5]) && x.Segments.Contains(l[6]));
            }

            bool CanBe6(List<eSegment> l, Config config)
            {
                var digits = config.Patterns.Where(x => x.Segments.Count == 6);
                return digits.Any(x => x.Segments.Contains(l[0]) && x.Segments.Contains(l[1]) && x.Segments.Contains(l[3]) && x.Segments.Contains(l[4]) && x.Segments.Contains(l[5]) && x.Segments.Contains(l[6]));
            }

            bool CanBe7(List<eSegment> l, Config config)
            {
                var digit = config.Patterns.First(x => x.Segments.Count == 3);
                return digit.Segments.Contains(l[0]) && digit.Segments.Contains(l[2]) && digit.Segments.Contains(l[5]);
            }

            bool CanBe9(List<eSegment> l, Config config)
            {
                var digits = config.Patterns.Where(x => x.Segments.Count == 6);
                return digits.Any(x => x.Segments.Contains(l[0]) && x.Segments.Contains(l[1]) && x.Segments.Contains(l[2]) && x.Segments.Contains(l[3]) && x.Segments.Contains(l[5]) && x.Segments.Contains(l[6]));
            }

            bool CanBe0(List<eSegment> l, Config config)
            {
                var digits = config.Patterns.Where(x => x.Segments.Count == 6);
                return digits.Any(x => x.Segments.Contains(l[0]) && x.Segments.Contains(l[1]) && x.Segments.Contains(l[2]) && x.Segments.Contains(l[4]) && x.Segments.Contains(l[5]) && x.Segments.Contains(l[6]));
            }

            int convert(List<eSegment> decryptor, List<eSegment> input)
            {
                bool AreEquals(int idx)
                {
                    return input.Contains(decryptor[idx]);
                }
                if (input.Count == 2) return 1;
                else if (input.Count == 4) return 4;
                else if (input.Count == 3) return 7;
                else if (input.Count == 7) return 8;
                else if (input.Count == 5)
                {
                    if (AreEquals(0) && AreEquals(2) && AreEquals(3) && AreEquals(4) && AreEquals(6)) return 2;
                    else if (AreEquals(0) && AreEquals(2) && AreEquals(3) && AreEquals(5) && AreEquals(6)) return 3;
                    else if (AreEquals(0) && AreEquals(1) && AreEquals(3) && AreEquals(5) && AreEquals(6)) return 5;
                }
                else if (input.Count == 6)
                {
                    if (AreEquals(0) && AreEquals(1) && AreEquals(3) && AreEquals(4) && AreEquals(5) && AreEquals(6)) return 6;
                    else if (AreEquals(0) && AreEquals(1) && AreEquals(2) && AreEquals(3) && AreEquals(5) && AreEquals(6)) return 9;
                    else if (AreEquals(0) && AreEquals(1) && AreEquals(2) && AreEquals(4) && AreEquals(5) && AreEquals(6)) return 0;
                }
                return -1;
            }
            var res = 0d;

            foreach (var config in configs)
            {
                var d1 =
                    c
                    .Where(x => CanBe1(x, config))
                    .Where(x => CanBe2(x, config))
                    .Where(x => CanBe3(x, config))
                    .Where(x => CanBe4(x, config))
                    .Where(x => CanBe5(x, config))
                    .Where(x => CanBe6(x, config))
                    .Where(x => CanBe7(x, config))
                    .Where(x => CanBe9(x, config))
                    .Where(x => CanBe0(x, config))
                    .ToList();
                if (d1.Count != 1)
                {
                    throw new Exception();
                }

                var digits = config.Output.Select(x => convert(d1[0], x.Segments)).Select((x, idx) => x * Math.Pow(10, config.Output.Count - idx - 1)).Sum();
                res += digits;
                ;
            }
            return ((long)res).ToString();
        }

        private class Config
        {
            public Config(string input)
            {
                var a = input.Split(" | ");
                Patterns = a[0].Split(" ").Select(x => new Digit(x)).ToList();
                Output = a[1].Split(" ").Select(x => new Digit(x)).ToList();
            }

            public List<Digit> Output { get; }
            public List<Digit> Patterns { get; }
        }

        private class Digit
        {
            public Digit(string segments)
            {
                Segments = segments.Select(x =>
                {
                    return x switch
                    {
                        'a' => eSegment.a,
                        'b' => eSegment.b,
                        'c' => eSegment.c,
                        'd' => eSegment.d,
                        'e' => eSegment.e,
                        'f' => eSegment.f,
                        'g' => eSegment.g,
                        _ => throw new Exception()
                    };
                }).ToList();
            }

            public List<eSegment> Segments { get; }
        }
    }
}