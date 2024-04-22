using Source.Scripts.Fishing;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Source.Scripts.UI
{
    public class FishIcon : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TextMeshProUGUI _countText;
        [SerializeField] private TextMeshProUGUI _nameText;
        
        public void SetupIcon(Fish fish)
        {
            _icon.sprite = fish.FishSprite;
            _nameText.text = fish.FishName;
        }
        
        public void UpdateCount(int count)
        {
            _countText.text = count.ToString();
        }
    }
}