using System;
using Source.Scripts.Core.Services;
using Source.Scripts.Fishing;
using Source.Scripts.InputSystems;
using UnityEngine;
using Zenject;

namespace Source.Scripts.Effects
{
    [RequireComponent(typeof(Animator))]
    public class SwimmerEffects : MonoBehaviour, IPause
    {
        private Animator _animator;
        private PauseService _pauseService;

        private static readonly int Direction = Animator.StringToHash("Direction");
        private static readonly int OnMinigameStart = Animator.StringToHash("OnMinigameStart");
        private static readonly int OnMinigameEnd = Animator.StringToHash("OnMinigameEnd");
        private static readonly int OnFishSpawned = Animator.StringToHash("OnFishSpawned");

        [Inject]
        public void Constructor(FishingMinigame fishingMinigame, GestureReceiver gestureReceiver, PauseService pauseService)
        {
            _pauseService = pauseService;
            
            fishingMinigame.OnFishSpawned += () => { _animator.SetTrigger(OnFishSpawned); };
            fishingMinigame.OnMinigameStart += () => { _animator.SetTrigger(OnMinigameStart); };
            fishingMinigame.OnMinigameEnd += () => { _animator.SetTrigger(OnMinigameEnd); };
            gestureReceiver.OnSwipe += (direction => { _animator.SetFloat(Direction, direction.x); });
        }

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void OnEnable()
        {
            _pauseService.AddPause(this);
        }

        private void OnDisable()
        {
            _pauseService.RemovePause(this);
        }

        public void Pause()
        {
            throw new NotImplementedException();
        }

        public void Resume()
        {
            throw new NotImplementedException();
        }
    }
}