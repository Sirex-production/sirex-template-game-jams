using System;

namespace Support.SLS
{
    /// <summary>
    /// Class that represents single data entity that can be saved
    /// </summary>
    /// <typeparam name="T">Type of data that will be saved</typeparam>
    [Serializable]
    public class SaveDataHolder<T>
    {
        private T _data;
        
        public T Data
        {
            get => _data;
            
            set
            {
                _data = value;
                OnDataChanged?.Invoke(_data);
            }
        }

        internal event Action<T> OnDataChanged;

        internal SaveDataHolder() { }

        internal SaveDataHolder(T data) => _data = data;
    }
}