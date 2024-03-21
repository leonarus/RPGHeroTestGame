using Hero;
using HeroSelection;
using Lobby;
using UnityEngine;

public class GameScreenManager : MonoBehaviour
{
    [SerializeField] private HeroesManager _heroesManager;
    [SerializeField] private LobbyScreenManager _lobbyScreenManager;
    [SerializeField] private HeroSelectionScreenManager _heroSelectionScreenManager;

    private void Start()
    {
        var heroes = _heroesManager.GetHeroes();
        _lobbyScreenManager.Initialize(heroes);
        _heroSelectionScreenManager.Initialize(heroes);
        _heroSelectionScreenManager.HeroChanged += OnHeroChanged;
    }
    
    private void OnHeroChanged(HeroController selectedHero)
    {
        _heroesManager.ActivateSelectedHero(selectedHero);
    }
    
    private void OnDestroy()
    {
        _heroSelectionScreenManager.HeroChanged -= OnHeroChanged;
    }
}