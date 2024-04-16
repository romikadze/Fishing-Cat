using Source.Scripts.Core.Services;
using Source.Scripts.Fishing;
using Source.Scripts.InputSystems;
using UnityEngine;
using Zenject;

namespace Source.Scripts.Effects
{
    [RequireComponent(typeof(Animator))]
    public class CatEffects : MonoBehaviour, IPause
    {
        private Animator _animator;
        private PauseService _pauseService;
        
        private static readonly int OnHook = Animator.StringToHash("OnHook");
        private static readonly int OnMinigameEnd = Animator.StringToHash("OnMinigameEnd");
        private static readonly int Direction = Animator.StringToHash("Direction");

        [Inject]
        public void Constructor(FishingMinigame fishingMinigame, GestureReceiver gestureReceiver, FishingRod fishingRod, PauseService pauseService)
        {
            _pauseService = pauseService;
            
            fishingRod.OnHooked += () => { _animator.SetTrigger(OnHook); };
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
            throw new System.NotImplementedException();
        }

        public void Resume()
        {
            throw new System.NotImplementedException();
        }
    }
}