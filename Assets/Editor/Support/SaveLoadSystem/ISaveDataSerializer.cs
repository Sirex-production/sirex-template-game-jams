namespace Support.SLS
{
    public interface ISaveDataSerializer
    {
        public string SerializeData(SaveData saveData);
        public SaveData DeserializeData(string serializedSaveData);
    }
}