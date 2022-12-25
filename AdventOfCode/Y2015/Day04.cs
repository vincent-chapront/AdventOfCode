using System.Security.Cryptography;
using System.Text;

namespace AdventOfCode.Y2015
{
    internal class Day04 : GenericDay
    {
        public string Compute1(params string[] input)
        {
            return Compute(input[0], 5);
        }

        public string Compute2(params string[] input)
        {
            return Compute(input[0], 6);
        }

        private static bool CheckMD5(MD5 md5, string input, int leadingZeroes)
        {
            byte[] inputBytes = Encoding.ASCII.GetBytes(input);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            for (int i = 0; i < leadingZeroes / 2; i++)
            {
                if (hashBytes[i] != 0)
                {
                    return false;
                }
            }
            if (leadingZeroes % 2 != 0)
            {
                if (hashBytes[leadingZeroes / 2] > 0x0F)
                {
                    return false;
                }
            }
            return true;
        }

        private static string Compute(string input, int leadingZeroes)
        {
            using (MD5 md5 = MD5.Create())
            {
                long res = 0;
                while (res < long.MaxValue - 1)
                {
                    res++;

                    var md5Res = CheckMD5(md5, input + res, leadingZeroes);
                    if (md5Res)
                    {
                        return res.ToString();
                    }
                }
                return "ERROR";
            }
        }
    }
}