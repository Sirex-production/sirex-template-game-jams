using UnityEngine;

namespace Support.SLS
{
    public class SaveLoadSystem : MonoSingleton<SaveLoadSystem>
    {
        private SaveData _saveData;
        private ISaveDataSerializer _saveDataSerializer = new BinarySerializer();

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

        public void PerformSave()
        {
            var serializedData = _saveDataSerializer.SerializeData(_saveData);
            
            PlayerPrefs.SetString("save", serializedData);
            PlayerPrefs.Save();
        }

        public void ClearSaveData()
        {
            PlayerPrefs.SetString("save", null);
            PlayerPrefs.Save();
        }
    }
}