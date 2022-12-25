using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode.Y2021
{
    internal class Day03 : GenericDay
    {
        private static int CalcCo2(string[] input)
        {
            var validCO2 = input;
            int i = 0;
            while (validCO2.Length > 1)
            {
                var r = Inverse(validCO2);
                var number1 = r[i].Count(x => x == '1');
                var number0 = r[i].Count(x => x == '0');
                if (number1 >= number0)
                {
                    validCO2 = validCO2.Where(x => x[i] == '0').ToArray();
                }
                else
                {
                    validCO2 = validCO2.Where(x => x[i] == '1').ToArray();
                }
                i++;
            }
            var co2 = Convert.ToInt32(string.Join("", validCO2[0]), 2);
            return co2;
        }

        private static int CalcOxygen(string[] input)
        {
            var validOxygen = input;
            int i = 0;
            while (validOxygen.Length > 1)
            {
                var r = Inverse(validOxygen);
                var number1 = r[i].Count(x => x == '1');
                var number0 = r[i].Count(x => x == '0');
                if (number1 >= number0)
                {
                    validOxygen = validOxygen.Where(x => x[i] == '1').ToArray();
                }
                else
                {
                    validOxygen = validOxygen.Where(x => x[i] == '0').ToArray();
                }
                i++;
            }
            return Convert.ToInt32(string.Join("", validOxygen[0]), 2);
        }

        public string Compute1(params string[] input)
        {
            var r = Inverse(input);

            string gamma = "";
            string epsilon = "";
            for (int i = 0; i < r.GetLength(0); i++)
            {
                var number1 = r[i].Count(x => x == '1');
                var number0 = r[i].Count(x => x == '0');
                gamma += number1 > number0 ? "1" : "0";
                epsilon += number1 > number0 ? "0" : "1";
            }

            return (Convert.ToInt32(gamma, 2) * Convert.ToInt32(epsilon, 2)).ToString();
        }

        public string Compute2(params string[] input)
        {
            var oxygen = CalcOxygen(input);

            int co2 = CalcCo2(input);

            return (oxygen * co2).ToString();
        }

        private static char[][] Inverse(string[] input)
        {
            char[][] r = new char[input[0].Length][];

            for (int j = 0; j < input[0].Length; j++)
            {
                r[j] = new char[input.Length];
                for (int i = 0; i < input.Length; i++)
                {
                    r[j][i] = input[i][j];
                }
            }

            return r;
        }
    }
}