using System;
using UnityEngine;

namespace Support.EditorTools.HierarchyHighlighter
{
    public sealed class HierarchyHighlighterView : MonoBehaviour
    {
        public HierarchyObjectViewData viewData;

        private void Awake()
        {
#if !UNITY_EDITOR
            Destroy(this);
#endif
        }
    }
    
    [Serializable]
    public struct HierarchyObjectViewData
    {
        public Color backgroundColor;
        public Color fontColor;
        public bool isBold;
        public bool isItalic;
        public int fontSize;
    }
}