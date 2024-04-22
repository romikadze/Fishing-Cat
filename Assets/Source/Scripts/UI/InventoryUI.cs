using System;
using System.Collections.Generic;
using Source.Scripts.Core.Services;
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

        private void UpdateFishIcon(string fishName, int count)
        {
            if (!_fishIcons.ContainsKey(fishName))
            {
                FishIcon fishIcon = Instantiate(_fishIconPrefab, _contentTransform);
                Debug.Log(PathService.Prefabs.FISH_PATH + fishName);
                fishIcon.SetupIcon(Resources.Load<Fish>(PathService.Prefabs.FISH_PATH + fishName));

                _fishIcons.Add(fishName, fishIcon);
            }

            _fishIcons[fishName].UpdateCount(count);
        }
    }
}