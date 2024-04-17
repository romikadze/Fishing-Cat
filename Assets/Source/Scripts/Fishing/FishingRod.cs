using System;
using System.Collections;
using Source.Scripts.Core.Services;
using Source.Scripts.InputSystems;
using UnityEngine;
using Zenject;

namespace Source.Scripts.Fishing
{
    [RequireComponent(typeof(Animator))]
    public class FishingRod : MonoBehaviour, IPause
    {
        public Action OnHooked;

        [field: SerializeField] public float SwimmerSpeed { get; private set; }
        [field: SerializeField] public float FishingPower { get; private set; }
        [field: SerializeField] public float FishingAccuracyMultiplier { get; private set; }
        
        [SerializeField] private float _minWaitTime;
        [SerializeField] private float _maxWaitTime;

        private GestureReceiver _gestureReceiver;
        private PauseService _pauseService;
        private Coroutine _countdownTick;

        [Inject]
        public void Constructor(GestureReceiver gestureReceiver, PauseService pauseService,
            FishingMinigame fishingMinigame)
        {
            _gestureReceiver = gestureReceiver;
            _pauseService = pauseService;

            fishingMinigame.OnFishSpawned += SetReadyToHook;
        }

        private void OnEnable()
        {
            _pauseService.AddPause(this);
        }

        private void OnDisable()
        {
            _pauseService.RemovePause(this);
            _gestureReceiver.OnSwipe -= Hook;
        }

        private void Hook(Vector2 direction)
        {
            if (direction.y > 0 && direction.y > Mathf.Abs(direction.x) * 2)
            {
                if (_countdownTick != null)
                    StopCoroutine(_countdownTick);
                
                OnHooked?.Invoke();
                _gestureReceiver.OnSwipe -= Hook;
            }
        }

        private void SetReadyToHook()
        {
            _gestureReceiver.OnSwipe += Hook;
            _countdownTick = StartCoroutine(CountdownTick());
        }

        private IEnumerator CountdownTick()
        {
            yield return new WaitForSeconds(5);
            _gestureReceiver.OnSwipe -= Hook;
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