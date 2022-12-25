using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode.Y2020
{
    internal class Day13 : GenericDay
    {
        protected override object Part1()
        {
            Assert.AreEqual(295, Compute1(Resources.Year2020.Day13Test.ToLines()));
            var res = Compute1(Resources.Year2020.Day13.ToLines());
            Assert.AreEqual(2092, res);
            return res;
        }

        protected override object Part2()
        {
            throw new NotImplementedException();
            //var res = Compute2(Resources.Year2020.Day13.ToLines());
        }

        public string Compute1(params string[] input)
        {
            var arrival = int.Parse(input[0]);
            var services = input[1].Split(",").Where(x => x != "x").Select(x => int.Parse(x)).ToList();
            var maxService = services.Max();
            for (int i = arrival; i <= maxService + arrival; i++)
            {
                var a = services.Where(x => i % x == 0).ToList();
                if (a.Count == 1)
                {
                    return (a[0] * (i - arrival)).ToString();
                }
            }
            return "ERROR";
        }

        /*
                public string Compute2(params string[] input)
                {
                    if(!isTest)   return int.MaxValue;
                    var services = input[1].Split(",").Select((value, idx) => (value, idx)).Where(x => x.value != "x").Select(x=>new Bus(int.Parse(x.value),x.idx)).ToList();

                    while (true)
                    {
                        var maxStart = services.Select(x => x.NextStart-x.Delay).Max();
                        if (!isTest && maxStart < 100000000000000) maxStart = 100000000000000;
                        var servicesDebug = string.Join(" ; ", services.Select(x => x.ToString()));

                        foreach (var bus in services)
                        {
                            bus.NextStart+=(long)((Math.Ceiling((maxStart- (bus.NextStart- bus.Delay))/(double)bus.Id))* bus.Id);
                        }

                        var res = services.All(x => x.NextStart - x.Delay == maxStart);

                        if (res)
                        {
                            return maxStart;
                        }
                    }
                    return int.MaxValue;
                }*/

        private class Bus
        {
            public Bus(int id, int delay)
            {
                Id = id;
                Delay = delay;
                NextStart = id;
            }

            public int Delay { get; }
            public int Id { get; }
            public long NextStart { get; set; }

            public override string ToString()
            {
                return $"({Id}, {Delay}, {NextStart})";
            }
        }
    }
}