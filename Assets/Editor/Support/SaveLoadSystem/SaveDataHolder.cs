using System;

namespace Support.SLS
{
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