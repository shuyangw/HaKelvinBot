using System;
using System.Collections.Generic;
using System.Text;

namespace HaKelvinBot.Util
{
    public class MiscUtil
    {
        /// <summary>
        /// Computes the Levenshtein edit distance of two strings:
        /// https://en.wikipedia.org/wiki/Levenshtein_distance
        /// </summary>
        /// <param name="a">The first string of the comparision.</param>
        /// <param name="b">The second string of the comparision.</param>
        /// <returns>The distance that was computed</returns>
        public static int LevenshteinDistance(string a, string b)
        {
            if (string.IsNullOrEmpty(a) && string.IsNullOrEmpty(b))
                return 0;

            if (string.IsNullOrEmpty(a))
                return b.Length;

            if (string.IsNullOrEmpty(b))
                return a.Length;

            int lengthA = a.Length;
            int lengthB = b.Length;
            var distances = new int[lengthA + 1, lengthB + 1];
            for (int i = 0; i <= lengthA; distances[i, 0] = i++) ;
            for (int j = 0; j <= lengthB; distances[0, j] = j++) ;

            for (int i = 1; i <= lengthA; i++)
            {
                for (int j = 1; j <= lengthB; j++)
                {
                    int cost = b[j - 1] == a[i - 1] ? 0 : 1;
                    distances[i, j] = Math.Min(
                        Math.Min(distances[i - 1, j] + 1, distances[i, j - 1] + 1),
                        distances[i - 1, j - 1] + cost
                    );
                }
            }

            return distances[lengthA, lengthB];
        }
    }
}
