using System;
using System.Collections.Generic;
using Hero;
using UnityEngine;

namespace HeroSelection
{
    public class HeroSwitcher : MonoBehaviour
    {
        public event Action<HeroController> HeroChanged;

        private IReadOnlyList<HeroController> _heroControllers;
        private int _currentHeroIndex;

        public void Initialize(IReadOnlyList<HeroController> heroControllers)
        {
            _heroControllers = heroControllers;
            DeactivateAllHeroes();
        }

        public void SetCurrentHero(HeroController heroController)
        {
            _currentHeroIndex = GetCurrentHeroIndex(heroController);
            UpdateHeroActivation(_currentHeroIndex);
        }

        public void GoToPreviousHero()
        {
            ChangeHero(-1);
        }

        public void GoToNextHero()
        {
            ChangeHero(1);
        }

        private void ChangeHero(int direction)
        {
            _currentHeroIndex = (_currentHeroIndex + direction + _heroControllers.Count) % _heroControllers.Count;
            UpdateHeroActivation(_currentHeroIndex);
        }

        private void UpdateHeroActivation(int index)
        {
            DeactivateAllHeroes();

            if (index >= 0 && index < _heroControllers.Count)
            {
                var currentHeroController = _heroControllers[index];
                currentHeroController.gameObject.SetActive(true);
                HeroChanged?.Invoke(currentHeroController);
            }
        }

        private void DeactivateAllHeroes()
        {
            foreach (var heroController in _heroControllers)
            {
                heroController.gameObject.SetActive(false);
            }
        }

        private int GetCurrentHeroIndex(HeroController heroController)
        {
            for (var i = 0; i < _heroControllers.Count; i++)
            {
                if (_heroControllers[i] == heroController)
                {
                    return i;
                }
            }
            return -1; // If the hero is not found
        }
    }
}
