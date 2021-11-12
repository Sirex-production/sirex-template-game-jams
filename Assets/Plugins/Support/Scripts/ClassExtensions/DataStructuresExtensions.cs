using System;
using System.Collections.Generic;

namespace Extensions
{
    /// <summary>
    /// Class that holds all extension methods for DataStructures 
    /// </summary>
    public static class DataStructuresExtensions
    {
        /// <summary>
        /// Same as First() method from the standard library but does not throw exception when an element is not found
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