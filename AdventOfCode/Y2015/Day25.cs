using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode.Y2015
{
    internal class Day25 : GenericDay
    {
        protected override object Part1()
        {
            Assert.AreEqual(20151125, Compute(1, 1));
            Assert.AreEqual(27995004, Compute(6, 6));
            var res = Compute(2978, 3083);
            Assert.AreEqual(2650453, res);

            return res;
        }

        public string Compute1(params string[] input)
        {
            throw new NotImplementedException();
        }

        public string Compute2(params string[] input)
        {
            throw new NotImplementedException();
        }

        private static long Compute(int targetR, int targetC)
        {
            long res = 20151125;
            int r = 1;
            int c = 1;
            while (r != targetR || c != targetC)
            {
                if (r > 1)
                {
                    c++;
                    r--;
                }
                else
                {
                    r = c + 1;
                    c = 1;
                }
                res = (res * 252533) % 33554393;
            }

            return res;
        }
    }
}