using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Y2021
{
    internal class Day24 : GenericDay
    {
        private static void Clean(string[] input)
        {
            List<string> l = new List<string>();
            var toClean = new List<string> { "inp w",
"mul x 0",
"add x z",
"mod x 26",
"eql x w",
"eql x 0",
"mul y 0",
"add y 25",
"mul y x",
"add y 1",
"mul z y",
"mul y 0",
"add y w",
"mul y x",
"add z y"};
            foreach (var line in input)
            {
                if (!toClean.Contains(line) && !line.StartsWith("div z "))
                {
                    l.Add(line);
                }
            }
            var cleaned = new List<string>();
            for (int i = 0; i < 14; i++)
            {
                int check = int.Parse(l[i * 2].Split(" ")[2]);
                int offset = int.Parse(l[i * 2 + 1].Split(" ")[2]);
                if (check > 0)
                {
                    cleaned.Add($"PUSH input[{i}] + {offset}");
                }
                else
                {
                    cleaned.Add($"POP. Must have input[{i}] == popped_value - {offset}");
                }
                ;
            }
            ;
        }

        public string Compute1(string[] input, string args)
        {
            Clean(input);
            throw new NotImplementedException();
        }
        

        public string Compute2(string[] input, string args)
        {
            Clean(input);
            throw new NotImplementedException();
        }

        private static bool IsValid(string[] input, long value)
        {
            var dico = new Dictionary<string, long>
            {
                {"w",0 },
                {"x",0 },
                {"y",0 },
                {"z",0 }
            };
            var stack = new Queue<int>(value.ToString().Select(x => x - '0').ToList());
            if (stack.Contains(0)) return false;

            long getValue(string s) => dico.ContainsKey(s) ? dico[s] : long.Parse(s);

            foreach (var line in input)
            {
                var a = line.Split(' ');
                switch (a[0])
                {
                    case "inp":
                        dico[a[1]] = stack.Dequeue();
                        break;

                    case "add":
                        dico[a[1]] += getValue(a[2]);
                        break;

                    case "mul":
                        dico[a[1]] *= getValue(a[2]);
                        break;

                    case "div":
                        dico[a[1]] /= getValue(a[2]);
                        break;

                    case "mod":
                        dico[a[1]] %= getValue(a[2]);
                        break;

                    case "eql":
                        dico[a[1]] = dico[a[1]] == getValue(a[2]) ? 1 : 0;
                        break;

                    default:
                        break;
                }
            }
            return dico["z"] == 0;
        }

        private static List<Action<Queue<int>, long[]>> Parse(string[] input)
        {
            var res = new List<Action<Queue<int>, long[]>>();
            bool isDigit(char c) => '0' <= c && c <= '9';
            foreach (var line in input)
            {
                var a = line.Split(' ');
                var target = a[1].First() - 'w';
                if (a[0] == "inp")
                {
                    res.Add((stack, l) =>
                    {
                        l[target] = stack.Dequeue();
                    });
                }
                else
                {
                    int val;
                    if (int.TryParse(a[2], out val))
                    {
                        switch (a[0])
                        {
                            case "add":
                                res.Add((stack, l) =>
                                {
                                    l[target] += val;
                                });
                                break;

                            case "mul":
                                res.Add((stack, l) =>
                                {
                                    l[target] *= val;
                                });
                                break;

                            case "div":
                                res.Add((stack, l) =>
                                {
                                    l[target] /= val;
                                });
                                break;

                            case "mod":
                                res.Add((stack, l) =>
                                {
                                    l[target] %= val;
                                });
                                break;

                            case "eql":
                                res.Add((stack, l) =>
                                {
                                    l[target] = l[target] == val ? 1 : 0;
                                });
                                break;
                        }
                    }
                    else
                    {
                        var source = a[2].First() - 'w';
                        if (source == -74)
                        {
                            ;
                        }
                        switch (a[0])
                        {
                            case "add":
                                res.Add((stack, l) =>
                                {
                                    l[target] += l[source];
                                });
                                break;

                            case "mul":
                                res.Add((stack, l) =>
                                {
                                    l[target] *= l[source];
                                });
                                break;

                            case "div":
                                res.Add((stack, l) =>
                                {
                                    l[target] /= l[source];
                                });
                                break;

                            case "mod":
                                res.Add((stack, l) =>
                                {
                                    l[target] %= l[source];
                                });
                                break;

                            case "eql":
                                res.Add((stack, l) =>
                                {
                                    l[target] = l[target] == l[source] ? 1 : 0;
                                });
                                break;
                        }
                    }
                }
            }

            return res;
        }
    }
}