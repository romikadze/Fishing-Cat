using System;
using System.Collections.Generic;
using System.Linq;
using Source.Scripts.Core.Data;
using Source.Scripts.Core.Services;
using Source.Scripts.Fishing;
using UnityEngine;
using Zenject;

namespace Source.Scripts
{
    public class Inventory : MonoBehaviour
    {
        public Action<string, int> OnFishCountChange;

        private SaveService _saveService;
        private Dictionary<string, int> _fishes;

        [Inject]
        public void Constructor(FishingMinigame fishingMinigame, SaveService saveService)
        {
            _saveService = saveService;

            fishingMinigame.OnFishCaught += AddFish;
        }

        private void Start()
        {
            GetInventoryData();
        }

        private void OnApplicationPause(bool pauseStatus)
        {
            if (pauseStatus)
                AddInventoryData();
        }

        private void AddFish(Fish fish)
        {
            if (!_fishes.TryAdd(fish.FishName, 1))
            {
                _fishes[fish.FishName]++;
            }

            OnFishCountChange?.Invoke(fish.FishName, _fishes[fish.FishName]);
        }

        private bool TryRemoveFish(Fish fish)
        {
            if (_fishes.ContainsKey(fish.FishName) && _fishes[fish.FishName] > 0)
            {
                _fishes[fish.FishName]--;

                if (_fishes[fish.FishName] == 0)
                {
                    _fishes.Remove(fish.FishName);
                }

                return true;
            }

            return false;
        }

        #region Save/Load

        private void AddInventoryData()
        {
            InventoryData inventoryData = new InventoryData();
            foreach (var fish in _fishes)
            {
                inventoryData.AddItem(fish.Key, fish.Value);
                Debug.Log(fish.Key + "/" + fish.Value);
            }
            _saveService.CurrentSaveData.AddData(PathService.Data.INVENTORY, inventoryData);
        }

        private void GetInventoryData()
        {
            if (_saveService.CurrentSaveData.TryGetData(PathService.Data.INVENTORY, out InventoryData inventoryData))
            {
                _fishes = new Dictionary<string, int>();
                _fishes = inventoryData.Inventory;

                foreach (var fish in _fishes)
                {
                    OnFishCountChange?.Invoke(fish.Key, fish.Value);
                }
            }
            else
                _fishes = new Dictionary<string, int>();
        }

        #endregion
    }
}