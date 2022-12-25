using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode.Y2020
{
    internal class Day22 : GenericDay
    {
        private static int game = 0;

        protected override object Part1()
        {
            throw new NotImplementedException();
            Assert.AreEqual(306, Compute1(Resources.Year2020.Day22Test.ToLines()));

            var res = Compute1(Resources.Year2020.Day22.ToLines());
            Assert.AreEqual(32401, res);
            return res;
        }

        protected override object Part2()
        {
            // 34056 TOO HIGH
            Assert.AreEqual(291, Compute2(Resources.Year2020.Day22Test.ToLines()));
            var res = Compute2(Resources.Year2020.Day22.ToLines());
            //Assert.IsTrue(res < 34056);
            return res;
        }

        public string Compute1(params string[] input)
        {
            var (player1, player2) = Parse(input);

            while (player1.Count > 0 && player2.Count > 0)
            {
                var card1 = player1.Dequeue();
                var card2 = player2.Dequeue();
                if (card1 > card2)
                {
                    player1.Enqueue(card1);
                    player1.Enqueue(card2);
                }
                else
                {
                    player2.Enqueue(card2);
                    player2.Enqueue(card1);
                }
            }
            return ComputeWinnerScore(player1, player2).ToString();
        }

        public string Compute2(params string[] input)
        {
            var (player1, player2) = Parse(input);

            Game2(player1, player2);
            return ComputeWinnerScore(player1, player2).ToString();
        }

        private static long ComputePlayerScore(Queue<int> player)
        {
            var q = new Queue<int>(player);
            var res = 0;
            for (int i = q.Count; i > 0; i--)
            {
                res += q.Dequeue() * i;
            }
            return res;
        }

        private static long ComputeWinnerScore(Queue<int> player1, Queue<int> player2)
        {
            var winner = player1.Count > 0 ? player1 : player2;
            return ComputePlayerScore(winner);
        }

        private static void ConsoleWriteLine(string s)
        {
            //Console.WriteLine(s);
        }

        private static void Game2(Queue<int> player1, Queue<int> player2)
        {
            game++;

            //ConsoleWriteLine("=== Game " + game + " ===");
            List<string> playedRound = new List<string>();
            int round = 0;
            while (player1.Count > 0 && player2.Count > 0)
            {
                //ConsoleWriteLine("");
                round++;
                //ConsoleWriteLine($"-- Round {round} (Game {game}) --");
                var key = string.Join(".", player1) + "-" + string.Join(".", player2);
                if (playedRound.Contains(key))
                {
                    player2.Clear();
                    return;
                }

                playedRound.Add(key);
                //ConsoleWriteLine("Player 1's deck: "+string.Join(", ", player1));
                //ConsoleWriteLine("Player 2's deck: "+string.Join(", ", player2));
                var card1 = player1.Dequeue();
                var card2 = player2.Dequeue();
                //ConsoleWriteLine("Player 1 plays: " + card1);
                //ConsoleWriteLine("Player 2 plays: " + card2);

                if (player1.Count >= card1 && player2.Count > card2)
                {
                    //ConsoleWriteLine("Playing a sub-game to determine the winner...");
                    var player1bis = new Queue<int>(player1.AsEnumerable().Take(card1));
                    var player2bis = new Queue<int>(player2.AsEnumerable().Take(card2));
                    Game2(player1bis, player2bis);
                    game--;
                    //ConsoleWriteLine("");
                    //ConsoleWriteLine($"...anyway, back to game {game}.");
                    if (player1bis.Count > player2bis.Count)
                    {
                        //ConsoleWriteLine($"Player 1 wins round {round} of game {game}!");
                        player1.Enqueue(card1);
                        player1.Enqueue(card2);
                    }
                    else
                    {
                        //ConsoleWriteLine($"Player 2 wins round {round} of game {game}!");
                        player2.Enqueue(card2);
                        player2.Enqueue(card1);
                    }
                }
                else
                {
                    if (card1 > card2)
                    {
                        //ConsoleWriteLine($"Player 1 wins round {round} of game {game}!");
                        player1.Enqueue(card1);
                        player1.Enqueue(card2);
                    }
                    else
                    {
                        //ConsoleWriteLine($"Player 2 wins round {round} of game {game}!");
                        player2.Enqueue(card2);
                        player2.Enqueue(card1);
                    }
                }
                //Console.ReadKey();
            }

            int winner = player1.Count > player2.Count ? 1 : 2;
            //ConsoleWriteLine($"The winner of game {game} is player {winner}!");
        }

        private static (Queue<int> player1, Queue<int> player2) Parse(string[] input)
        {
            int i = 0;
            var deckPlayer1 = ParseDeck(input, ref i);
            i++;
            var deckPlayer2 = ParseDeck(input, ref i);

            return (deckPlayer1, deckPlayer2);
        }

        private static Queue<int> ParseDeck(string[] input, ref int i)
        {
            var deckPlayer1 = new Queue<int>();
            i++;
            for (; i < input.Length && input[i] != ""; i++)
            {
                deckPlayer1.Enqueue(int.Parse(input[i]));
            }

            return deckPlayer1;
        }
    }
}