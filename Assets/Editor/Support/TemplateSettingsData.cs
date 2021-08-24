using UnityEngine;

namespace Support
{
    public class TemplateSettingsData : ScriptableObject
    {
        [Tooltip("How many pixel on the screen has to be passed in one frame to invoke swipe event")]
        [SerializeField] private float minimumDeltaSwipe = 2f;

        public float MinimumDeltaSwipe => minimumDeltaSwipe;
    }
}