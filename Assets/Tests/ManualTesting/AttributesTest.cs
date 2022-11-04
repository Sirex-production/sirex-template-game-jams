using NaughtyAttributes;
using UnityEngine;

namespace Support.Tests.Manual
{
    public class AttributesTest : MonoBehaviour
    {
        [Required, SerializeField] private GameObject notNull;

        [ReadOnly] 
        [SerializeField] private GameObject readOnly;

        [SerializeField] private GameObject usualField;
    }
}