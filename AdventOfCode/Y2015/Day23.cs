namespace AdventOfCode.Y2015
{
    internal class Day23 : GenericDay
    {
        public string Compute1(string[] input, string args)
        {
            var (_, b) = Compute1(input);
            return b.ToString();
        }

        public string Compute2(string[] input, string args)
        {
            var (_, b) = Compute1(input, 1);
            return b.ToString();
        }

        private static (long a, long b) Compute1(string[] input, long startA = 0, long startB = 0)
        {
            var a = startA;
            var b = startB;
            var currentLine = 0;
            while (currentLine < input.Length)
            {
                var line = input[currentLine];
                if (line.StartsWith("hlf"))
                {
                    if (line.EndsWith("a"))
                    {
                        a /= 2;
                    }
                    else
                    {
                        b /= 2;
                    }
                    currentLine++;
                }
                else if (line.StartsWith("tpl"))
                {
                    if (line.EndsWith("a"))
                    {
                        a *= 3;
                    }
                    else
                    {
                        b *= 3;
                    }
                    currentLine++;
                }
                else if (line.StartsWith("inc"))
                {
                    if (line.EndsWith("a"))
                    {
                        a++;
                    }
                    else
                    {
                        b++;
                    }
                    currentLine++;
                }
                else if (line.StartsWith("jmp"))
                {
                    var value = int.Parse(line.Split(" ")[1].Replace("+", ""));
                    currentLine += value;
                }
                else if (line.StartsWith("jio"))
                {
                    var val = (line.Contains("a,")) ? a : b;
                    var value = int.Parse(line.Split(" ")[2].Replace("+", ""));
                    currentLine += val == 1 ? value : 1;
                }
                else if (line.StartsWith("jie"))
                {
                    var val = (line.Contains("a,")) ? a : b;
                    var value = int.Parse(line.Split(" ")[2].Replace("+", ""));
                    currentLine += val % 2 == 0 ? value : 1;
                }
            }

            return (a, b);
        }
    }
}