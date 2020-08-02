using HaKelvinBot.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExtensionMethods
{
    /// <summary>
    /// This class provides some extension methods for the C# String class that is needed for some cases.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Overload for MostLike that takes in a string array instead of an ICollection
        /// </summary>
        /// <param name="str">The string that is to be compared.</param>
        /// <param name="stringList">The list of strings that we would like to compare against.</param>
        /// <returns>The string that is most like the input string.</returns>
        public static string MostLike(this string str, ICollection<string> stringList)
        {
            int minDistance = int.MaxValue;
            string minString = "";

            //Pretty expensive, seek alternative
            foreach(string otherString in stringList)
            {
                int currDist = Math.Min(minDistance, MiscUtil.LevenshteinDistance(str, otherString));
                minDistance = currDist;
                minString = otherString;
            }

            return minString;
        }

        /// <summary>
        /// Overload for MostLike that takes in a string array instead of an ICollection
        /// </summary>
        /// <param name="str">The string that is to be compared.</param>
        /// <param name="stringList">The list of strings that we would like to compare against.</param>
        /// <returns>The string that is most like the input string.</returns>
        public static string MostLike(this string str, string[] stringList)
        {
            return MostLike(str, stringList.ToList<string>());
        }
    }
}
