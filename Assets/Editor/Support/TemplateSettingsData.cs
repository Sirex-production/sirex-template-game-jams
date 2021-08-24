using UnityEngine;

namespace Support
{
    [CreateAssetMenu(menuName = "Data/Template project/Template Settings", fileName = "NewTemplateSettingsData")]
    public class TemplateSettingsData : ScriptableObject
    {
        [Header("Input settings setting")]
        [Tooltip("How many pixel on the screen has to be passed in one frame to invoke swipe event")]
        [SerializeField] private float minimumDeltaSwipe = 2f;

        public float MinimumDeltaSwipe => minimumDeltaSwipe;
    }
}