using System;
using System.Collections.Generic;

namespace RandomWordDisplay.API.Extensions
{
    public static class ListExtensions
    {
        private static readonly Random _random = new();

        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = _random.Next(n + 1);
                T temp = list[k];
                list[k] = list[n];
                list[n] = temp;
            }
        }
    }
}