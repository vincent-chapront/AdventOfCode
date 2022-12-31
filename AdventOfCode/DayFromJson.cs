using System;

namespace AdventOfCode
{
    public class DayFromJson
    {
        private readonly GenericDay instance;
        private readonly int year;
        private readonly InputFile_Day day;

        public DayFromJson(int year, Type type, InputFile_Day day)
        {
            instance = (GenericDay)Activator.CreateInstance(type);
            this.year = year;
            this.day = day;
        }

        private string Indent(int i = 0)
        {
            return new string(' ', i * 2);
        }

        public void Run()
        {
            Console.WriteLine($"{year} Day {day.Day}");
            Console.WriteLine($"{Indent(1)}Part 1");
            RunPart(day.File, day.Part1, instance.Compute1);
            Console.WriteLine();
            Console.WriteLine($"{Indent(1)}Part 2");
            RunPart(day.File, day.Part2, instance.Compute2);
            Console.WriteLine();
            Console.WriteLine();
        }

        private void RunPart(string file, InputFile_Part part, Func<string[], string, string> compute)
        {
            if (part.Tests != null && part.Tests.Count > 0)
            {
                Console.WriteLine($"{Indent(2)}Test");
                for (int i = 0; i < part.Tests.Count; i++)
                {
                    InputFile_Test test = part.Tests[i];
                    Console.Write($"{Indent(3)}{i + 1} : ");
                    string[] testInput;
                    if (test.File != null)
                    {
                        testInput = System.IO.File.ReadAllLines("Inputs/" + year + "/" + test.File + ".txt");
                    }
                    else
                    {
                        testInput = new string[] { test.Text };
                    }

                    try
                    {
                        var res = compute(testInput, test.Args);
                        if (res == test.Result)
                        {
                            WriteSuccess($"{res}");
                        }
                        else
                        {
                            WriteError($"{res}, expected :{test.Result}");
                        }
                    }
                    catch (NotImplementedException ex)
                    {
                        WriteError("NOT IMPLEMENTED");
                    }
                }
            }
            Console.Write($"{Indent(2)}Result: ");
            try
            {
                var result = compute(System.IO.File.ReadAllLines("Inputs/" + year + "/" + file + ".txt"), part.Args);
                if (string.IsNullOrWhiteSpace(part.Result))
                {
                    WriteWarning($"{result}");
                }
                else if (result == part.Result)
                {
                    WriteSuccess($"{result}");
                }
                else
                {
                    WriteError($"{result}, expected :{part.Result}");
                }
            }
            catch (NotImplementedException ex)
            {
                WriteError($"NOT IMPLEMENTED");
            }
        }

        private static void Write(string msg, ConsoleColor f, ConsoleColor b)
        {
            Console.ForegroundColor = f;
            Console.BackgroundColor = b;
            Console.WriteLine(msg);
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
        }

        private static void WriteError(string msg) => Write(msg, ConsoleColor.Red, ConsoleColor.Black);

        private static void WriteWarning(string msg) => Write(msg, ConsoleColor.Yellow, ConsoleColor.Black);

        private static void WriteSuccess(string msg) => Write(msg, ConsoleColor.Green, ConsoleColor.Black);
    }
}