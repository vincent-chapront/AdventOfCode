﻿using System.Collections.Generic;

namespace AdventOfCode
{
    public static class DictionaryHelpers
    {
        public static Dictionary<T, long> AddOrIncrement<T>(this Dictionary<T, long> input, T key, long increment = 1)
        {
            if (!input.ContainsKey(key))
            {
                input.Add(key, 0);
            }
            input[key] += increment;

            return input;
        }

        public static Dictionary<T, int> AddOrIncrement<T>(this Dictionary<T, int> input, T key, int increment = 1)
        {
            if (!input.ContainsKey(key))
            {
                input.Add(key, 0);
            }
            input[key] += increment;

            return input;
        }
    }
}