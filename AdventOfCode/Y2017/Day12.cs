using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Y2017
{
    internal class Day12 : GenericDay
    {
        private static Dictionary<string, Program> Build(string[] input)
        {
            var dico = new Dictionary<string, Program>();
            foreach (string s in input)
            {
                var a = s.Split(" <-> ");
                var src = a[0];
                var dests = a[1].Split(", ");

                if (!dico.ContainsKey(src))
                {
                    dico.Add(src, new Program(src));
                }
                foreach (var dest in dests)
                {
                    if (dest == src)
                    {
                        continue;
                    }
                    if (!dico.ContainsKey(dest))
                    {
                        dico.Add(dest, new Program(dest));
                    }
                    dico[src].Neighboor.Add(dico[dest]);
                    dico[dest].Neighboor.Add(dico[src]);
                }
            }

            return dico;
        }

        public string Compute1(params string[] input)
        {
            Dictionary<string, Program> dico = Build(input);
            var prgm = dico["0"];
            List<string> neighboors = GetNeighboors(prgm);

            return neighboors.Count.ToString();
        }

        private static List<string> GetNeighboors(Program prgm)
        {
            var q = new Queue<Program>();
            q.Enqueue(prgm);
            var neighboors = new List<string>();
            while (q.Count > 0)
            {
                var p = q.Dequeue();
                if (neighboors.Contains(p.Name)) continue;
                neighboors.Add(p.Name);
                foreach (var n in p.Neighboor)
                {
                    q.Enqueue(n);
                }
            }

            return neighboors;
        }

        public string Compute2(params string[] input)
        {
            Dictionary<string, Program> dico = Build(input);
            var groups = 0;

            var programs = dico.Select(x => x.Value).ToList();
            while (programs.Count > 0)
            {
                groups++;
                foreach (var n in GetNeighboors(programs[0]))
                {
                    var idx = programs.FindIndex(0, programs.Count, x => x.Name == n);
                    programs.RemoveAt(idx);
                }
            }

            return groups.ToString();
        }

        private class Program
        {
            public string Name;
            public List<Program> Neighboor = new List<Program>();

            public Program(string name)
            {
                Name = name;
            }
        }
    }
}