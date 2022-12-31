using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode.Y2021
{
    internal class Day16 : GenericDay
    {
        public string Compute1(string[] input, string args) => Parse(input[0]).GetVersionSum().ToString();

        public string Compute2(string[] input, string args) => Parse(input[0]).GetValue().ToString();

        private static Packet Parse(string input)
        {
            string binarystring = string.Join(string.Empty, input.Select(c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')));

            var idx = 0;

            return Parse(binarystring, ref idx);

            static Packet Parse(string s, ref int idx)
            {
                var version = Convert.ToInt32(s.Substring(idx, 3), 2);
                var typeId = Convert.ToInt32(s.Substring(idx + 3, 3), 2);
                idx += 6;
                if (typeId == 4)
                {
                    StringBuilder sb = new StringBuilder();
                    var done = false;
                    while (!done)
                    {
                        sb.Append(s.Substring(idx + 1, 4));
                        idx += 5;
                        if (s[idx - 5] == '0')
                        {
                            break;
                        }
                    }

                    return new PacketLiteralValue(typeId, version, Convert.ToInt64(sb.ToString(), 2));
                }
                else
                {
                    var lengthTypeId = s[idx];
                    idx++;
                    var operation = new PacketOperation(typeId, version);
                    if (lengthTypeId == '0')
                    {
                        var lengthSubPackets = Convert.ToInt32(s.Substring(idx, 15), 2);
                        idx += 15;
                        var startingIdx = idx;

                        while (idx - startingIdx < lengthSubPackets)
                        {
                            var p = Parse(s, ref idx);
                            operation.SubPackets.Add(p);
                        }
                    }
                    else
                    {
                        var numberSubPackets = Convert.ToInt32(s.Substring(idx, 11), 2);
                        idx += 11;
                        for (int i = 0; i < numberSubPackets; i++)
                        {
                            var p = Parse(s, ref idx);
                            operation.SubPackets.Add(p);
                        }
                    }
                    return operation;
                }
            }
        }

        private abstract class Packet
        {
            protected readonly int typeId;
            protected readonly int version;

            protected Packet(int typeId, int version)
            {
                this.typeId = typeId;
                this.version = version;
            }

            public abstract long GetValue();

            public abstract long GetVersionSum();
        }

        private class PacketLiteralValue : Packet
        {
            private readonly long value;

            public PacketLiteralValue(int typeId, int version, long value) : base(typeId, version)
            {
                this.value = value;
            }

            public override long GetValue()
            {
                return value;
            }

            public override long GetVersionSum()
            {
                return version;
            }
        }

        private class PacketOperation : Packet
        {
            public readonly List<Packet> SubPackets = new List<Packet>();

            public PacketOperation(int typeId, int version) : base(typeId, version)
            {
            }

            public override long GetValue()
            {
                return typeId switch
                {
                    0 => SubPackets.Sum(x => x.GetValue()),
                    1 => SubPackets.Select(x => x.GetValue()).Product(),
                    2 => SubPackets.Min(x => x.GetValue()),
                    3 => SubPackets.Max(x => x.GetValue()),
                    5 => SubPackets[0].GetValue() > SubPackets[1].GetValue() ? 1 : 0,
                    6 => SubPackets[0].GetValue() < SubPackets[1].GetValue() ? 1 : 0,
                    7 => SubPackets[0].GetValue() == SubPackets[1].GetValue() ? 1 : 0,
                    _ => throw new NotImplementedException(),
                };
            }

            public override long GetVersionSum()
            {
                return SubPackets.Sum(x => x.GetVersionSum()) + version;
            }
        }
    }
}