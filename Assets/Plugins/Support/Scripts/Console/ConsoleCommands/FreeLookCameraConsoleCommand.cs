using UnityEngine;

namespace Support.Console
{
    /// <summary>
    /// Console command that activates or deactivate free camera on main camera
    /// </summary>
    public class FreeLookCameraConsoleCommand : IConsoleCommand
    {
        public string Name => "noclip";
        public string Description => "Adds FreeLookCamera component to the main camera and allows you to fly freely";

        public string Execute(string[] args = null)
        {
            if (Camera.main == null)
                return "There is no main camera in the scene";

            if (Camera.main.gameObject.TryGetComponent(out FreeLookCamera _))
            {
                TurnOffFreeCamera();
                return "Free look camera was deactivated";
            }
            
            TurnOnFreeCamera();
            
            return "Free look camera was activated";
        }

        private void TurnOnFreeCamera()
        {
            Camera.main.gameObject.AddComponent<FreeLookCamera>();
        }

        private void TurnOffFreeCamera()
        {
            MonoBehaviour.Destroy(Camera.main.gameObject.GetComponent<FreeLookCamera>());
        }
    }
}