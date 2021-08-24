using UnityEngine;

namespace Support
{
    public class TemplateManager : MonoSingleton<TemplateManager>
    {
        [SerializeField] private TemplateSettingsData templateSettingsData;

        public TemplateSettingsData TemplateSettingsData => templateSettingsData;
    }
}