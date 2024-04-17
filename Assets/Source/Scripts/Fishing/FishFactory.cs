using Source.Scripts.Core.Services;
using UnityEngine;
using Zenject;

namespace Source.Scripts.Fishing
{
    public class FishFactory : PlaceholderFactory<string, Fish>
    {
        private readonly DiContainer _container;
        
        public FishFactory(DiContainer container)
        {
            _container = container;
        }

        public Fish Spawn(Vector3 position, Quaternion rotation, Transform parent)
        {
            return _container.InstantiatePrefabResourceForComponent<Fish>(PathService.Prefabs.FISH, 
                position, rotation, parent);
        }

        public void Despawn(Fish fish)
        {
            Object.Destroy(fish.gameObject);
        }
    }
}