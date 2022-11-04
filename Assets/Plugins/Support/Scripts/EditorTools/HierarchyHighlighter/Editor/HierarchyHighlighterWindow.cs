using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Support.EditorTools.HierarchyHighlighter
{
    public sealed class HierarchyHighlighterWindow : EditorWindow
    {
        [SerializeField] private VisualTreeAsset hierarchyHighlighterVisualAsset;

        private const float BACKGROUND_DRAWING_OFFSET = 16f;
        private const float RIGHT_SPACE = 15f;
        
        private static readonly HierarchyObjectViewData NULL_VIEW_DATA = new()
        {
            backgroundColor = Color.red,
            fontColor = Color.black,
            isBold = true,
            isItalic = false,
            fontSize = 12
        };
        
        private static HashSet<int> _instanceIdsWithNullFields = new();

        private ColorField _backgroundColorField;
        private ColorField _fontColorField;
        private Toggle _boldToggle;
        private Toggle _italicToggle;
        private SliderInt _fontSizeSlider;
        private Button _applyButton;
        private Button _resetButton;
        private Button _resetAllButton;
        private Button _findNullsButton;
        private Button _resetNullsButton;
        
        static HierarchyHighlighterWindow()
        {
            EditorApplication.hierarchyWindowItemOnGUI += DrawHierarchy;
        }

        [MenuItem("Tools/Hierarchy highlighter")]
        public static void ShowEditorWindow()
        {
            var hierarchyHighlighterWindow = GetWindow<HierarchyHighlighterWindow>();
            hierarchyHighlighterWindow.titleContent = new GUIContent("Hierarchy highlighter");
        }

        private static void DrawHierarchy(int instanceId, Rect hierarchyRect)
        {
            var selectedObject = EditorUtility.InstanceIDToObject(instanceId) as GameObject;
            
            if (selectedObject == null)
                return;
            
            bool isMarkedWithCustomStyle = selectedObject.TryGetComponent(out HierarchyHighlighterView hierarchyHighlighterView);
            bool isMarkedAsNull = _instanceIdsWithNullFields.Contains(instanceId);

            if(!isMarkedWithCustomStyle && !isMarkedAsNull)
                return;

            var viewData = isMarkedWithCustomStyle ? hierarchyHighlighterView.viewData : NULL_VIEW_DATA;
            var fontStyleInHierarchy = FontStyle.Normal;

            if (viewData.isBold)
                fontStyleInHierarchy = FontStyle.Bold;
            else if (viewData.isItalic)
                fontStyleInHierarchy = FontStyle.Italic;
            else
                fontStyleInHierarchy = FontStyle.BoldAndItalic;

            hierarchyRect.center += Vector2.right * BACKGROUND_DRAWING_OFFSET;
            hierarchyRect.width -= RIGHT_SPACE;

            if (viewData.backgroundColor.a > 0f)
                EditorGUI.DrawRect(hierarchyRect, viewData.backgroundColor);

            if (viewData.fontColor.a > 0f)
            {
                var normal = new GUIStyle(EditorStyles.textField).normal;
                normal.textColor = viewData.fontColor;

                EditorGUI.LabelField(hierarchyRect, selectedObject.name, new GUIStyle
                {
                    fontSize = viewData.fontSize,
                    fontStyle = fontStyleInHierarchy,
                    normal = normal
                });
            }
        }

        private void CreateGUI()
        {
            InitializeFields();
            ProcessFields();
        }

        private void InitializeFields()
        {
            hierarchyHighlighterVisualAsset.CloneTree(rootVisualElement);

            _backgroundColorField = rootVisualElement.Q<ColorField>("BackgroundColor");
            _fontColorField = rootVisualElement.Q<ColorField>("FontColor");
            _boldToggle = rootVisualElement.Q<Toggle>("Bold");
            _italicToggle = rootVisualElement.Q<Toggle>("Italic");
            _fontSizeSlider = rootVisualElement.Q<SliderInt>("FontSize");
            _applyButton = rootVisualElement.Q<Button>("Apply");
            _resetButton = rootVisualElement.Q<Button>("Reset");
            _resetAllButton = rootVisualElement.Q<Button>("ResetAll");
            _findNullsButton = rootVisualElement.Q<Button>("FindNulls");
            _resetNullsButton = rootVisualElement.Q<Button>("ResetNulls");
        }

        private void ProcessFields()
        {
            _applyButton.clicked += OnApplyButtonClicked;
            _resetButton.clicked += OnResetButtonClicked;
            _findNullsButton.clicked += OnFindNullsButtonClicked;
            _resetNullsButton.clicked += OnResetNullsButtonClicked;
            _resetAllButton.clicked += OnResetAllButtonClicked;
        }

        private void OnApplyButtonClicked()
        {
            var selectedGameObjects = Selection.gameObjects;
            
            if(selectedGameObjects == null || selectedGameObjects.Length < 1)
                return;

            var viewData = new HierarchyObjectViewData
            {
                backgroundColor = _backgroundColorField.value,
                fontColor = _fontColorField.value,
                isBold = _boldToggle.value,
                isItalic = _italicToggle.value,
                fontSize = _fontSizeSlider.value
            };

            foreach (var go in selectedGameObjects)
            {
                if(go == null)
                    continue;

                if (go.TryGetComponent(out HierarchyHighlighterView hierarchyHighlighterView))
                    hierarchyHighlighterView.viewData = viewData;
                else
                    go.AddComponent<HierarchyHighlighterView>().viewData = viewData;
                
                EditorUtility.SetDirty(go);
            }
        }

        private void OnResetButtonClicked()
        {
            var selectedGameObjects = Selection.gameObjects;
            
            if(selectedGameObjects == null || selectedGameObjects.Length < 1)
                return;

            foreach (var go in selectedGameObjects)
            {
                if(go == null)
                    continue;

                if (go.TryGetComponent(out HierarchyHighlighterView hierarchyHighlighterView))
                {
                    EditorUtility.SetDirty(go);
                    DestroyImmediate(hierarchyHighlighterView);
                }
            }
        }

        private void OnResetAllButtonClicked()
        {
            var hierarchyHighlighterViewMonoBehaviours = FindObjectsOfType<HierarchyHighlighterView>();

            foreach (var hierarchyHighlighterView in hierarchyHighlighterViewMonoBehaviours)
            {
                if(hierarchyHighlighterView == null)
                    continue;

                EditorUtility.SetDirty(hierarchyHighlighterView.gameObject);
                DestroyImmediate(hierarchyHighlighterView);
            }
        }

        private void OnFindNullsButtonClicked()
        {
            var monoBehaviours = FindObjectsOfType<MonoBehaviour>();

            _instanceIdsWithNullFields.Clear();

            foreach (var monoBehaviour in monoBehaviours)
            {
                var type = monoBehaviour.GetType();
                var fields = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

                foreach (var field in fields)
                {
                    bool isRequired = field.GetCustomAttributes(false)
                                           .Count(attribute => attribute is NaughtyAttributes.RequiredAttribute) > 0;

                    if (!isRequired)
                        continue;
                    
                    bool isValueNull = field.GetValue(monoBehaviour) == null;

                    if (isValueNull)
                    {
                        _instanceIdsWithNullFields.Add(monoBehaviour.gameObject.GetInstanceID());
                        Debug.Log($"[Hierarchy Highlighter] object with null field: {monoBehaviour.name}", monoBehaviour);
                    }
                }
            }

            if (_instanceIdsWithNullFields.Count < 1)
                Debug.Log("[Hierarchy Highlighter] null fields were not found. Good job!");
        }

        private void OnResetNullsButtonClicked()
        {
            _instanceIdsWithNullFields.Clear();
        }
    }
}