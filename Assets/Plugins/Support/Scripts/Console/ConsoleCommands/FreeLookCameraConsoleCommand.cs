using UnityEngine;

namespace Support.Console
{
    /// <summary>
    /// Console command that activates free camera on main camera
    /// </summary>
    public class FreeLookCameraConsoleCommand : IConsoleCommand
    {
        public string CommandName => "noclip";

        public string Execute(string[] args = null)
        {
            TurnOnFreeCamera();

            return "Free look camera was activated";
        }

        private void TurnOnFreeCamera()
        {
            Camera.main.gameObject.AddComponent<FreeLookCamera>();
        }
    }
}