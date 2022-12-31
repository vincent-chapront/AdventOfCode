using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Y2015
{
    internal class Day14 : GenericDay
    {
        public string Compute1(string[] input, string args)
        {
            var timeInSecond = Convert.ToInt32(args);
            var reindeers = input.Select(Parse).ToList();

            var maxDistance = int.MinValue;
            foreach (var reindeer in reindeers)
            {
                int distance = DistanceAtTime(reindeer, timeInSecond);

                maxDistance = Math.Max(maxDistance, distance);
            }

            return maxDistance.ToString();
        }

        public string Compute2(string[] input, string args)
        {
            var timeInSecond = Convert.ToInt32(args);

            var reindeers = input.Select(Parse).ToList();
            var trackings = reindeers.ToDictionary(x => x.name, _ => new ReindeerTracking());

            for (int i = 1; i <= timeInSecond; i++)
            {
                foreach (var reindeer in reindeers)
                {
                    trackings[reindeer.name].Distance = DistanceAtTime(reindeer, i);
                }
                var maxDistance = trackings.Max(x => x.Value.Distance);
                trackings.Where(x => x.Value.Distance == maxDistance).ToList().ForEach(x => x.Value.Score++);
            }
            return trackings.Max(x => x.Value.Score).ToString();
        }

        private static int DistanceAtTime((string name, int speed, int flyTime, int restTime) reindeer, int timeInSecond)
        {
            var fullFlyRestTime = timeInSecond / (reindeer.restTime + reindeer.flyTime);
            var distance = fullFlyRestTime * reindeer.speed * reindeer.flyTime;

            var remainingTime = timeInSecond % (reindeer.restTime + reindeer.flyTime);
            if (remainingTime > reindeer.flyTime)
            {
                distance += reindeer.speed * reindeer.flyTime;
            }
            else
            {
                distance += reindeer.speed * remainingTime;
            }

            return distance;
        }

        private static (string name, int speed, int flyTime, int restTime) Parse(string line)
        {
            var regex = new Regex(@"([a-zA-Z]+) can fly (\d+) km/s for (\d+) seconds, but then must rest for (\d+) seconds.");

            var match = regex.Match(line);
            var name = match.Groups[1].Value;
            var speed = int.Parse(match.Groups[2].Value);
            var flyTime = int.Parse(match.Groups[3].Value);
            var restTime = int.Parse(match.Groups[4].Value);

            return (name, speed, flyTime, restTime);
        }

        private class ReindeerTracking
        {
            public int Distance;
            public int Score;

            public override string ToString()
            {
                return Distance + " - " + Score;
            }
        }
    }
}