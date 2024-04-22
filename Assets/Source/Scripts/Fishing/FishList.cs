using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Source.Scripts.Fishing
{
    public class FishList : MonoBehaviour
    {
        [SerializeField] private List<FishOption> _fishes;

        public Fish GetRandomFish()
        {
            float spawnChance = 0;
            foreach (var fishOption in _fishes)
            {
                spawnChance += fishOption.SpawnChance;
            }
            float random = Random.Range(0, spawnChance);

            foreach (var fishOption in _fishes)
            {
                if (random < fishOption.SpawnChance)
                    return fishOption.Fish;
                random -= fishOption.SpawnChance;
            }

            return _fishes[0].Fish;
        }
    }

    [Serializable]
    public class FishOption
    {
        [field: SerializeField] public Fish Fish { get; private set; }
        [field: SerializeField] public float SpawnChance { get; private set; }
    }
}