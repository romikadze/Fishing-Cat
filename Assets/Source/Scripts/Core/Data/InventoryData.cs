using System;
using System.Collections.Generic;

namespace Source.Scripts.Core.Data
{
    [Serializable]
    public class InventoryData : SaveData
    {
        public Dictionary<string, int> Inventory { get; private set; } = new Dictionary<string, int>();

        public void AddItem(string itemName, int itemCount)
        {
            Inventory.Add(itemName, itemCount);
        }
    }
}