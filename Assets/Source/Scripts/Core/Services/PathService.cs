using UnityEngine;

namespace Source.Scripts.Core.Services
{
    public static class PathService
    {
        public class Services
        {
            public const string PAUSE_SERVICE = "Prefabs/Services/PauseService";
            public const string SAVE_SERVICE = "Prefabs/Services/SaveService";
            public const string SETTINGS = "Prefabs/Services/Settings";
        }
        
        public class Data
        {
            public static readonly string FilePath = Application.persistentDataPath + "/saveData.json";
            public const string SETTINGS_ID = "Settings";
        }

        public class Prefabs
        {
            public const string FISH = "Prefabs/GameObjects/Space Carp";
        }
        
        public class UI
        {
            public const string FishingProgressBar = "Prefabs/UI/FishingProgressBar";
        }
    }
}