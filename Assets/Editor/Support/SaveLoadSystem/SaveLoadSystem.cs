using Unity.Plastic.Antlr3.Runtime.Misc;
using UnityEngine;

namespace Support
{
    public class SaveLoadSystem : MonoSingleton<SaveLoadSystem>
    {
        [SerializeField] private SaveData saveData;

        public event Action OnValueChanged;
        
        public SaveData SaveData
        {
            get
            {
                OnValueChanged?.Invoke();
                
                return saveData;
            }
        }
    }
}