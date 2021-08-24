using UnityEngine;

namespace EditorExtensions
{
    public class HierarchyHighlighter : MonoBehaviour
    {
        [SerializeField] private Font fontStyle;
        [SerializeField] private Color fontColor;
        [Space]
        [SerializeField] private Color backgroundColor;

        public Font FontStyle => fontStyle;
        public Color FontColor => fontColor;
        public Color BackgroundColor => backgroundColor;
    }
}