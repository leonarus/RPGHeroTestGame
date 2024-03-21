using System.Collections.Generic;
using System.Linq;
using Currency;
using Hero;
using HeroSelection;
using UnityEngine;

namespace Lobby
{
    public class LobbyScreenManager : MonoBehaviour
    {
        [SerializeField] 
        private CurrencyManager _coins;
        [SerializeField] 
        private CurrencyManager _gems;
        [SerializeField] 
        private CurrencyView _coinsView;
        [SerializeField] 
        private CurrencyView _gemsView;
        [SerializeField] 
        private LobbyScreenView _lobbyScreenView;
        [SerializeField] 
        private HeroSelectionScreenManager _heroSelectionScreenManager;

        [SerializeField] private LuckySpinScreenManager _luckySpinScreen;
        
        private HeroController _currentHeroController;
        
        public void Initialize(IEnumerable<HeroController> heroControllers)
        {
            SubscribeToEvents();
    
            _currentHeroController = heroControllers.FirstOrDefault(hero => hero.HeroSettings.IsSelected);
            if (_currentHeroController != null)
            {
                _lobbyScreenView.UpdateUI(_currentHeroController.HeroSettings);
            }
            
            _coins.Initialize();
            _gems.Initialize();
        }
        
        public void OpenHeroSelectionScreen()
        {
            _heroSelectionScreenManager.OpenScreen(_currentHeroController);
        }
        
        
        private void OnHeroSelected(HeroController heroController)
        {
            _currentHeroController = heroController;
            _lobbyScreenView.UpdateUI(heroController.HeroSettings);
        }
        private void SubscribeToEvents()
        {
            _heroSelectionScreenManager.HeroSelected += OnHeroSelected;
            _coins.ScoreChangedEvent += _coinsView.UpdateView;
            _gems.ScoreChangedEvent += _gemsView.UpdateView;
        }

        private void UnsubscribeFromEvents()
        {
            _heroSelectionScreenManager.HeroSelected -= OnHeroSelected;
            _coins.ScoreChangedEvent -= _coinsView.UpdateView;
            _gems.ScoreChangedEvent -= _gemsView.UpdateView;
        }
        
        private void OnDestroy()
        {
            UnsubscribeFromEvents();
        }
    }
}