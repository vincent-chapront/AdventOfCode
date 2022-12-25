using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode.Y2022
{
    internal class Day05 : GenericDay
    {
        //protected override object Part1()
        //{
        //    Assert.AreEqual("CMZ", Compute1(Resources.Year2022.Day05Test.ToLines()));
        //    var res = Compute1(Resources.Year2022.Day05.ToLines());
        //    Assert.AreEqual("ZSQVCCJLL", res);
        //    return res;
        //}

        //protected override object Part2()
        //{
        //    Assert.AreEqual("MCD", Compute2(Resources.Year2022.Day05Test.ToLines()));
        //    var res = Compute2(Resources.Year2022.Day05.ToLines());
        //    Assert.AreEqual("QZFJRWHGS", res);
        //    return res;
        //}

        public string Compute1(string[] input)
        {
            return Run1(input);
        }

        public string Compute2(string[] input)
        {
            return Run2(input);
        }
        record Step(int n, int from, int to,string Full);
        private static string Run1(string[] input)
        {
            (List<Step> steps, Stack<string>[] stacks) = Parse(input);

            foreach (var step in steps)
            {
                for (int i = 0; i < step.n; i++)
                {
                        var a = stacks[step.from - 1].Pop();
                        stacks[step.to - 1].Push(a);
                }
            }

            var res = "";
            foreach (var stack in stacks)
            {
                res += stack.Peek();
            }
            return res;
        }
        private static string Run2(string[] input)
        {
            (List<Step> steps, Stack<string>[] stacks) = Parse(input);

            foreach (var step in steps)
            {
                var tmpStack = new Stack<string>();
                for (int i = 0; i < step.n; i++)
                {
                    var a = stacks[step.from - 1].Pop();
                    tmpStack.Push(a);
                }
                while (tmpStack.Count > 0)
                {
                    stacks[step.to - 1].Push(tmpStack.Pop());
                }
            }

            var res = "";
            foreach (var stack in stacks)
            {
                res += stack.Peek();
            }
            return res;
        }

        private static (List<Step> steps, Stack<string>[] stacks) Parse(string[] input)
        {
            var initialState = input.TakeWhile(x => !string.IsNullOrEmpty(x)).ToList();
            var currentState = initialState.Take(initialState.Count - 1).ToList();

            var regex = new Regex(@"move (\d+) from (\d+) to (\d+)");
            List<Step>  steps = input
                .Skip(initialState.Count + 1)
                .Select(x => regex.Match(x).Groups)
                .Select(x => new Step(int.Parse(x[1].ToString()), int.Parse(x[2].ToString()), int.Parse(x[3].ToString()), x[0].ToString()))
                .ToList();
            Stack<string>[]  stacks = new Stack<string>[(initialState.Last().Length + 1) / 4];
            for (int i = 0; i < stacks.Length; i++)
            {
                stacks[i] = new Stack<string>();
            }

            for (int i = currentState.Count - 1; i >= 0; i--)
            {
                for (int idx = 0; idx < stacks.Length; idx++)
                {
                    var c = currentState[i].Substring(idx * 4 + 1, 1);
                    if (string.IsNullOrWhiteSpace(c))
                    {
                        continue;
                    }

                    stacks[idx].Push(c);
                }
            }
            return (steps, stacks);
        }
    }
}