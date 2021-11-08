namespace Support.SLS
{
    public interface ISaveDataSerializer
    {
        /// <summary>
        /// Method that serializes SaveData in some format
        /// </summary>
        /// <param name="saveData">Save data that will be serialized</param>
        /// <returns>String that represents serialized data</returns>
        public string SerializeData(SaveData saveData);
        /// <summary>
        /// Method that deserializes SaveData from the string
        /// </summary>
        /// <param name="serializedSaveData">string that represents serialized saveData</param>
        /// <returns>Deserialized SaveData</returns>
        public SaveData DeserializeData(string serializedSaveData);
    }
}