using UnityEngine;

namespace Extensions
{
    /// <summary>
    /// Class that holds all extension methods for Vectors class 
    /// </summary>
    public static class VectorExtensions
    {
        public static Vector3 Abs(this Vector3 vector3)
        {
            vector3 = new Vector3(Mathf.Abs(vector3.x), Mathf.Abs(vector3.y), Mathf.Abs(vector3.z));

            return vector3;
        }
        
        public static Vector2 Abs(this Vector2 vector2)
        {
            vector2 = new Vector3(Mathf.Abs(vector2.x), Mathf.Abs(vector2.y));

            return vector2;
        }
    }
}