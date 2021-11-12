using NaughtyAttributes;
using UnityEngine;

namespace Support.SLS
{
    /// <summary>
    /// Class that is responsible for managing saves
    /// </summary>
    public class SaveLoadSystem : MonoSingleton<SaveLoadSystem>
    {
        private SaveData _saveData;
        private ISaveDataSerializer _saveDataSerializer = new BinarySerializer();

        /// <summary>Data that can be saved</summary>
        public SaveData SaveData => _saveData;

        protected override void Awake()
        {
            base.Awake();

            var serializedSaveData = PlayerPrefs.GetString("save");
            if (string.IsNullOrEmpty(serializedSaveData)) 
                _saveData = new SaveData();
            else
                _saveData = _saveDataSerializer.DeserializeData(serializedSaveData);
        }

        /// <summary>Saves data to the drive</summary>
        public void PerformSave()
        {
            var serializedData = _saveDataSerializer.SerializeData(_saveData);
            
            PlayerPrefs.SetString("save", serializedData);
            PlayerPrefs.Save();
        }

        /// <summary>Deletes saved data from the drive</summary>
        [Button("Clear save data")]
        public void ClearSaveData()
        {
            PlayerPrefs.SetString("save", null);
            PlayerPrefs.Save();
        }
    }
}