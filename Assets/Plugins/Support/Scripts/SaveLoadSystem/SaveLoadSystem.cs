using NaughtyAttributes;
using UnityEngine;

namespace Support.SLS
{
    /// <summary>
    /// Class that is responsible for managing saves
    /// </summary>
    public sealed class SaveLoadSystem : MonoBehaviour
    {
        private SaveData _saveData;
        private ISaveDataSerializer _saveDataSerializer = new BinarySerializer();

        /// <summary>Data that can be saved</summary>
        public SaveData SaveData => _saveData;

        private void Awake()
        {
            var serializedSaveData = PlayerPrefs.GetString("save");
            _saveData = string.IsNullOrEmpty(serializedSaveData) ? new SaveData() : _saveDataSerializer.DeserializeData(serializedSaveData);
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