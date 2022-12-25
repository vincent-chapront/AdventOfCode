using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Y2015
{
    internal class Day16 : GenericDay
    {
        public string Compute1(params string[] input)
        {
            var aunts = input.Select(x => new Description(x)).ToList();

            var aunt =
                aunts
                .Where(x => !x.children.HasValue || x.children.Value == 3)
                .Where(x => !x.cats.HasValue || x.cats.Value == 7)
                .Where(x => !x.samoyeds.HasValue || x.samoyeds.Value == 2)
                .Where(x => !x.pomeranians.HasValue || x.pomeranians.Value == 3)
                .Where(x => !x.akitas.HasValue || x.akitas.Value == 0)
                .Where(x => !x.vizslas.HasValue || x.vizslas.Value == 0)
                .Where(x => !x.goldfish.HasValue || x.goldfish.Value == 5)
                .Where(x => !x.trees.HasValue || x.trees.Value == 3)
                .Where(x => !x.cars.HasValue || x.cars.Value == 2)
                .Where(x => !x.perfumes.HasValue || x.perfumes.Value == 1)
                .ToList();
            return aunt.Count == 1 ? aunt.First().Number.ToString() : "ERROR";
        }

        public string Compute2(params string[] input)
        {
            var aunts = input.Select(x => new Description(x)).ToList();

            var aunt =
                aunts
                .Where(x => !x.children.HasValue || x.children.Value == 3)
                .Where(x => !x.cats.HasValue || x.cats.Value > 7)
                .Where(x => !x.samoyeds.HasValue || x.samoyeds.Value == 2)
                .Where(x => !x.pomeranians.HasValue || x.pomeranians.Value < 3)
                .Where(x => !x.akitas.HasValue || x.akitas.Value == 0)
                .Where(x => !x.vizslas.HasValue || x.vizslas.Value == 0)
                .Where(x => !x.goldfish.HasValue || x.goldfish.Value < 5)
                .Where(x => !x.trees.HasValue || x.trees.Value > 3)
                .Where(x => !x.cars.HasValue || x.cars.Value == 2)
                .Where(x => !x.perfumes.HasValue || x.perfumes.Value == 1)
                .ToList();
            return aunt.Count == 1 ? aunt.First().Number.ToString() : "ERROR";
        }

        public class Description
        {
            public int? akitas;
            public int? cars;
            public int? cats;
            public int? children;
            public int? goldfish;
            public int Number;
            public int? perfumes;
            public int? pomeranians;
            public int? samoyeds;
            public int? trees;
            public int? vizslas;

            public Description(string line)
            {
                var match = Regex.Match(line, @"Sue (\d+): (.*)");
                Number = int.Parse(match.Groups[1].ToString());
                var categories = match.Groups[2].ToString().Split(", ");
                foreach (var category in categories)
                {
                    var a = category.Split(": ");
                    var value = int.Parse(a[1]);
                    switch (a[0])
                    {
                        case "children": children = value; break;
                        case "cats": cats = value; break;
                        case "samoyeds": samoyeds = value; break;
                        case "pomeranians": pomeranians = value; break;
                        case "akitas": akitas = value; break;
                        case "vizslas": vizslas = value; break;
                        case "goldfish": goldfish = value; break;
                        case "trees": trees = value; break;
                        case "cars": cars = value; break;
                        case "perfumes": perfumes = value; break;
                    }
                }
                ;
            }
        }
    }
}