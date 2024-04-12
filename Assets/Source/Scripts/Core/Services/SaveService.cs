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
        
        public void Save()
        {
            OnSave?.Invoke();

            using FileStream file = File.Create(PathService.Data.FilePath);
            new BinaryFormatter().Serialize(file, CurrentSaveData);
        }

        public FileSaveData Load()
        {
            if (!IsFileExists())
                Save();

            using FileStream file = File.Open(PathService.Data.FilePath, FileMode.Open);
            return CurrentSaveData = (FileSaveData)new BinaryFormatter().Deserialize(file);
        }

        public bool IsFileExists()
        {
            return File.Exists(PathService.Data.FilePath);
        }
    }
}