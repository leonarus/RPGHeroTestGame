using System;
using System.Collections.Generic;
using Currency;
using Hero;
using UnityEngine;

namespace HeroSelection
{
    public class HeroSelectionScreenManager : MonoBehaviour
    {
        public event Action<HeroController> HeroSelected;
        public event Action<HeroController> HeroChanged;
        public event Action<bool> HeroPurchaseAttempted;

        [SerializeField] private CurrencyManager _coins;
        [SerializeField] private CurrencyView _coinsView;
        [SerializeField] private CurrencyManager _gems;
        [SerializeField] private CurrencyView _gemsView;
        [SerializeField] private HeroSelectionScreenView _heroSelectionScreenView;
        [SerializeField] private HeroSwitcher _heroSwitcher;

        private HeroController _currentHeroController;
        private IReadOnlyList<HeroController> _heroControllers;

        public void Initialize(IReadOnlyList<HeroController> heroControllers)
        {
            _heroControllers = heroControllers;
            _heroSwitcher.Initialize(_heroControllers);
        }

        public void OpenScreen(HeroController heroController)
        {
            gameObject.SetActive(true);
            _coins.ScoreChangedEvent += _coinsView.UpdateView;
            _gems.ScoreChangedEvent += _gemsView.UpdateView;
            _coins.Initialize();
            _gems.Initialize();
            SetCurrentHero(heroController);
            _heroSwitcher.HeroChanged += OnHeroChanged;
            _heroSwitcher.SetCurrentHero(heroController);
        }

        public void TryBuyHero()
        {
            var heroSettings = _currentHeroController.HeroSettings;

            if (heroSettings.WasBought)
            {
                HeroPurchaseAttempted?.Invoke(false);
                return;
            }

            if (_coins.TryBuy(heroSettings.Price))
            {
                heroSettings.WasBought = true;
                // Обновляем представление в зависимости от результатов покупки
                _heroSelectionScreenView.UpdateHeroSelectionView(heroSettings);
                // Сообщаем о попытке покупки
                HeroPurchaseAttempted?.Invoke(true);
            }
            else
            {
                // Сообщаем о неудачной попытке покупки
                HeroPurchaseAttempted?.Invoke(false);
            }
        }

        public void SelectHero()
        {
            SetHeroSelection(_currentHeroController);
            HeroSelected?.Invoke(_currentHeroController);
        }

        public void OnBackButtonClicked()
        {
            // Возвращаемся к выбранному герою, предполагая, что он был сохранен ранее
            SetHeroSelection(_currentHeroController);
        }

        private void OnHeroChanged(HeroController currentHeroController)
        {
            SetCurrentHero(currentHeroController);
            HeroChanged?.Invoke(currentHeroController);
        }

        private void OnDisable()
        {
            _heroSwitcher.HeroChanged -= OnHeroChanged;
            _coins.ScoreChangedEvent += _coinsView.UpdateView;
            _gems.ScoreChangedEvent += _gemsView.UpdateView;
        }

        private void SetHeroSelection(HeroController hero)
        {
            foreach (var heroController in _heroControllers)
            {
                heroController.HeroSettings.IsSelected = false;
                heroController.gameObject.SetActive(false);
            }

            if (hero == null) return;
            hero.HeroSettings.IsSelected = true;
            hero.gameObject.SetActive(true);
        }

        private void SetCurrentHero(HeroController heroController)
        {
            _currentHeroController = heroController;
            _heroSelectionScreenView.UpdateHeroSelectionView(heroController.HeroSettings);
        }
    }
}
