using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode.Y2016
{
    internal class Day04 : GenericDay
    {
        public string Compute1(string[] input, string args)
        {
            return input.Select(Parse).Where(x => IsValid(x)).Sum(x => x.id).ToString();
        }

        public string Compute2(string[] input, string args)
        {
            return input.Select(Parse).Where(x => IsValid(x)).First(x => Decrypt(x) == "northpole-object-storage").id.ToString();
        }

        private static string Decrypt((string name, int id, string checksum) p)
        {
            var res = "";
            foreach (var c in p.name)
            {
                var a = (int)c;
                if (c >= 'a' && c <= 'z')
                {
                    a = (((c - 'a') + p.id) % 26) + 'a';
                }
                res += (char)a;
            }
            return res;
        }

        private static bool IsValid((string name, int id, string checksum) p)
        {
            var checksum =
                p.name.Replace("-", "")
                .GroupBy(x => x)
                .ToDictionary(x => x.Key, x => x.ToList().Count)
                .Select(x => (x.Key, x.Value))
                .OrderByDescending(x => x.Value)
                .ThenBy(x => x.Key)
                .Select(x => x.Key)
                .Take(5)

                .Join("");

            return checksum == p.checksum;
        }

        private static bool IsValid(string v)
        {
            var p = Parse(v);
            return IsValid(p);
        }

        private static (string name, int id, string checksum) Parse(string s)
        {
            var regex = new Regex(@"([a-z\-]+)-(\d+)\[(\w*)\]");
            var r = regex.Match(s).Groups;

            return (r[1].Value, int.Parse(r[2].Value), r[3].Value);
        }
    }
}