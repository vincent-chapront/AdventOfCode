using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Y2017
{
    internal class Day07 : GenericDay
    {
        public string Compute1(params string[] input)
        {
            Dictionary<string, Program> dico = Build(input);
            var root = dico.First(x => x.Value.Parent == null).Value;
            return root.Name;
        }

        private static Dictionary<string, Program> Build(string[] input)
        {
            var dico = new Dictionary<string, Program>();
            foreach (var line in input)
            {
                //fwft (72) -> ktlj, cntj, xhth
                var name = line.Substring(0, line.IndexOf(' '));
                int weight = GetWeight(line);

                var idx1 = line.IndexOf(" -> ");
                var children = new string[0];
                if (idx1 > -1)
                {
                    var s = line.Substring(idx1 + 4);
                    children = s.Split(", ");
                }
                ;
                if (!dico.ContainsKey(name))
                {
                    dico.Add(name, new Program { Name = name });
                }
                dico[name].Weight = weight;

                foreach (var child in children)
                {
                    if (!dico.ContainsKey(child))
                    {
                        dico.Add(child, new Program { Name = child });
                    }
                    dico[child].Parent = dico[name];
                    dico[name].SubPrograms.Add(dico[child]);
                }
            }

            return dico;
        }

        private static int GetWeight(string line)
        {
            var idx1 = line.IndexOf('(');
            var idx2 = line.IndexOf(')');
            var weight = int.Parse(line.Substring(idx1 + 1, idx2 - idx1 - 1));
            return weight;
        }

        public string Compute2(params string[] input)
        {
            Dictionary<string, Program> dico = Build(input);

            var root = dico.First(x => x.Value.Parent == null).Value;
            var mainroot = dico.First(x => x.Value.Parent == null).Value;
            while (true)
            {
                bool hasUnbalanced = false;
                //if(root.IsBalanced)

                foreach(var sub in root.SubPrograms)
                {
                    if (!sub.IsBalanced)
                    {
                        root = sub;
                        hasUnbalanced = true;
                        break;
                    }
                }
                if (!hasUnbalanced && !root.IsBalanced)
                {
                    var a= root.SubPrograms.GroupBy(x => x.FullWeight).ToDictionary(x => x.Key, x => x.ToList());
                    var validWeight = a.Where(x => x.Value.Count > 1).First().Key;
                    var invalid = root.SubPrograms.First(x => x.FullWeight != validWeight);
                    return invalid.Weight + (validWeight - invalid.FullWeight).ToString();
                }
            }
        }

        private class Program
        {
            public string Name;
            public int Weight;
            public List<Program> SubPrograms = new List<Program>();
            public Program Parent = null;
            public int FullWeight => Weight + SubPrograms.Sum(x => x.FullWeight);
            public bool IsBalanced
            {
                get
                {
                    if(SubPrograms.Count<=1)
                    {
                        return true;
                    }
                    var weights = SubPrograms.Select(x => x.FullWeight).ToArray();
                    for (int i = 1; i < weights.Length; i++)
                    {
                        if(weights[i]!=weights[0])
                        {
                            return false;
                        }
                    }
                    return true;
                }
            }
        }
    }
}