using System.Collections.Generic;

namespace AdventOfCode
{
    public class InputFile_Year
    {
        public int Year { get; set; }
        public List<InputFile_Day> Days { get; set; }
    }
    public class InputFile_Day
    {
        public int Day { get; set; }
        public InputFile_Part Part1 { get; set; }
        public InputFile_Part Part2 { get; set; }
        public string File { get; set; }
    }
    public  class InputFile_Part
    {
        public List<InputFile_Test> Tests { get; set; }
        public string Result { get; set; }
        public string Args { get; set; }
    }

    public class InputFile_Test
    {
        public string File { get; set; }
        public string Args { get; set; }
        public string Text { get; set; }
        public string Result { get; set; }
    }
}