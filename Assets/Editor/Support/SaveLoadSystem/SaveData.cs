using UnityEngine;

namespace Support
{
    [CreateAssetMenu(menuName = "Data/Save load system/Save", fileName = "NewSave")]
    public class SaveData : ScriptableObject
    {
        [Min(0)] public int currentLevel;
    }
}