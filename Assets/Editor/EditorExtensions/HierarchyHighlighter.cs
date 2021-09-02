using UnityEngine;

namespace EditorExtensions
{
    public class HierarchyHighlighter : MonoBehaviour
    {
        [SerializeField] private bool isDisplayed = true;
        [Space(10)]
        [SerializeField] [Range(0, 15)] private int fontSize = 10;
        [SerializeField] private FontStyle fontStyle = FontStyle.Normal;
        [SerializeField] private Color fontColor = Color.black;
        [Space]
        [SerializeField] private Color backgroundColor = Color.white;

        public bool IsDisplayed => isDisplayed;
        public int FontSize => fontSize;
        public FontStyle FontStyle => fontStyle;
        public Color FontColor => fontColor;
        public Color BackgroundColor => backgroundColor;

        private void OnValidate()
        {
            fontColor.a = 255;
            backgroundColor.a = 255;
        }
    }
}