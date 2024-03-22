using Currency;
using Managers;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LuckySpinScreenManager : MonoBehaviour
{
    [SerializeField] private AudioManager _audioManager;
    [SerializeField] private AttemptsController _attemptsController;
    [SerializeField] private AttemptsView _attemptsView;
    [SerializeField] private CollectedPrizesController _collectedPrizesController;
    [SerializeField] private CollectedPrizesView _collectedPrizesView;
    [SerializeField] private CurrencyManager _coinsManager;
    [SerializeField] private CurrencyManager _gemsManager;
    [SerializeField] private CurrencyView _coinsView;
    [SerializeField] private CurrencyView _gemsView;
    [SerializeField] private LootChest _lootChest;
    [SerializeField] private Item[] _prizes;

    private void Start()
    {
        SubscribeToEvents();
        InitializeCurrency();
    }
    
    private void SubscribeToEvents()
    {
        _attemptsController.AttemptsCountChanged += OnAttemptsChanged;
        foreach (var prize in _prizes)
        {
            if (prize != null)
                prize.ItemCollected += _collectedPrizesController.SetPrizeCount;
        }
        _collectedPrizesController.PrizeCountChanged += _collectedPrizesView.UpdatePrizesView;
        _coinsManager.ScoreChangedEvent += _coinsView.UpdateViewGradually;
        _gemsManager.ScoreChangedEvent += _gemsView.UpdateViewGradually;
        _lootChest.PrizesCollected += OnPrizesCollected;
    }

    private void InitializeCurrency()
    {
        _coinsManager.Initialize();
        _gemsManager.Initialize();
    }

    private void OnAttemptsChanged(int attempts)
    {
        _attemptsView.UpdateAttempts(attempts);
    }
    
    private void OnPrizesCollected()
    {
        _coinsManager.SetScore(_collectedPrizesController.Coins);
        _gemsManager.SetScore(_collectedPrizesController.Gems);
    }

    private void UnsubscribeFromEvents()
    {
        _attemptsController.AttemptsCountChanged -= OnAttemptsChanged;
        foreach (var prize in _prizes)
        {
            if (prize != null)
                prize.ItemCollected -= _collectedPrizesController.SetPrizeCount;
        }
        _collectedPrizesController.PrizeCountChanged -= _collectedPrizesView.UpdatePrizesView;
        _coinsManager.ScoreChangedEvent -= _coinsView.UpdateViewGradually;
        _gemsManager.ScoreChangedEvent -= _gemsView.UpdateViewGradually;
        _lootChest.PrizesCollected -= OnPrizesCollected;
    }
    
    private void OnDestroy()
    {
        UnsubscribeFromEvents();
    }
    
    public void OpenLobbyScreen()
    {
        SceneManager.LoadScene(GlobalConstants.LOBBY_SCENE);
    }
}
