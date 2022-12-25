using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode.Y2016
{
    internal class Day10 : GenericDay
    {
        protected override object Part1()
        {

            //Assert.AreEqual(2, Compute1(Resources.Year2016.Day10Test.ToLines(), 2, 5));
            var res = Compute1(Resources.Year2016.Day10.ToLines(), 17, 61);
            Assert.AreEqual(181, res);
            return res;
        }

        protected override object Part2()
        {
            var res = Compute2(Resources.Year2016.Day10.ToLines());
            Assert.AreEqual(12567, res);
            return res;
        }

        private static long Compute1(string[] input, int v1, int v2, bool showDebug=false)
        {
            if (showDebug)
            {
                Console.Clear();
                for (int i = 0; i < 210; i++)
                {
                    Console.SetCursorPosition(16 * (i / 35), i % 35);
                    Console.Write(i.ToString().PadLeft(3, ' ') + "-   -     /  ");
                }
            }

            var queue = new Queue<Instruction>(input.Select(x=>Instruction.Parse(x)));

            Dictionary<int, List<int>> bots = new Dictionary<int, List<int>>();

            int prev = 0;
            Action<int,bool> Display = (id,isNew) =>
            {
                if (showDebug)
                {
                    if (bots.ContainsKey(id))
                    {
                        Console.SetCursorPosition(16 * (id / 35), id % 35);
                        if (isNew)
                        {
                            Console.BackgroundColor = ConsoleColor.DarkRed;
                        }
                        Console.Write(
                            id.ToString().PadLeft(3, ' ')
                            + "-"
                            + (bots[id].Count > 0 ? bots[id][0].ToString().PadLeft(3, ' ') : "   ")
                            + "-"
                            + (bots[id].Count > 1 ? bots[id][1].ToString().PadLeft(3, ' ') : "   ")
                            );
                        Console.BackgroundColor = ConsoleColor.Black;
                    }
                }
            };
            Action<int> Clear = (id) =>
            {
                if (showDebug)
                {
                    if (bots.ContainsKey(id))
                    {
                        Console.SetCursorPosition(16 * (id / 35), id % 35);
                        Console.BackgroundColor = ConsoleColor.DarkBlue;
                        Console.Write(
                            id.ToString().PadLeft(3, ' ')
                            + "-"
                            + (bots[id].Count > 0 ? bots[id][0].ToString().PadLeft(3, ' ') : "   ")
                            + "-"
                            + (bots[id].Count > 1 ? bots[id][1].ToString().PadLeft(3, ' ') : "   ")
                            );
                        Console.BackgroundColor = ConsoleColor.Black;
                    }
                }
            };

            Action<int, int> add = (id, val) =>
            {
                if (!bots.ContainsKey(id))
                {
                    bots.Add(id, new List<int>());
                }
                bots[id].Add(val);

                Display(prev, false);
                Display(id, true);
                prev = id;
                //Console.ReadKey();
            };

            while (queue.Count > 0)
            {
                var instruction = queue.Dequeue();

                if (showDebug)
                {
                    Console.SetCursorPosition(0, 37);
                    Console.WriteLine(new string(' ', 120));
                    Console.SetCursorPosition(0, 37);
                    Console.WriteLine(instruction.Raw);
                }
                if(instruction is InstructionValue iv)
                {
                    add(iv.Bot, iv.Value);
                }
                else if(instruction is InstructionBot iv2)
                {
                    if(!bots.ContainsKey(iv2.BotSource) || bots[iv2.BotSource].Count!=2)
                    {
                        queue.Enqueue(instruction);
                        continue;
                    }

                    if (bots[iv2.BotSource].Min() == Math.Min(v1,v2)
                        && bots[iv2.BotSource].Max() == Math.Max(v1, v2))
                    {
                        return iv2.BotSource;
                    }

                    if(iv2.DestinationLowType== DestinationTypes.Bot)
                    {
                        add(iv2.DestinationLowId, bots[iv2.BotSource].Min());

                    }

                    if(iv2.DestinationHighType== DestinationTypes.Bot)
                    {
                        add(iv2.DestinationHighId, bots[iv2.BotSource].Max());
                    }
                    Clear(iv2.BotSource);
                    bots.Remove(iv2.BotSource);

                }
            }

            return 0;
        }

        private static long Compute2(string[] input, bool showDebug=false)
        {
            if (showDebug)
            {
                Console.Clear();
                for (int i = 0; i < 210; i++)
                {
                    Console.SetCursorPosition(16 * (i / 35), i % 35);
                    Console.Write(i.ToString().PadLeft(3, ' ') + "-   -     /  ");
                }
            }

            var queue = new Queue<Instruction>(input.Select(x=>Instruction.Parse(x)));

            Dictionary<int, List<int>> bots = new Dictionary<int, List<int>>();
            Dictionary<int, List<int>> output = new Dictionary<int, List<int>>();

            int prev = 0;
            Action<int,bool> Display = (id,isNew) =>
            {
                if (showDebug)
                {
                    if (bots.ContainsKey(id))
                    {
                        Console.SetCursorPosition(16 * (id / 35), id % 35);
                        if (isNew)
                        {
                            Console.BackgroundColor = ConsoleColor.DarkRed;
                        }
                        Console.Write(
                            id.ToString().PadLeft(3, ' ')
                            + "-"
                            + (bots[id].Count > 0 ? bots[id][0].ToString().PadLeft(3, ' ') : "   ")
                            + "-"
                            + (bots[id].Count > 1 ? bots[id][1].ToString().PadLeft(3, ' ') : "   ")
                            );
                        Console.BackgroundColor = ConsoleColor.Black;
                    }
                }
            };
            Action<int> Clear = (id) =>
            {
                if (showDebug)
                {
                    if (bots.ContainsKey(id))
                    {
                        Console.SetCursorPosition(16 * (id / 35), id % 35);
                        Console.BackgroundColor = ConsoleColor.DarkBlue;
                        Console.Write(
                            id.ToString().PadLeft(3, ' ')
                            + "-"
                            + (bots[id].Count > 0 ? bots[id][0].ToString().PadLeft(3, ' ') : "   ")
                            + "-"
                            + (bots[id].Count > 1 ? bots[id][1].ToString().PadLeft(3, ' ') : "   ")
                            );
                        Console.BackgroundColor = ConsoleColor.Black;
                    }
                }
            };

            Action<Dictionary<int, List<int>>, int, int> add = (dico, id, val) =>
            {
                if (!bots.ContainsKey(id))
                {
                    dico.Add(id, new List<int>());
                }
                dico[id].Add(val);

                Display(prev, false);
                Display(id, true);
                prev = id;
                //Console.ReadKey();
            };

            int? output0 = null;
            int? output1 = null;
            int? output2= null;
            while (queue.Count > 0)
            {
                var instruction = queue.Dequeue();

                if (showDebug)
                {
                    Console.SetCursorPosition(0, 37);
                    Console.WriteLine(new string(' ', 120));
                    Console.SetCursorPosition(0, 37);
                    Console.WriteLine(instruction.Raw);
                }
                if(instruction is InstructionValue iv)
                {
                    add(bots, iv.Bot, iv.Value);
                }
                else if(instruction is InstructionBot iv2)
                {
                    if(!bots.ContainsKey(iv2.BotSource) || bots[iv2.BotSource].Count!=2)
                    {
                        queue.Enqueue(instruction);
                        continue;
                    }

                    /*if (bots[iv2.BotSource].Min() == Math.Min(v1,v2)
                        && bots[iv2.BotSource].Max() == Math.Max(v1, v2))
                    {
                        return iv2.BotSource;
                    }*/

                    if(iv2.DestinationLowType== DestinationTypes.Bot)
                    {
                        add(bots, iv2.DestinationLowId, bots[iv2.BotSource].Min());
                    }
                    else
                    {
                        add(output, iv2.DestinationLowId, bots[iv2.BotSource].Min());
                        if (iv2.DestinationLowId == 0) output0 = bots[iv2.BotSource].Min();
                        if (iv2.DestinationLowId == 1) output1 = bots[iv2.BotSource].Min();
                        if (iv2.DestinationLowId == 2) output2 = bots[iv2.BotSource].Min();
                    }

                    if(iv2.DestinationHighType== DestinationTypes.Bot)
                    {
                        add(bots, iv2.DestinationHighId, bots[iv2.BotSource].Max());
                    }
                    else
                    {
                        add(output, iv2.DestinationHighId, bots[iv2.BotSource].Max());
                        if (iv2.DestinationHighId == 0) output0 = bots[iv2.BotSource].Max();
                        if (iv2.DestinationHighId == 1) output1 = bots[iv2.BotSource].Max();
                        if (iv2.DestinationHighId == 2) output2 = bots[iv2.BotSource].Max();

                    }
                    if(output0!=null && output1!= null && output2!= null)
                    {
                        return (long)output0.Value * (long)output1.Value * (long)output2.Value;
                    }
                    Clear(iv2.BotSource);
                    bots.Remove(iv2.BotSource);

                }
            }

            return 0;
        }

        abstract class Instruction 
        {
            static Regex RegexValue = new Regex(@"value (\d+) goes to bot (\d+)");
            static Regex RegexBot = new Regex(@"bot (\d+) gives low to (\w+) (\d+) and high to (\w+) (\d+)");

            public string Raw { get; internal set; }

            internal static Instruction Parse(string s)
            {
                var regexValue = RegexValue.Match(s);
                if (regexValue.Success)
                {
                    var res = new InstructionValue {Raw=s, Value = int.Parse(regexValue.Groups[1].Value), Bot = int.Parse(regexValue.Groups[2].Value)};
                    return res;
                }
                else
                {
                    var regexBot = RegexBot.Match(s);
                    if (regexBot.Success)
                    {
                        var lowType = regexBot.Groups[2].Value switch
                        {
                            "bot" => DestinationTypes.Bot,
                            "output" => DestinationTypes.Output
                        };
                        var highType = regexBot.Groups[4].Value switch
                        {
                            "bot" => DestinationTypes.Bot,
                            "output" => DestinationTypes.Output
                        };
                        var res = new InstructionBot
                        {
                            Raw = s,
                            BotSource = int.Parse(regexBot.Groups[1].Value),
                            DestinationLowId = int.Parse(regexBot.Groups[3].Value),
                            DestinationHighId = int.Parse(regexBot.Groups[5].Value),
                            DestinationHighType = highType,
                            DestinationLowType = lowType,
                        };

                        return res;
                    }
                }
                return new InstructionValue();
            }

        }
        class InstructionValue:Instruction
        {
            public int Value { get; set; }
            public int Bot { get; set; }
        }
        class InstructionBot : Instruction
        {
            public int BotSource { get; set; }
            public int DestinationLowId { get; set; }
            public int DestinationHighId { get; set; }
            public DestinationTypes DestinationLowType { get; set; }
            public DestinationTypes DestinationHighType { get; set; }

        }

        enum DestinationTypes
        {
            Bot,
            Output
        }
    }
}