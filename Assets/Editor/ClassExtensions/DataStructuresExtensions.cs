using System;
using System.Collections.Generic;

namespace Extensions
{
    public static class DataStructuresExtensions
    {
        /// <summary>
        /// Same as First() method from standard library but does not throws exception when element is not found 
        /// </summary>
        /// <param name="source">Collection where element will be found</param>
        /// <param name="predicate">Finding condition</param>
        /// <returns>Returns found element. If element was not found, returns default value of the type in the collection</returns>
        /// <exception cref="NullReferenceException">Exception is thrown when method is invoked on null</exception>
        public static T SafeFirst<T>(this IEnumerable<T> source, Predicate<T> predicate)
        {
            if (source == null)
                throw new NullReferenceException();
            
            foreach (var element in source)
            {
                if (predicate(element))
                    return element;
            }
            
            return default;
        }
    }
}