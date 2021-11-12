using System;

namespace Support.SLS
{
    /// <summary>
    /// Class that stores all the data that can be saved
    /// </summary>
    [Serializable]
    public class SaveData
    {
        public SaveDataHolder<int> CurrentLevelNumber { get; } = new SaveDataHolder<int>();
    }
}