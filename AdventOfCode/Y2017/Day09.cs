namespace AdventOfCode.Y2017
{
    internal class Day09 : GenericDay
    {
        public string Compute1(string[] input, string args)
        {
            return Compute(input[0]).numberOfGroups.ToString();
        }

        public string Compute2(string[] input, string args)
        {
            return Compute(input[0]).sizeGarbage.ToString();
        }

        private static (int numberOfGroups, int sizeGarbage) Compute(string input)
        {
            int i = 0;
            var res = ReadGroup(input, ref i, 1);
            return res;
        }

        private static (int numberOfGroups, int sizeGarbage) ReadGroup(string input, ref int i, int level)
        {
            var isReadingGarbage = false;
            var score = 0;
            var sizeGarbage = 0;
            while (i < input.Length)
            {
                var c = input[i];
                i++;
                if (c == '!')
                {
                    i++;
                    continue;
                }
                else if (isReadingGarbage)
                {
                    if (c == '>')
                    {
                        isReadingGarbage = false;
                    }
                    else
                    {
                        sizeGarbage++;
                    }
                }
                else
                {
                    if (c == '<')
                    {
                        isReadingGarbage = true;
                    }
                    else if (c == '{')
                    {
                        score += level;
                        var group = ReadGroup(input, ref i, level + 1);
                        score += group.numberOfGroups;
                        sizeGarbage += group.sizeGarbage;
                    }
                    else if (c == '}')
                    {
                        return (score, sizeGarbage);
                    }
                }
            }
            return (score, sizeGarbage);
        }

        private static int ReadGarbageSize(string input, ref int i, int level)
        {
            var isReadingGarbage = false;
            var score = 0;
            while (i < input.Length)
            {
                var c = input[i];
                i++;
                if (c == '!')
                {
                    i++;
                }
                else if (isReadingGarbage)
                {
                    if (c == '>')
                    {
                        isReadingGarbage = false;
                    }
                }
                else
                {
                    if (c == '<')
                    {
                        isReadingGarbage = true;
                    }
                    else if (c == '{')
                    {
                        score += level;
                        score += ReadGarbageSize(input, ref i, level + 1);
                    }
                    else if (c == '}')
                    {
                        return score;
                    }
                }
            }
            return score;
        }
    }
}