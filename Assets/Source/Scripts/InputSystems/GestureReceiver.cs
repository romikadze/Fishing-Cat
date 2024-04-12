using System;
using Source.Scripts.Core.Data;
using Source.Scripts.Core.Services;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Source.Scripts.InputSystems
{
    public class GestureReceiver : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public Action<Vector2> OnSwipe;

        [SerializeField] private float _minSwipeMagnitude;
        private float _maxSwipeMagnitude = 5f;
        private Vector2 _pointerDownPosition;
        
        [Inject]
        public void Constructor(SaveService saveService)
        {
            if (saveService.CurrentSaveData
                .TryGetData(PathService.Data.SETTINGS_ID, out SettingsData settingsData))
                _maxSwipeMagnitude = settingsData.MaxSwipeMagnitude;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _pointerDownPosition = eventData.position;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            Vector2 swipe = eventData.position - _pointerDownPosition;
            if (swipe.magnitude >= _minSwipeMagnitude)
            {
                if (swipe.magnitude > 3)//_maxSwipeMagnitude)
                    swipe = swipe.normalized * 3;//_maxSwipeMagnitude;

                OnSwipe?.Invoke(swipe.normalized);
            }
        }
    }
}