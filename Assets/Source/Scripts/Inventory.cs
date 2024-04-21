using System;
using System.Collections.Generic;
using Source.Scripts.Fishing;
using UnityEngine;
using Zenject;

namespace Source.Scripts
{
    public class Inventory : MonoBehaviour
    {
        public Action<Fish, int> OnFishCountChange;

        private Dictionary<string, int> _fishes = new Dictionary<string, int>();

        [Inject]
        public void Constructor(FishingMinigame fishingMinigame)
        {
            fishingMinigame.OnFishCaught += AddFish;
        }

        private void AddFish(Fish fish)
        {
            if (!_fishes.TryAdd(fish.Name, 1))
            {
                _fishes[fish.Name]++;
            }

            OnFishCountChange?.Invoke(fish, _fishes[fish.Name]);
            Debug.Log("Add fish");
            Debug.Log("Count: " + _fishes[fish.Name]);
        }

        private bool TryRemoveFish(Fish fish)
        {
            if (_fishes.ContainsKey(fish.Name) && _fishes[fish.Name] > 0)
            {
                _fishes[fish.Name]--;

                if (_fishes[fish.Name] == 0)
                {
                    _fishes.Remove(fish.Name);
                }

                return true;
            }

            return false;
        }
    }
}