using System;
using Source.Scripts.Core.Data;
using Source.Scripts.Core.Services;
using Source.Scripts.InputSystems;
using UnityEngine;
using Zenject;

namespace Source.Scripts.Fishing
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Swimmer : MonoBehaviour, IPause
    {
        public Action<bool> OnFishInTrigger;

        private Rigidbody2D _rigidbody;
        private GestureReceiver _gestureReceiver;
        private PauseService _pauseService;
        private FishingRod _fishingRod;

        private const float BASE_SPEED = 50;

        [Inject]
        public void Constructor(GestureReceiver gestureReceiver, PauseService pauseService, FishingRod fishingRod,
            FishingMinigame fishingMinigame)
        {
            _gestureReceiver = gestureReceiver;
            _pauseService = pauseService;
            _fishingRod = fishingRod;

            fishingMinigame.OnMinigameStart += EnableMovement;
            fishingMinigame.OnMinigameEnd += DisableMovement;
        }

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void OnEnable()
        {
            _pauseService.AddPause(this);
        }

        private void OnDisable()
        {
            _pauseService.RemovePause(this);
            _gestureReceiver.OnSwipe -= Move;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Fish fish))
            {
                OnFishInTrigger?.Invoke(true);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.TryGetComponent(out Fish fish))
            {
                OnFishInTrigger?.Invoke(false);
            }
        }

        private void EnableMovement()
        {
            _gestureReceiver.OnSwipe += Move;
        }

        private void DisableMovement()
        {
            _gestureReceiver.OnSwipe -= Move;
        }

        private void Move(Vector2 direction)
        {
            _rigidbody.AddForce(direction * BASE_SPEED * _fishingRod.SwimmerSpeed);
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