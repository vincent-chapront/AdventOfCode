using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace AdventOfCode.Y2022
{
    internal class Day11 : GenericDay
    {
        public string Compute1(string[] input)
        {
            var monkeys = Parse(input);
            for (int i = 0; i < 20; i++)
            {
                foreach (var m in monkeys)
                {
                    Turn1(m, monkeys);
                }
            }
            var a = monkeys.OrderByDescending(x => x.InspectedItems).Take(2).Product(x => x.InspectedItems);
            return a.ToString();
        }

        public string Compute2(string[] input)
        {
            var monkeys = Parse(input);
            var turn = 0;
            var productOfDivisor = monkeys.Select(x => x.Test).Product();
            for (int i = 0; i < 10000; i++)
            {
                turn++;
                foreach (var m in monkeys)
                {
                    Turn2(m, monkeys, productOfDivisor);
                }
                if (turn == 1 || turn == 20 || turn % 1000 == 0)
                {
                    ;
                }
            }
            var a = monkeys.OrderByDescending(x => x.InspectedItems).Take(2).Product(x => x.InspectedItems);
            return a.ToString();
        }

        private static string ReadId(Queue<string> input)
        {
            var line = input.Dequeue();
            return line.Substring(7, line.Length - 8);
        }

        private static List<long> ReadItems(Queue<string> input)
        {
            var line = input.Dequeue();
            line = line.Substring(line.IndexOf(":") + 2);
            return line.Split(", ").Select(x => Convert.ToInt64(x)).ToList();
        }

        private static string ReadOperation(Queue<string> input)
        {
            var line = input.Dequeue();
            return line.Substring(line.IndexOf(" old ") + 5);
        }

        private static string ReadTarget(Queue<string> input)
        {
            var line = input.Dequeue();
            return line.Substring(line.IndexOf("monkey ") + 7);
        }

        private static int ReadTest(Queue<string> input)
        {
            var line = input.Dequeue();
            return Convert.ToInt32(line.Substring(line.IndexOf("by ") + 3));
        }

        private long CalcNewItem(Monkey monkey, long item)
        {
            var a = monkey.Operation.Split(" ");
            if (a[0] == "*")
            {
                if (a[1] == "old")
                {
                    return item * item;
                }
                else
                {
                    return item* Convert.ToInt64(a[1]);
                }
            }
            else
            {
                if (a[1] == "old")
                {
                    return item+ item;
                }
                else
                {
                    return item+ Convert.ToInt64(a[1]);
                }
            }
        }

        private List<Monkey> Parse(string[] input)
        {
            var inputQueue = new Queue<string>(input);
            var monkeys = new List<Monkey>();
            while (inputQueue.Count > 0)
            {
                monkeys.Add(ParseMonkey(inputQueue));
                if (inputQueue.Count > 0)
                {
                    inputQueue.Dequeue();
                }
            }
            return monkeys;
        }

        private Monkey ParseMonkey(Queue<string> input)
        {
            var m = new Monkey();
            m.Id = ReadId(input);
            m.Items.AddRange(ReadItems(input));
            m.Operation = ReadOperation(input);
            m.Test = ReadTest(input);

            m.TargetTrue = ReadTarget(input);
            m.TargetFalse = ReadTarget(input);
            return m;
        }

        private void Turn1(Monkey monkey, List<Monkey> monkeys)
        {
            while (monkey.Items.Count > 0)
            {
                monkey.InspectedItems++;
                var item = monkey.Items[0];
                monkey.Items.RemoveAt(0);
                item = CalcNewItem(monkey, item);
                item = item / 3;
                if (item % monkey.Test == 0)
                {
                    monkeys.FirstOrDefault(x => x.Id == monkey.TargetTrue).Items.Add(item);
                }
                else
                {
                    monkeys.FirstOrDefault(x => x.Id == monkey.TargetFalse).Items.Add(item);
                }
            }
        }

        private void Turn2(Monkey monkey, List<Monkey> monkeys, long productOfDivisor)
        {
            while (monkey.Items.Count > 0)
            {
                monkey.InspectedItems++;
                var item = monkey.Items[0];
                monkey.Items.RemoveAt(0);
                item = CalcNewItem(monkey, item);
                item = item % productOfDivisor;
                //item = item / 3;
                if (item % monkey.Test == 0)
                {
                    monkeys.FirstOrDefault(x => x.Id == monkey.TargetTrue).Items.Add(item);
                }
                else
                {
                    monkeys.FirstOrDefault(x => x.Id == monkey.TargetFalse).Items.Add(item);
                }
            }
        }

        private class Monkey
        {
            public string Id { get; set; }
            public long InspectedItems { get; set; } = 0;
            public List<long> Items { get; } = new List<long>();

            public string Operation { get; set; }
            public string TargetFalse { get; set; }
            public string TargetTrue { get; set; }
            public int Test { get; set; }
        }
    }
}