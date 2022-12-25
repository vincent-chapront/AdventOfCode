using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AdventOfCode.Y2015
{
    internal class Day20 : GenericDay
    {
        public string Compute1(params string[] input)
        {
            throw new NotImplementedException();
            Assert.AreEqual(10, GetNumberOfGiftPerHouse(1));
            Assert.AreEqual(30, GetNumberOfGiftPerHouse(2));
            Assert.AreEqual(40, GetNumberOfGiftPerHouse(3));
            Assert.AreEqual(70, GetNumberOfGiftPerHouse(4));
            Assert.AreEqual(60, GetNumberOfGiftPerHouse(5));
            Assert.AreEqual(120, GetNumberOfGiftPerHouse(6));
            Assert.AreEqual(80, GetNumberOfGiftPerHouse(7));
            Assert.AreEqual(150, GetNumberOfGiftPerHouse(8));
            Assert.AreEqual(130, GetNumberOfGiftPerHouse(9));
            var res = Compute1("29000000");
            Assert.AreEqual(-1, res);

            for (int a = 0; a < 120; a++)
            {
                Console.WriteLine(a + " : " + GetNumberOfGiftPerHouse(a));
            }

            int houseNumber = 1;
            while (GetNumberOfGiftPerHouse(houseNumber) < i)
            {
                houseNumber++;
                ;
            }
            return houseNumber.ToString();
        }

        private static int GetNumberOfGiftPerHouse(int houseNumber)
        {
            var factors = Factors(houseNumber);

            return factors.Sum(x => x * 10);
        }

        public string Compute2(params string[] input)
        {
            throw new NotImplementedException();
        }

        public static List<int> Factors(int me)
        {
            return Enumerable.Range(1, me).Where(x => me % x == 0).ToList();
        }
    }
}