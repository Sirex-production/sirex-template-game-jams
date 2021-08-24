using UnityEngine;

namespace Support
{
    public class SaveLoadSystem : MonoSingleton<SaveLoadSystem>
    {
        [SerializeField] private SaveData saveData;

        public SaveData SaveData => saveData;
    }
}