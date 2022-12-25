using System;
using System.IO;

namespace Init
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            string path = @"G:\OneDrive\Documents\C#\AdventOfCode\AdventOfCode\Inputs";
            for (int year = 2022; year <= 2022; year++)
            {
                BuildYear(path, year);
            }
        }

        private static void BuildYear(string path, int year)
        {
            //var res = @"[{""year"":  " + year + @",""days"": [";
            //for (int day = 1; day <= 25; day++)
            //{
            //    res += @"{""day"": " + day + @",""file"": ""Day" + day.ToString("00") + @""",""part1"": {""tests"": [{""file"": ""Day" + day.ToString("00") + @"Test"",""result"": null}],""result"": null},""part2"": {""tests"": [{""file"": ""Day" + day.ToString("00") + @"Test"",""result"": null}],""result"": null}},";
            //}
            //res = res.Substring(0, res.Length - 1);
            //res += @"]";
            //System.IO.File.WriteAllText("Input" + year + ".json", res);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            for (int day = 09; day <= 25; day++)
            {
                string pathInput = Path.Combine(path, year.ToString(), "Day" + day.ToString("00") + ".txt");
                if (!File.Exists(pathInput))
                {
                    File.Create(pathInput);
                }
                string pathInputTest = Path.Combine(path, year.ToString(), "Day" + day.ToString("00") + "Test.txt");
                if (!File.Exists(pathInputTest))
                {
                    File.Create(pathInputTest);
                }
            }
        }
    }
}