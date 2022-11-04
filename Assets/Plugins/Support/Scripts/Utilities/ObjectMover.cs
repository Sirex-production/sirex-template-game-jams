using NaughtyAttributes;
using UnityEngine;

namespace Support.Utilities
{
    /// <summary>
    /// Class for rotating objects
    /// </summary>
    public class ObjectMover : MonoBehaviour
    {
        [BoxGroup("Movement")]
        [Min(0)] public float sinSpeed = 1f;
        [BoxGroup("Movement")]
        public Vector3 sinMovementOffset;
        [BoxGroup("Rotation")]
        public Vector3 rotationDirectionalSpeed;

        private void Update()
        {
            var localRotation = transform.localRotation;
            var targetRotation = Quaternion.Euler(rotationDirectionalSpeed) * localRotation;
               
            localRotation = Quaternion.Lerp(localRotation, targetRotation, Time.deltaTime);
            transform.localRotation = localRotation;

            transform.localPosition += sinMovementOffset * (Mathf.Sin(Time.time * sinSpeed) * Time.deltaTime);
        }
    }
}