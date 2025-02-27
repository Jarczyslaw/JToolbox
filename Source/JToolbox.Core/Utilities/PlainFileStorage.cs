using System;
using System.IO;

namespace JToolbox.Core.Utilities
{
    public abstract class PlainFileStorage<T>
        where T : class, new()
    {
        private static readonly object _lock = new object();

        protected PlainFileStorage()
        {
            Load();
        }

        public T Data { get; private set; }

        public abstract string FilePath { get; }

        public T Load()
        {
            lock (_lock)
            {
                bool createNewStorageFile = PerformLoading();
                if (createNewStorageFile)
                {
                    PerformSaving();
                }

                return Data;
            }
        }

        public void Save()
        {
            lock (_lock)
            {
                PerformSaving();
            }
        }

        public void Update(Action<T> action)
        {
            lock (_lock)
            {
                action(Data);
                PerformSaving();
            }
        }

        protected abstract T DeserializeData(string serialized);

        protected abstract string SerializeData(T data);

        private bool PerformLoading()
        {
            if (!File.Exists(FilePath))
            {
                Data = new T();
                return true;
            }
            else
            {
                string serialized = File.ReadAllText(FilePath);
                Data = DeserializeData(serialized);

                return false;
            }
        }

        private void PerformSaving()
        {
            string serialized = SerializeData(Data);
            File.WriteAllText(FilePath, serialized);
        }
    }
}