using System;
using System.Collections.Generic;

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

        public void AddData(string id, SaveData data)
        {
            if (_data.ContainsKey(id))
                _data.Remove(id);
            _data[id] = data;
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