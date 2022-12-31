using System;
using System.Linq;

namespace AdventOfCode.Y2016
{
    internal class Day05 : GenericDay
    {
        public string Compute1(string[] input, string args)
        {
            var text = input[0];
            int i = 0;
            var res = "";
            while (res.Length < 8)
            {
                var v = CreateMD5(text + i);
                if (v.StartsWith("00000"))
                {
                    res += v.Substring(5, 1);
                }
                i++;
            }
            return res;
        }

        public string Compute2(string[] input, string args)
        {
            var text = input[0];
            int i = 0;
            var res = "";
            var resA = new string[8];
            while (resA.Any(x => x == null))
            {
                var v = CreateMD5(text + i);
                if (v.StartsWith("00000"))
                {
                    res += v.Substring(5, 2) + "-";
                    var idxChar = v.Substring(5, 1)[0];
                    if ('0' <= idxChar && idxChar < '8')
                    {
                        var idx = idxChar - '0';
                        if (resA[idx] == null)
                        {
                            resA[idx] = v.Substring(6, 1);
                        }
                    }
                }
                i++;
            }
            return string.Join("", resA);
        }

        public static string CreateMD5(string input)
        {
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                return Convert.ToHexString(hashBytes);
            }
        }
    }
}