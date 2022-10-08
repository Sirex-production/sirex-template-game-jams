using UnityEngine;

namespace Support
{
    /// <summary>
    /// Class for rotating objects
    /// </summary>
    public class ObjectRotator : MonoBehaviour
    {
        public Vector3 rotationDirectionalSpeed;

        private void FixedUpdate()
        {
            var localRotation = transform.localRotation;
            var targetRotation = Quaternion.Euler(rotationDirectionalSpeed) * localRotation;
               
            localRotation = Quaternion.Lerp(localRotation, targetRotation, Time.fixedDeltaTime);
            transform.localRotation = localRotation;
        }
    }
}