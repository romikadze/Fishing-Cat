using System;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Source.Scripts.Fishing
{
    public class FishingMinigame : MonoBehaviour
    {
        public Action OnFishSpawned;
        public Action OnMinigameStart;
        public Action OnMinigameEnd;

        [SerializeField] private Transform _fishSpawnPoint;

        private FishFactory _fishFactory;
        private Swimmer _swimmer;
        private FishingRod _fishingRod;
        private float _progress = 30;
        private Coroutine _progressTick;
        private float _looseValue = 0f;
        private float _winValue = 100f;
        private float _progressDelta = 0.1f;

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
            _fishFactory.Spawn(_fishSpawnPoint.position, Quaternion.identity, null);
            OnFishSpawned?.Invoke();
        }

        private void ChangeProgressWay(bool increase)
        {
            if (increase)
                _progressDelta = Mathf.Abs(_progressDelta);
            else
                _progressDelta = -Mathf.Abs(_progressDelta);
        }

        private IEnumerator ProgressTick()
        {
            OnMinigameStart?.Invoke();
            while (_progress < _winValue && _progress > _looseValue )
            {
                _progress += _progressDelta;
                yield return new WaitForEndOfFrame();
            }
            OnMinigameEnd?.Invoke();
        }
        
    }
}