using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Y2022
{
    internal class Day13 : GenericDay
    {
        public string Compute1(string[] input, string args)
        {
            var res = 0;
            var comparer = new Comparer();

            for (int i = 0; i < input.Length; i += 3)
            {
                var left = Parser.Parse(input[i]);
                var right = Parser.Parse(input[i + 1]);
                var comp = comparer.Compare(left, right);
                if (comp == 1) res += (i / 3) + 1;
            }
            return res.ToString();
        }

        public string Compute2(string[] input, string args)
        {
            var packets = new List<ItemList>
            {
                Parser.Parse("[[2]]"),
                Parser.Parse("[[6]]")
            };

            foreach (var item in input)
            {
                if (string.IsNullOrWhiteSpace(item)) continue;
                packets.Add(Parser.Parse(item));
            }

            var comparer = new Comparer();
            packets = packets.OrderByDescending(x => x, comparer).ToList();

            var res = 1;
            for (int i = 0; i < packets.Count; i++)
            {
                ItemList item = packets[i];
                if (item.ToString() == "[[2]]"
                    || item.ToString() == "[[6]]")
                {
                    res *= (i + 1);
                }
            }
            return res.ToString();
        }

        private class Comparer : IComparer<ItemList>
        {
            public int Compare(ItemList left, ItemList right)
            {
                var index = 0;
                while (true)
                {
                    if (left.Items.Count > index && right.Items.Count > index)
                    {
                        var leftItem = left.Items[index];
                        var rightItem = right.Items[index];
                        if (leftItem is ItemInteger leftItemInt
                            && rightItem is ItemInteger rightItemInt)
                        {
                            if (leftItemInt.Value < rightItemInt.Value)
                            {
                                return 1;
                            }
                            else if (leftItemInt.Value > rightItemInt.Value)
                            {
                                return -1;
                            }
                        }
                        else if (leftItem is ItemList leftItemList
                            && rightItem is ItemList rightItemList)
                        {
                            var a = Compare(leftItemList, rightItemList);
                            if (a != 0)
                            {
                                return a;
                            }
                        }
                        else
                        {
                            if (leftItem is ItemInteger item
                            && rightItem is ItemList rightItemList2)
                            {
                                var itemList = new ItemList();
                                itemList.Items.Add(item);
                                var a = Compare(itemList, rightItemList2);
                                if (a != 0)
                                {
                                    return a;
                                }
                            }
                            if (leftItem is ItemList leftItemList2
                            && rightItem is ItemInteger item2)
                            {
                                var itemList = new ItemList();
                                itemList.Items.Add(item2);
                                var a = Compare(leftItemList2, itemList);
                                if (a != 0)
                                {
                                    return a;
                                }
                            }
                        }

                        index++;
                    }
                    else if (left.Items.Count == index && right.Items.Count == index)
                    {
                        return 0;
                    }
                    else if (left.Items.Count == index)
                    {
                        return 1;
                    }
                    else if (right.Items.Count == index)
                    {
                        return -1;
                    }
                }
            }
        }

        private abstract class Item
        {
        }

        private class ItemInteger : Item
        {
            public ItemInteger(int value)
            {
                Value = value;
            }

            public int Value { get; set; }

            public override string ToString()
            {
                return Value.ToString();
            }
        }

        private class ItemList : Item
        {
            public List<Item> Items { get; } = new List<Item>();

            public override string ToString()
            {
                return "[" + string.Join(",", Items) + "]";
            }
        }

        private static class Parser
        {
            public static ItemList Parse(string input)
            {
                int idx = 1;
                return ParseList(input, ref idx);
            }

            private static ItemInteger ParseInteger(string input, ref int idx)
            {
                var res = "";
                for (int i = idx; i < input.Length; i++)
                {
                    if ('0' <= input[i] && input[i] <= '9')
                    {
                        idx++;
                        res += input[i];
                    }
                    else
                    {
                        break;
                    }
                }
                return new ItemInteger(Convert.ToInt32(res));
            }

            private static ItemList ParseList(string input, ref int idx)
            {
                var res = new ItemList();
                while (idx < input.Length)
                {
                    if (input[idx] == '[')
                    {
                        idx++;
                        var a = ParseList(input, ref idx);
                        res.Items.Add(a);
                    }
                    else if ('0' <= input[idx] && input[idx] <= '9')
                    {
                        var a = ParseInteger(input, ref idx);
                        res.Items.Add(a);
                    }
                    else if (input[idx] == ',')
                    {
                        idx++;
                    }
                    else if (input[idx] == ']')
                    {
                        idx++;
                        return res;
                    }
                }

                return res;
            }
        }
    }
}