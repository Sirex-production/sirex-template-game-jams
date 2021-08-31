using UnityEngine;

namespace Support
{
    public class TemplateManager : MonoSingleton<TemplateManager>
    {
        [SerializeField] private TemplateSettingsData templateSettingsData;
        [Space]
        [SerializeField] private int targetFpsOnCurrentScene = 60;

        public TemplateSettingsData TemplateSettingsData => templateSettingsData;

        private void Start()
        {
            Application.targetFrameRate = targetFpsOnCurrentScene;
        }
    }
}