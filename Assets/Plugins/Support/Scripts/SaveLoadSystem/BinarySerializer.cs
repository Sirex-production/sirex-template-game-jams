using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Support.SLS
{
    /// <summary>
    /// Class that is responsible for serializing and deserializing SaveData into binary format
    /// </summary>
    public class BinarySerializer : ISaveDataSerializer
    {
        /// <summary>
        /// Method that serializes SaveData in binary format
        /// </summary>
        /// <param name="saveData">Save data that will be serialized</param>
        /// <returns>String that represents serialized data in binary format</returns>
        public string SerializeData(SaveData saveData)
        {
            using (var stream = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, saveData);

                return Convert.ToBase64String(stream.ToArray());
            }
        }

        /// <summary>
        /// Method that deserializes SaveData from the string
        /// </summary>
        /// <param name="serializedSaveData">string that represents serialized SaveData in binary form</param>
        /// <returns>Deserialized SaveData</returns>
        public SaveData DeserializeData(string serializedSaveData)
        {
            var serializedBytesData = Convert.FromBase64String(serializedSaveData);

            using (var stream = new MemoryStream(serializedBytesData))
            {
                var formatter = new BinaryFormatter();
                var deserializedSaveData = formatter.Deserialize(stream);

                return (SaveData) deserializedSaveData;
            }
        }
    }
}