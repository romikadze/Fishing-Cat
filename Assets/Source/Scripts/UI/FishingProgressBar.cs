using System;
using Source.Scripts.Fishing;
using UnityEngine;
using UnityEngine.UI;

namespace Source.Scripts.UI
{
    public class FishingProgressBar : MonoBehaviour
    {
        [SerializeField] private Image _fillImage;

        [SerializeField] private FishingMinigame _fishingMinigame;
        
        private void OnEnable()
        {
            _fishingMinigame.OnProgressChange += UpdateBar;
        }

        private void OnDisable()
        {
            _fishingMinigame.OnProgressChange -= UpdateBar;
        }

        private void UpdateBar(float value)
        {
            value *= 0.01f;
            _fillImage.fillAmount = value;
        }
    }
}