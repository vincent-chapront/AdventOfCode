using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode.Y2020
{
    internal class Day08 : GenericDay
    {
        public string Compute1(string[] input, string args)
        {
            var instructions = input.Select(x => new Instruction(x)).ToArray();
            var res = RunProgram(instructions);
            return res.acc.ToString();
        }

        public string Compute2(string[] input, string args)
        {
            var instructions = input.Select(x => new Instruction(x)).ToArray();
            (int acc, bool success) res;
            for (int i = 0; i < instructions.Length; i++)
            {
                res = RunProgram(instructions);
                if (res.success)
                {
                    return res.acc.ToString();
                }
                if (instructions[i].Operation == Instruction.eOperation.Jmp)
                {
                    instructions[i].Operation = Instruction.eOperation.Nop;

                    res = RunProgram(instructions);
                    if (res.success)
                    {
                        return res.acc.ToString();
                    }
                    instructions[i].Operation = Instruction.eOperation.Jmp;
                }
                if (instructions[i].Operation == Instruction.eOperation.Nop)
                {
                    instructions[i].Operation = Instruction.eOperation.Jmp;

                    res = RunProgram(instructions);
                    if (res.success)
                    {
                        return res.acc.ToString();
                    }
                    instructions[i].Operation = Instruction.eOperation.Nop;
                }
            }
            return int.MaxValue.ToString();
        }

        private static (int acc, bool success) RunProgram(Instruction[] instructions)
        {
            var instructionIdxRan = new List<int>();
            int idx = 0;
            int acc = 0;
            while (!instructionIdxRan.Contains(idx) && idx < instructions.Length)
            {
                instructionIdxRan.Add(idx);
                var instruction = instructions[idx];
                switch (instruction.Operation)
                {
                    case Instruction.eOperation.Nop:
                        idx++;
                        break;

                    case Instruction.eOperation.Acc:
                        idx++;
                        acc += instruction.Value;
                        break;

                    case Instruction.eOperation.Jmp:
                        idx += instruction.Value;
                        break;

                    default:
                        break;
                }
            }
            return (acc, idx >= instructions.Length);
        }

        private class Instruction
        {
            public Instruction(string instruction)
            {
                var v = instruction.Split(" ");
                Operation = v[0] switch
                {
                    "nop" => eOperation.Nop,
                    "acc" => eOperation.Acc,
                    "jmp" => eOperation.Jmp,
                    _ => throw new System.Exception()
                };
                Value = int.Parse(v[1]);
            }

            public enum eOperation
            {
                Nop,
                Acc,
                Jmp
            }

            public eOperation Operation { get; set; }
            public int Value { get; }
        }
    }
}