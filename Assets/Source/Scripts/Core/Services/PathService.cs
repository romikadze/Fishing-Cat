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
            public static readonly string FilePath = Application.persistentDataPath + "/saveData.data";
            public const string INVENTORY = "Inventory";
            public const string FISHING_ROD = "FishingRod";
            public const string SETTINGS = "Settings";
        }

        public class Prefabs
        {
            public const string FISHES = "Prefabs/Fishes/";
            public const string FISH_LIST = "Prefabs/Fishes/FishList";
        }
        
        public class UI
        {
          
        }
    }
}