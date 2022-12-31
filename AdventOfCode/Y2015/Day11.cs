using System.Text;

namespace AdventOfCode.Y2015
{
    internal class Day11 : GenericDay
    {
        public string Compute1(string[] input, string args)
        {
            var res = NextValid(input[0]);
            return res;
        }

        public string Compute2(string[] input, string args)
        {
            var res = NextValid(input[0]);
            res = NextValid(res);
            return res;
        }

        private static bool IsValid(string s)
        {
            var hasSequence = false;
            var numberOfPair = 0;
            var lastPair = -1;
            for (int i = 0; i < s.Length; i++)
            {
                var c = s[i];
                if (c == 'i' || c == 'o' || c == 'l')
                {
                    return false;
                }
                if (i < s.Length - 1)
                {
                    if (c == s[i + 1])
                    {
                        if (lastPair != i - 1)
                        {
                            numberOfPair++;
                            lastPair = i;
                        }
                    }
                }

                if (i < s.Length - 2)
                {
                    if (c == s[i + 1] - 1 && s[i + 1] == s[i + 2] - 1)
                    {
                        hasSequence = true;
                    }
                }
            }

            return hasSequence && numberOfPair >= 2;
        }

        private static string Next(string s)
        {
            if (s[^1] == 'z')
            {
                var retenu = 1;
                var sb = new StringBuilder();
                sb.Insert(0, 'a');
                for (int i = s.Length - 2; i >= 0; i--)
                {
                    char c;
                    if (s[i] == 'z' && retenu == 1)
                    {
                        c = 'a';
                        retenu = 1;
                    }
                    else
                    {
                        c = (char)(s[i] + retenu);
                        retenu = 0;
                    }
                    sb.Insert(0, c);
                }
                return sb.ToString();
            }
            else
            {
                return s[..^1] + (char)(s[^1] + 1);
            }
        }

        private static string NextValid(string s)
        {
            int i = 0;
            var a = s[..5];
            do
            {
                i++;
                var b = Next(s);
                if (b[..5] != a)
                {
                    a = s[..5];
                }
                s = b;
            } while (!IsValid(s));
            return s;
        }
    }
}