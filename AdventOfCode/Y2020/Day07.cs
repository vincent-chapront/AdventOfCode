using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode.Y2020
{
    internal class Day07 : GenericDay
    {
        private static List<Bag> BuildBags(string[] input)
        {
            var data =
                input
                .Select(x =>
                    x.Replace(" bags", "")
                    .Replace(" bag", "")
                    .Replace(".", "")
                    .Split(" contain ")
                )
                .Select(y =>
                {
                    var bag = new Bag(y[0]);

                    var inners =
                    y[1] == "no other"
                    ? new List<(int count, string innerColor)>()
                    : y[1]
                        .Split(", ")
                        .Select(z =>
                        {
                            var idx = z.IndexOf(" ");
                            int count = int.Parse(z.Substring(0, idx));
                            string innerColor = z.Substring(idx).Trim();

                            return (count, innerColor);
                        }
                        )
                        .ToList();
                    return (bag, inners);
                }
                )
                .ToList();

            foreach (var datum in data)
            {
                foreach (var v in datum.inners)
                {
                    var bag = data.FirstOrDefault(x => x.bag.Color == v.innerColor).bag;
                    datum.bag.InnerBags.Add((v.count, bag));
                }
            }

            return data.Select(x => x.bag).ToList();
        }

        public string Compute1(string[] input, string args)
        {
            var data = BuildBags(input);

            var res = 0;
            foreach (var bag in data)
            {
                if (bag.Color == "shiny gold")
                {
                    continue;
                }
                if (Contains(bag, "shiny gold"))
                {
                    res++;
                }
            }

            return res.ToString();
        }

        public string Compute2(string[] input, string args)
        {
            var data = BuildBags(input);

            var shinyGold = data.FirstOrDefault(x => x.Color == "shiny gold");

            var res = CountInner(shinyGold) - 1;

            return res.ToString();
        }

        private static bool Contains(Bag bag, string color)
        {
            if (bag.Color == color)
            {
                return true;
            }
            else
            {
                foreach (var inner in bag.InnerBags)
                {
                    if (Contains(inner.innerBag, color))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private static int CountInner(Bag bag)
        {
            int res = 1;
            foreach (var inner in bag.InnerBags)
            {
                res += inner.count * CountInner(inner.innerBag);
            }
            return res;
        }

        public class Bag
        {
            public Bag(string color)
            {
                Color = color;
            }

            public string Color { get; }
            public List<(int count, Bag innerBag)> InnerBags { get; } = new List<(int count, Bag innerBag)>();
        }
    }
}