using UnityEngine;

namespace Source.Scripts.Fishing
{
    public class Fish : MonoBehaviour
    {
        [field: SerializeField] public float Power { get; private set; }
        [field: SerializeField] public Sprite FishSprite { get; private set; }
        [field: SerializeField] public string Name { get; private set; }
        
    }
}