using UnityEngine;

namespace Source.Scripts.Effects
{
    [RequireComponent(typeof(Animator))]
    public class FishingRodEffects : MonoBehaviour
    {
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }
    }
}