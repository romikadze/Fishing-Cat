using System;
using System.Collections.Generic;
using Source.Scripts.Fishing;
using UnityEngine;

namespace Source.Scripts.UI
{
    [RequireComponent(typeof(Inventory))]
    public class InventoryUI : MonoBehaviour
    {
        [SerializeField] private FishIcon _fishIconPrefab;
        [SerializeField] private Transform _contentTransform;
        private Inventory _inventory;
        private Dictionary<String, FishIcon> _fishIcons = new Dictionary<String, FishIcon>();

        private void Awake()
        {
            _inventory = GetComponent<Inventory>();
            _inventory.OnFishCountChange += UpdateFishIcon;
        }

        private void UpdateFishIcon(Fish fish, int count)
        {
            if (!_fishIcons.ContainsKey(fish.Name))
            {
                FishIcon fishIcon = Instantiate(_fishIconPrefab, _contentTransform);
                fishIcon.SetupIcon(fish);

                _fishIcons.Add(fish.Name, fishIcon);
            }

            _fishIcons[fish.Name].UpdateCount(count);
        }
    }
}