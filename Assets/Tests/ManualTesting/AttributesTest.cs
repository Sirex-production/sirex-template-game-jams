using EditorExtensions;
using UnityEngine;

namespace Tests.Manual
{
    public class AttributesTest : MonoBehaviour
    {
        [NotNull]
        [SerializeField] private GameObject notNull;

        [ReadOnly] 
        [SerializeField] private GameObject readOnly;

        [SerializeField] private GameObject usualField;

        //todo Does not work with value types
        // [NotNull]
        // public int valueType = 2;
    }
}