using NaughtyAttributes;
using UnityEngine;

namespace EditorExtensions
{
    /// <summary>
    /// Class that represents data of highlighted object in hierarchy
    /// </summary>
    public class HierarchyHighlighter : MonoBehaviour
    {
        [SerializeField] private bool isDisplayed = true;
        [Space(10)]
        [ShowIf("isDisplayed"), Foldout("Appearance")]
        [SerializeField] [Range(0, 15)] private int fontSize = 10;
        [ShowIf("isDisplayed"), Foldout("Appearance")]
        [SerializeField] private FontStyle fontStyle = FontStyle.Normal;
        [ShowIf("isDisplayed"), Foldout("Appearance")]
        [SerializeField] private Color fontColor = Color.black;
        [Space]
        [ShowIf("isDisplayed"), Foldout("Appearance")]
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