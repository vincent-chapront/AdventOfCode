using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode.Y2021
{
    internal class Day12 : GenericDay
    {
        private static bool CanNotRevisitPart1(Stack<Node> visited, Node node)
        {
            return visited.Contains(node);
        }

        private static bool CanNotRevisitPart2(Stack<Node> visited, Node node)
        {
            return visited.Contains(node) && visited.Where(x => x.IsSmall).GroupBy(x => x.Name).ToDictionary(x => x.Key, x => x.ToList()).Any(x => x.Value.Count != 1);
        }

        private static List<List<Node>> Compute(string[] input, Func<Stack<Node>, Node, bool> canNotRevisit)
        {
            var nodes = new List<Node>();
            foreach (string s in input)
            {
                var a = s.Split("-");
                var node1 = GetNode(nodes, a[0]);
                var node2 = GetNode(nodes, a[1]);
                node1.AddNode(node2);
                node2.AddNode(node1);
            }

            var start = nodes.First(x => x.IsStart);

            var result = new List<List<Node>>();
            ListPath(start, new Stack<Node>(), result, canNotRevisit);
            return result;
        }

        public string Compute1(string[] input, string args)
        {
            return Compute(input, CanNotRevisitPart1).Count.ToString();
        }

        public string Compute2(string[] input, string args)
        {
            return Compute(input, CanNotRevisitPart2).Count.ToString();
        }

        private static Node GetNode(List<Node> nodes, string name)
        {
            var node = nodes.FirstOrDefault(x => x.Name == name);
            if (node == null)
            {
                node = new Node(name);
                nodes.Add(node);
            }

            return node;
        }

        private static void ListPath(Node start, Stack<Node> visited, List<List<Node>> result, Func<Stack<Node>, Node, bool> canNotRevisit)
        {
            visited.Push(start);
            foreach (var node in start.Nodes)
            {
                if (node.IsEnd)
                {
                    visited.Push(node);
                    var path = new List<Node>(visited);
                    path.Reverse();
                    result.Add(path);
                    visited.Pop();
                }
                else
                {
                    if (node.IsStart || node.IsEnd)
                    {
                        continue;
                    }
                    if (node.IsSmall)
                    {
                        if (canNotRevisit(visited, node))
                        {
                            continue;
                        }
                    }
                    ListPath(node, visited, result, canNotRevisit);
                }
            }
            visited.Pop();
        }

        public class Node
        {
            public Node(string name)
            {
                Name = name;
            }

            public bool IsEnd => Name == "end";
            public bool IsLarge => !IsStart && !IsEnd && Name == Name.ToUpper();
            public bool IsSmall => !IsStart && !IsEnd && !IsLarge;
            public bool IsStart => Name == "start";
            public string Name { get; }
            public List<Node> Nodes { get; } = new List<Node>();

            public void AddNode(Node n)
            {
                Nodes.Add(n);
            }

            public override string ToString()
            {
                return Name + "(" + (IsLarge ? "Large" : "Small") + ")" + " - " + string.Join(", ", Nodes.Select(x => x.Name));
            }
        }
    }
}