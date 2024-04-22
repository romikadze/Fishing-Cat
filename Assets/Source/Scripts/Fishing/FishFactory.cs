using System.IO;
using Source.Scripts.Core.Services;
using UnityEngine;
using Zenject;

namespace Source.Scripts.Fishing
{
    public class FishFactory : PlaceholderFactory<string, Fish>
    {
        private readonly DiContainer _container;
        private FishList _fishList;

        public FishFactory(DiContainer container, FishList fishList)
        {
            _container = container;
            _fishList = fishList;
        }

        public Fish Spawn(Vector3 position, Quaternion rotation, Transform parent)
        {
            return _container.InstantiatePrefabForComponent<Fish>(_fishList.GetRandomFish(), position, rotation, parent);
        }

        public void Despawn(Fish fish)
        {
            Object.Destroy(fish.gameObject);
        }
    }
}