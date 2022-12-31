using System.Collections.Generic;

namespace AdventOfCode.Y2016
{
    internal class Day02 : GenericDay
    {
        //protected override object Part1()
        //{
        //    Assert.AreEqual("1985", Compute1(AdventOfCode.Resources.Year2016.Day02Test.ToLines()));
        //    var res = Compute1(Resources.Year2016.Day02.ToLines());
        //    Assert.AreEqual("44558", res);
        //    return res;
        //}

        //protected override object Part2()
        //{
        //    Assert.AreEqual("5DB3", Compute2(Resources.Year2016.Day02Test.ToLines()));
        //    var res = Compute2(Resources.Year2016.Day02.ToLines());
        //    Assert.AreEqual("6BBAD", res);
        //    return res;
        //}

        public string Compute1(string[] input, string args)
        {
            var graph = new Dictionary<(char, char), char>
            {
                {('1','R'),'2' },
                {('1','D'),'4' },

                {('2','R'),'3' },
                {('2','D'),'5' },
                {('2','L'),'1' },

                {('3','D'),'6' },
                {('3','L'),'2' },

                {('4','U'),'1' },
                {('4','R'),'5' },
                {('4','D'),'7' },

                {('5','U'),'2' },
                {('5','R'),'6' },
                {('5','D'),'8' },
                {('5','L'),'4' },

                {('6','U'),'3' },
                {('6','D'),'9' },
                {('6','L'),'5' },

                {('7','U'),'4' },
                {('7','R'),'8' },

                {('8','U'),'5' },
                {('8','R'),'9' },
                {('8','L'),'7' },

                {('9','U'),'6' },
                {('9','L'),'8' },
            };

            return GetCode(input, graph, '5');
        }

        private static string GetCode(string[] input, Dictionary<(char, char), char> graph, char startPosition)
        {
            var currentPosition = startPosition;
            var res = "";
            foreach (var line in input)
            {
                foreach (var instruction in line)
                {
                    if (graph.ContainsKey((currentPosition, instruction)))
                    {
                        currentPosition = graph[(currentPosition, instruction)];
                    }
                }
                res += currentPosition;
            }

            return res;
        }

        public string Compute2(string[] input, string args)
        {
            var graph = new Dictionary<(char, char), char>
            {
                {('1','D'),'3' },

                {('2','R'),'3' },
                {('2','D'),'6' },

                {('3','U'),'1' },
                {('3','R'),'4' },
                {('3','D'),'7' },
                {('3','L'),'2' },

                {('4','D'),'8' },
                {('4','L'),'3' },

                {('5','R'),'6' },

                {('6','U'),'2' },
                {('6','R'),'7' },
                {('6','D'),'A' },
                {('6','L'),'5' },

                {('7','U'),'3' },
                {('7','R'),'8' },
                {('7','D'),'B' },
                {('7','L'),'6' },

                {('8','U'),'4' },
                {('8','R'),'9' },
                {('8','D'),'C' },
                {('8','L'),'7' },

                {('9','L'),'8' },

                {('A','U'),'6' },
                {('A','R'),'B' },

                {('B','U'),'7' },
                {('B','R'),'C' },
                {('B','D'),'D' },
                {('B','L'),'A' },

                {('C','U'),'8' },
                {('C','L'),'B' },

                {('D','U'),'B' },
            };

            return GetCode(input, graph, '5');
        }
    }
}