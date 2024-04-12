using System;
using System.Collections.Generic;
using Source.Scripts.Core.Services;

namespace Source.Scripts.Core.Data
{
    [Serializable]
    public class FileSaveData : SaveData
    {
        private Dictionary<string, SaveData> _data = new Dictionary<string, SaveData>();

        public bool TryGetData<T>(string id, out T data) where T : SaveData
        {
            if (_data.TryGetValue(id, out SaveData saveData))
            {
                data = (T)saveData;
                return true;
            }

            data = null;
            return false;
        }

        public bool TryAddData(string id, SaveData data)
        {
            return _data.TryAdd(id, data);
        }

        public bool TryRemoveData(string id)
        {
            return _data.Remove(id);
        }

        public bool DataExists(string id)
        {
            return _data.ContainsKey(id);
        }
    }
}