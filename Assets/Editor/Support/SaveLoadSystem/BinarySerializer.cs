using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Support.SLS
{
    public class BinarySerializer : ISaveDataSerializer
    {
        public string SerializeData(SaveData saveData)
        {
            using (var stream = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, saveData);

                return Convert.ToBase64String(stream.ToArray());
            }
        }

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