using Currency;
using Managers;
using UnityEngine;


public class LuckySpinScreenManager : MonoBehaviour
{
    [SerializeField] private AudioManager _audioManager;
    [SerializeField] 
    private AttemptsController _attemptsController;
    [SerializeField] 
    private AttemptsView _attemptsView;
    [SerializeField] 
    private CollectedPrizesController _collectedPrizesController;
    [SerializeField] 
    private CollectedPrizesView _collectedPrizesView;

    [SerializeField] 
    private CurrencyManager _coins;
    [SerializeField] 
    private CurrencyManager _gems;
    [SerializeField] 
    private CurrencyView _coinsView;
    [SerializeField] 
    private CurrencyView _gemsView;
    
    [SerializeField] 
    private LootChest _lootChest;
    [SerializeField] 
    private Item[] _prizes;

    private void Awake()
    {
        // При изменении количества попыток спинов - отображаем это на UI и проигрываем анимацию
        _attemptsController.AttemptsCountChanged += OnAttemptsChanged;
        // Подписываемся на событие подбора каждого приза изменением его количества у контроллера собранных призов
        foreach (var prize in _prizes)
        {
            prize.ItemCollected += _collectedPrizesController.SetPrizeCount;
        }

        // При изменении количества призов в контроллере - изменяем это и на UI
        _collectedPrizesController.PrizeCountChanged += _collectedPrizesView.UpdatePrizesView;
        
        // // При изменении количества моент/алмазов - изменяем это и на UI
        _coins.ScoreChangedEvent += _coinsView.UpdateViewGradually;
        _gems.ScoreChangedEvent += _gemsView.UpdateViewGradually;
        _coins.Initialize();
        _gems.Initialize();
        
        // Когда сундук собрал все призы - добавляем к счету количество собранных монет и алмазов
        _lootChest.PrizesCollected += OnPrizesCollected;
    }
    
    private void OnAttemptsChanged(int attempts)
    {
        _attemptsView.UpdateAttempts(attempts);
    }
    
    private void OnPrizesCollected()
    {
        _coins.SetScore(_collectedPrizesController.Coins);
        _gems.SetScore(_collectedPrizesController.Gems);
    }
    
    private void OnDestroy()
    {
        _attemptsController.AttemptsCountChanged -= OnAttemptsChanged;
        foreach (var prize in _prizes)
        {
            prize.ItemCollected -= _collectedPrizesController.SetPrizeCount;
        }

        _collectedPrizesController.PrizeCountChanged -= _collectedPrizesView.UpdatePrizesView;
        _coins.ScoreChangedEvent -= _coinsView.UpdateViewGradually;
        _gems.ScoreChangedEvent -= _gemsView.UpdateViewGradually;
        _lootChest.PrizesCollected -= OnPrizesCollected;
    }
}