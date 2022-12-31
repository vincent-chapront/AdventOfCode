using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace AdventOfCode.Y2017
{
    internal class Day01 : GenericDay
    {
        //protected override object Part1()
        //{
        //    Assert.AreEqual(3, Compute1("1122"));
        //    Assert.AreEqual(4, Compute1("1111"));
        //    Assert.AreEqual(0, Compute1("1234"));
        //    Assert.AreEqual(9, Compute1("91212129"));
        //    var res = Compute1(Resources.Year2017File.Day01);
        //    Assert.AreEqual(1393, res);
        //    return res;
        //}

        //protected override object Part2()
        //{
        //    Assert.AreEqual(6, Compute2("1212"));
        //    Assert.AreEqual(0, Compute2("1221"));
        //    Assert.AreEqual(4, Compute2("123425"));
        //    Assert.AreEqual(12, Compute2("123123"));
        //    Assert.AreEqual(4, Compute2("12131415"));
        //    var res = Compute2(Resources.Year2017File.Day01);
        //    Assert.AreEqual(1292, res);
        //    return res;
        //}

        public string Compute1(string[] input, string args)
        {
            var text = input.First();
            long res = 0;
            for (int i = 0; i < text.Length; i++)
            {
                var current = text[i];
                var nextIndex = (i + 1) % text.Length;
                char next = text[nextIndex];
                if (current == next)
                {
                    res += (long)(current - '0');
                }
            }
            return res.ToString();
        }

        public string Compute2(string[] input, string args)
        {
            var text = input.First();
            long res = 0;
            for (int i = 0; i < text.Length; i++)
            {
                var current = text[i];
                var nextIndex = (i + text.Length / 2) % text.Length;
                char next = text[nextIndex];
                if (current == next)
                {
                    res += (long)(current - '0');
                }
            }
            return res.ToString();
        }
    }
}