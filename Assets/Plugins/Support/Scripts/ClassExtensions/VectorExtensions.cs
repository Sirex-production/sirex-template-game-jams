using UnityEngine;

namespace Support.Extensions
{
    /// <summary>
    /// Class that holds all extension methods for Vectors class 
    /// </summary>
    public static class VectorExtensions
    {
        /// <summary>
        /// Sets positive values to the vector
        /// </summary>
        /// <returns>Vector with positive values</returns>
        public static Vector3 Abs(this Vector3 vector3)
        {
            vector3 = new Vector3(Mathf.Abs(vector3.x), Mathf.Abs(vector3.y), Mathf.Abs(vector3.z));

            return vector3;
        }
        
        /// <summary>
        /// Sets positive values to the vector
        /// </summary>
        /// <returns>Vector with positive values</returns>
        public static Vector2 Abs(this Vector2 vector2)
        {
            vector2 = new Vector3(Mathf.Abs(vector2.x), Mathf.Abs(vector2.y));

            return vector2;
        }
        
        /// <summary>
        /// Generates random normalized vector
        /// </summary>
        /// <returns>Normalized vector with random values</returns>
        public static Vector3 RandomDirection()
        {
            return new Vector3(Random.value, Random.value, Random.value).normalized;
        }
    }
}