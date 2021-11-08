using System;

namespace Support.SLS
{
    [Serializable]
    public class SaveData
    {
        public SaveDataHolder<int> CurrentLevelNumber { get; } = new SaveDataHolder<int>();
    }
}