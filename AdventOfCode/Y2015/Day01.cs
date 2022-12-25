using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode.Y2015
{
    internal class Day01 : GenericDay
    {
        public string Compute1(params string[] input)
        {
            string text = input[0];
            var currentFloor = 0;
            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] == '(')
                {
                    currentFloor++;
                }
                else if (text[i] == ')')
                {
                    currentFloor--;
                }
            }
            return currentFloor.ToString();
        }

        public string Compute2(params string[] input)
        {
            string text = input[0];
            var currentFloor = 0;
            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] == '(')
                {
                    currentFloor++;
                }
                else if (text[i] == ')')
                {
                    currentFloor--;
                    if (currentFloor == -1)
                    {
                        return (i + 1).ToString();
                    }
                }
            }
            return "ERROR";
        }
    }
}