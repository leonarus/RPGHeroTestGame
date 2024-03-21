using System.Collections.Generic;
using Hero;
using Hero.Settings;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace HeroSelection
{
    public class HeroSelectionScreenView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _heroNameText;
        [SerializeField] private Image _typeImage;
        [SerializeField] private TextMeshProUGUI _levelText;
        [SerializeField] private TextMeshProUGUI _experienceText;
        [SerializeField] private TextMeshProUGUI _descriptionText;
        [SerializeField] private Slider _healthSlider;
        [SerializeField] private Slider _attackSlider;
        [SerializeField] private Slider _defenseSlider;
        [SerializeField] private Slider _speedSlider;
        [SerializeField] private Sprite[] _heroTypesSprite;
        [SerializeField] private TextMeshProUGUI _priceText;
        [SerializeField] private Button _buyButton;
        [SerializeField] private Button _selectButton;

        private readonly Dictionary<HeroType, int> _typeSpriteIndexMap = new()
        {
            { HeroType.Melee, 0 },
            { HeroType.Archer, 1 },
            { HeroType.Mage, 2 }
        };

        public void UpdateHeroSelectionView(HeroSettings heroSettings)
        {
            UpdateButtonView(heroSettings.WasBought);
            UpdateHeroTextData(heroSettings);
            UpdateHeroStatsData(heroSettings);
            UpdateSpriteHeroType(heroSettings.Type);
        }

        private void UpdateButtonView(bool heroWasBought)
        {
            _buyButton.gameObject.SetActive(!heroWasBought);
            _selectButton.interactable = heroWasBought;
        }

        private void UpdateHeroTextData(HeroSettings heroSettings)
        {
            _heroNameText.text = heroSettings.Name;
            _levelText.text = heroSettings.Level.ToString();
            _experienceText.text = $"{heroSettings.Experience}/100";
            _descriptionText.text = heroSettings.Description;
            _priceText.text = heroSettings.Price.ToString();
        }

        private void UpdateHeroStatsData(HeroSettings heroSettings)
        {
            var maxSettings = HeroesManager.MaxSettings;

            _healthSlider.value = NormalizeValue(heroSettings.Health, maxSettings.Health);
            _attackSlider.value = NormalizeValue(heroSettings.Attack, maxSettings.Attack);
            _defenseSlider.value = NormalizeValue(heroSettings.Defense, maxSettings.Defense);
            _speedSlider.value = NormalizeValue(heroSettings.Speed, maxSettings.Speed);
        }

        private void UpdateSpriteHeroType(HeroType heroType)
        {
            if (_typeSpriteIndexMap.TryGetValue(heroType, out var spriteIndex))
            {
                _typeImage.sprite = _heroTypesSprite[spriteIndex];
            }
        }

        private static float NormalizeValue(float value, float maxValue)
        {
            return value / maxValue;
        }
    }
}
