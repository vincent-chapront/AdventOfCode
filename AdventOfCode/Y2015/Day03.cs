using System.Collections.Generic;

namespace AdventOfCode.Y2015
{
    internal class Day03 : GenericDay
    {
        public string Compute1(string[] input, string args)
        {
            var text = input[0];
            var visitedHouses = new List<string>();
            var currentLocation = (0, 0);
            visitedHouses.Add($"{currentLocation.Item1},{currentLocation.Item2}");
            foreach (var c in text)
            {
                if (c == '<') currentLocation = (currentLocation.Item1, currentLocation.Item2 - 1);
                if (c == '>') currentLocation = (currentLocation.Item1, currentLocation.Item2 + 1);
                if (c == '^') currentLocation = (currentLocation.Item1 - 1, currentLocation.Item2);
                if (c == 'v') currentLocation = (currentLocation.Item1 + 1, currentLocation.Item2);
                var key = $"{currentLocation.Item1},{currentLocation.Item2}";
                if (!visitedHouses.Contains(key))
                {
                    visitedHouses.Add(key);
                }
            }
            return visitedHouses.Count.ToString();
        }

        public string Compute2(string[] input, string args)
        {
            var text = input[0];
            var visitedHouses = new List<string>();
            var currentLocations = new List<(int, int)>
            {
                (0,0),
                (0,0)
            };
            visitedHouses.Add($"{currentLocations[0].Item1},{currentLocations[0].Item2}");
            for (int i = 0; i < text.Length; i++)
            {
                var c = text[i];
                string key;
                var indexCurrentLocation = i % 2;

                if (c == '<') currentLocations[indexCurrentLocation] = (currentLocations[indexCurrentLocation].Item1, currentLocations[indexCurrentLocation].Item2 - 1);
                if (c == '>') currentLocations[indexCurrentLocation] = (currentLocations[indexCurrentLocation].Item1, currentLocations[indexCurrentLocation].Item2 + 1);
                if (c == '^') currentLocations[indexCurrentLocation] = (currentLocations[indexCurrentLocation].Item1 - 1, currentLocations[indexCurrentLocation].Item2);
                if (c == 'v') currentLocations[indexCurrentLocation] = (currentLocations[indexCurrentLocation].Item1 + 1, currentLocations[indexCurrentLocation].Item2);
                key = $"{currentLocations[indexCurrentLocation].Item1},{currentLocations[indexCurrentLocation].Item2}";

                if (!visitedHouses.Contains(key))
                {
                    visitedHouses.Add(key);
                }
            }
            return visitedHouses.Count.ToString();
        }
    }
}