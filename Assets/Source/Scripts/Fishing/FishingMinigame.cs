using System;
using System.Collections;
using Source.Scripts.Core.Data;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Source.Scripts.Fishing
{
    public class FishingMinigame : MonoBehaviour
    {
        public Action OnFishSpawned;
        public Action OnMinigameStart;
        public Action OnMinigameEnd;
        public Action<Fish> OnFishCaught;
        public Action<float> OnProgressChange;

        [SerializeField] private Transform _fishSpawnPoint;

        [Header("Fishing Options")] [SerializeField] [Range(10f, 80f)]
        private float _startProgress;

        [SerializeField] [Range(0.1f, 0.5f)] private float _progressIncrement;
        [SerializeField] [Range(-0.1f, -0.5f)] private float _progressDecrement;

        private FishFactory _fishFactory;
        private Swimmer _swimmer;
        private FishingRod _fishingRod;
        private Coroutine _progressTick;
        private Fish _fish;
        private FishingRodData _fishingRodData;

        private float _progress;
        private float _looseValue = 0f;
        private float _winValue = 100f;
        private float _progressDelta;

        [Inject]
        public void Constructor(FishFactory fishFactory, Swimmer swimmer, FishingRod fishingRod)
        {
            _fishFactory = fishFactory;
            _swimmer = swimmer;
            _fishingRod = fishingRod;

            _fishingRod.OnHooked += () => StartCoroutine(ProgressTick());
            _swimmer.OnFishInTrigger += ChangeProgressWay;
        }

        private void Start()
        {
            StartCoroutine(SpawnFish());
        }

        private IEnumerator SpawnFish()
        {
            yield return new WaitForSeconds(Random.Range(3f, 7f));
            OnFishSpawned?.Invoke();
        }

        private void ChangeProgressWay(bool increase)
        {
            if (increase)
                _progressDelta = _progressIncrement * GetFishingCoefficient();
            else
                _progressDelta = _progressDecrement;
        }

        private void FishCaught()
        {
            OnFishCaught?.Invoke(_fish);
        }

        private IEnumerator ProgressTick()
        {
            _fish = _fishFactory.Spawn(_fishSpawnPoint.position, Quaternion.identity, null);
            OnMinigameStart?.Invoke();
            _progress = _startProgress;
            while (_progress < _winValue && _progress > _looseValue)
            {
                _progress += _progressDelta;
                OnProgressChange?.Invoke(_progress);
                yield return new WaitForEndOfFrame();
            }

            if (_progress >= _winValue)
                FishCaught();

            _fishFactory.Despawn(_fish);
            OnMinigameEnd?.Invoke();
            StartCoroutine(SpawnFish());
        }

        private float GetFishingCoefficient()
        {
            return _fishingRod.FishingPower / _fish.Power;
        }
    }
}