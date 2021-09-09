using UnityEngine;

namespace Support.Console
{
    public class FreeLookCameraConsoleCommand : IConsoleCommand
    {
        public string CommandName => "camera_free";

        public string Execute()
        {
            TurnOnFreeCamera();

            return "Camera was activated";
        }

        private void TurnOnFreeCamera()
        {
            Camera.main.gameObject.AddComponent<FreeLookCamera>();
        }
    }
}