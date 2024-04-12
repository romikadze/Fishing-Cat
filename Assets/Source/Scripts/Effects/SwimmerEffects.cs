using System;
using Source.Scripts.Fishing;
using Source.Scripts.InputSystems;
using UnityEngine;
using Zenject;

namespace Source.Scripts.Effects
{
    [RequireComponent(typeof(Animator))]
    public class SwimmerEffects : MonoBehaviour
    {
        private Animator _animator;

        private static readonly int Direction = Animator.StringToHash("Direction");
        private static readonly int OnStartMinigame = Animator.StringToHash("OnStartMinigame");
        private static readonly int OnEndMinigame = Animator.StringToHash("OnEndMinigame");
        private static readonly int OnFishSpawned = Animator.StringToHash("OnFishSpawned");

        [Inject]
        public void Constructor(FishingMinigame fishingMinigame, GestureReceiver gestureReceiver)
        {
            fishingMinigame.OnFishSpawned += () => { _animator.SetTrigger(OnFishSpawned); };
            fishingMinigame.OnMinigameStart += () => { _animator.SetTrigger(OnStartMinigame); };
            fishingMinigame.OnMinigameEnd += () => { _animator.SetTrigger(OnEndMinigame); };
            gestureReceiver.OnSwipe += (direction => { _animator.SetFloat(Direction, direction.x); });
        }

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }
    }
}