using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode.Y2021
{
    internal class Day21 : GenericDay
    {
        public string Compute1(string[] input, string args)
        {
            (int p1, int p2) = Parse(input);

            var dice = 1;
            var roll = 0;
            var p1Score = 0;
            var p2Score = 0;
            var player = 1;
            while (p1Score < 1000 && p2Score < 1000)
            {
                var move = dice++ + dice++ + dice++;
                roll += 3;
                if (player == 1)
                {
                    p1 = (p1 + move) % 10;
                    p1Score += p1 + 1;
                    player = 2;
                }
                else
                {
                    p2 = (p2 + move) % 10;
                    p2Score += p2 + 1;
                    player = 1;
                }
            }

            long res = roll;
            if (p1Score > p2Score)
            {
                res *= p2Score;
            }
            else
            {
                res *= p1Score;
            }
            return res.ToString();
        }

        public string Compute2(string[] input, string args)
        {
            Dictionary<int, int> dicoStat = new Dictionary<int, int>() {
                { 3,1},{ 4,3},{ 5,6},{ 6,7},{ 7,6},{ 8,3},{ 9,1}
            };

            (int p1Position, int p2Position) = Parse(input);

            var p1Win = 0L;
            var p2Win = 0L;

            Run(p1Position, 0, p2Position, 0, 1, 1, new List<int>());

            void Run(int p1Position, int p1Score, int p2Position, int p2Score, int player, int c, List<int> throws)
            {
                if (p1Score >= 21) { p1Win += c; return; }
                if (p2Score >= 21) { p2Win += c; return; }
                if (player == 1)
                {
                    for (int i = 3; i <= 9; i++)
                    {
                        int pos = (p1Position + i) % 10;
                        int score = p1Score + pos + 1;
                        var t = new List<int>(throws);
                        t.Add(i);
                        Run(pos, score, p2Position, p2Score, 2, c * dicoStat[i], t);
                    }
                }
                else
                {
                    for (int i = 3; i <= 9; i++)
                    {
                        int pos = (p2Position + i) % 10;
                        int score = p2Score + pos + 1;
                        var t = new List<int>(throws);
                        t.Add(i);
                        Run(p1Position, p1Score, pos, score, 1, c * dicoStat[i], t);
                    }
                }
            }
            return Math.Max(p1Win, p2Win).ToString();
        }

        private static (int p1, int p2) Parse(string[] input)
        {
            var p1 = int.Parse(input[0].Substring(input[0].LastIndexOf(' ') + 1)) - 1;
            var p2 = int.Parse(input[1].Substring(input[1].LastIndexOf(' ') + 1)) - 1;
            return (p1, p2);
        }
    }
}