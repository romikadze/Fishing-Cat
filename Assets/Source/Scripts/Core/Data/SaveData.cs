using System;

namespace Source.Scripts.Core.Data
{
    [Serializable]
    public class SaveData
    {
        public string Id;
        public Type Type;

        public SaveData()
        {
        }

        public SaveData(string id, Type type)
        {
            Id = id;
            Type = type;
        }
    }
}