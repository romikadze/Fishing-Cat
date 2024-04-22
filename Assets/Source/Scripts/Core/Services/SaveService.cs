using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Source.Scripts.Core.Data;
using UnityEngine;

namespace Source.Scripts.Core.Services
{
    public class SaveService : MonoBehaviour
    {
        public Action OnSave;

        public FileSaveData CurrentSaveData { get; private set; } = new FileSaveData();

        private void Awake()
        {
            Load();
            Debug.Log("Load: " + CurrentSaveData);
        }

        private void OnApplicationQuit()
        {
            Save();
        }

        public void Save()
        {
            Debug.Log("Save");
            OnSave?.Invoke();
            
            BinaryFormatter formatter = new BinaryFormatter();
            using FileStream file = new FileStream(PathService.Data.FilePath, FileMode.Create);
            formatter.Serialize(file, CurrentSaveData);
        }

        public FileSaveData Load()
        {
            if (!IsFileExists())
                return CurrentSaveData;

            BinaryFormatter formatter = new BinaryFormatter();
            using FileStream file = new FileStream(PathService.Data.FilePath, FileMode.Open);
            return CurrentSaveData = (FileSaveData)formatter.Deserialize(file);
        }

        public bool IsFileExists()
        {
            return File.Exists(PathService.Data.FilePath);
        }
    }
}