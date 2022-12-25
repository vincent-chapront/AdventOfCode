using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode.Y2020
{
    internal class Day18 : GenericDay
    {
        private enum eOperations
        { Addition, Multiplication }

        private enum eState
        { Left, Operation, Right }

        public string Compute1(params string[] input)
        {
            Assert.AreEqual(6, Part1("2+4"));
            Assert.AreEqual(8, Part1("2*4"));
            Assert.AreEqual(24, Part1("20+4"));
            Assert.AreEqual(80, Part1("20*4"));
            Assert.AreEqual(8, Part1("2+4+2"));
            Assert.AreEqual(10, Part1("2*4+2"));
            Assert.AreEqual(18, Part1("2 + 4 * 3"));
            Assert.AreEqual(14, Part1("2+(4*3)"));
            Assert.AreEqual(13632, Part1("((2 + 4 * 9) * (6 + 9 * 8 + 6) + 6) + 2 + 4 * 2"));
            Assert.AreEqual(13652, Part1("((2 + 4 * 9) * (6 + 9 * 8 + 6) + 6) + 2 + 14 * 2"));

            return input.Select(x => Part1(x)).Sum().ToString();
        }

        public string Compute2(params string[] input)
        {
            Assert.AreEqual(51, ComputeReversePolishNotation(ReversePolishNotation("1 + (2 * 3) + (4 * (5 + 6))")));
            Assert.AreEqual(46, ComputeReversePolishNotation(ReversePolishNotation("2 * 3 + (4 * 5)")));
            Assert.AreEqual(1445, ComputeReversePolishNotation(ReversePolishNotation("5 + (8 * 3 + 9 + 3 * 4 * 3)")));
            Assert.AreEqual(669060, ComputeReversePolishNotation(ReversePolishNotation("5 * 9 * (7 * 3 * 3 + 9 * 3 + (8 + 6 * 4))")));
            Assert.AreEqual(23340, ComputeReversePolishNotation(ReversePolishNotation("((2 + 4 * 9) * (6 + 9 * 8 + 6) + 6) + 2 + 4 * 2")));

            return input.Select(x => ComputeReversePolishNotation(ReversePolishNotation(x))).Sum().ToString();
        }

        private static long ComputePart1(string input, ref int i)
        {
            input = input.Replace(" ", "");
            bool done = false;
            var currentTest = eState.Left;
            long left = 0;
            eOperations operation = eOperations.Addition;
            while (!done)
            {
                switch (currentTest)
                {
                    case eState.Left:

                        left = ReadOperand(input, ref i);
                        currentTest = eState.Operation;
                        break;

                    case eState.Operation:

                        operation = input[i] == '+' ? eOperations.Addition : eOperations.Multiplication;
                        currentTest = eState.Right;
                        i++;
                        break;

                    case eState.Right:
                        long right = ReadOperand(input, ref i);
                        left = operation == eOperations.Addition ? left + right : left * right;
                        currentTest = eState.Operation;
                        if (i >= input.Length || input[i] == ')')
                        {
                            done = true;
                            i++;
                        }
                        break;

                    default:
                        break;
                }
            }
            return left;
        }

        private static long ComputeReversePolishNotation(Stack<Token> stack)
        {
            var op = stack.Pop() as Operator;
            long operandLeft = ReadOperand(stack);
            long operandRight = ReadOperand(stack);
            if (op.IsAddition) return operandLeft + operandRight;
            if (op.IsProduct) return operandLeft * operandRight;
            return 0;
        }

        private static bool IsDigit(char current)
        {
            return '0' <= current && current <= '9';
        }

        private static bool IsOperandus(char c) => (c >= '0' && c <= '9' || c == '.');

        private static bool IsOperator(char c) => (c == '-' || c == '+' || c == '*' || c == '/');

        private static long Part1(string input)
        {
            int i = 0;
            return ComputePart1(input, ref i);
        }

        private static int Prior(char c)
            => c switch
            {
                '+' => 2,
                '*' => 1,
                _ => throw new ArgumentException(),
            };

        private static long ReadOperand(string input, ref int i)
        {
            if (input[i] == '(')
            {
                i++;
                return ComputePart1(input, ref i);
            }

            long left;
            var s1 = "";
            while (i < input.Length && IsDigit(input[i]))
            {
                s1 += input[i];
                i++;
            }
            left = long.Parse(s1);
            return left;
        }

        private static long ReadOperand(Stack<Token> stack)
        {
            return stack.Peek() is Operator
                ? ComputeReversePolishNotation(stack)
                : (stack.Pop() as Number).Value;
        }

        private static Stack<Token> ReversePolishNotation(string input)
        {
            Queue<Token> q = new Queue<Token>();
            Stack<Token> s = new Stack<Token>();

            Stack<char> stack = new Stack<char>();
            var str = input.Replace(" ", string.Empty);
            StringBuilder formula = new StringBuilder();
            for (int i = 0; i < str.Length; i++)
            {
                char x = str[i];
                if (x == '(')
                {
                    stack.Push(x);
                }
                else if (x == ')')
                {
                    while (stack.Count > 0 && stack.Peek() != '(')
                    {
                        char a = stack.Pop();
                        q.Enqueue(new Operator(a));
                        s.Push(new Operator(a));
                        formula.Append(a + " ");
                    }

                    stack.Pop();
                }
                else if (IsOperandus(x))
                {
                    var n = "";
                    while (i < str.Length && IsOperandus(str[i]))
                    {
                        n += str[i];
                        i++;
                    }
                    q.Enqueue(new Number(int.Parse(n)));
                    s.Push(new Number(int.Parse(n)));
                    formula.Append(n + " ");
                    i--;
                }
                else if (IsOperator(x))
                {
                    while (stack.Count > 0 && stack.Peek() != '(' && Prior(x) <= Prior(stack.Peek()))
                    {
                        char a = stack.Pop();
                        q.Enqueue(new Operator(a));
                        s.Push(new Operator(a));
                        formula.Append(a + " ");
                    }

                    stack.Push(x);
                }
                else
                {
                    char y = stack.Pop();
                    if (y != '(')
                    {
                        formula.Append(y + " ");
                    }
                }
            }
            while (stack.Count > 0)
            {
                var a = stack.Pop();
                formula.Append(a + " ");
                q.Enqueue(new Operator(a));
                s.Push(new Operator(a));
            }
            var res1 = q.Aggregate("", (acc, elm) => acc + " " + elm);
            var res2 = formula.ToString();
            Assert.AreEqual(" " + res2, res1 + " ");
            return s;
        }

        private class Number : Token
        {
            public Number(long value)
            {
                Value = value;
            }

            public long Value { get; }

            public override string ToString()
            {
                return Value.ToString();
            }
        }

        private class Operator : Token
        {
            public Operator(char symbol)
            {
                Symbol = symbol;
            }

            public bool IsAddition => Symbol == '+';
            public bool IsProduct => Symbol == '*';
            public char Symbol { get; }

            public override string ToString()
            {
                return Symbol.ToString();
            }
        }

        private class Token
        { }
    }
}