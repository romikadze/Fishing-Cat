using System;

namespace Source.Scripts.Core.Data
{
    [Serializable]
    public class SettingsData : SaveData
    {
        public float MaxSwipeMagnitude { get; set; }
        public float Sounds { get; set; }
        public float Music { get; set; }
    }
}