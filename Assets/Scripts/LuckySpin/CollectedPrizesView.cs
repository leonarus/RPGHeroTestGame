using System.Collections.Generic;
using TMPro;
using UnityEngine;
/// <summary>
/// Класс, отвечающий за отображение количества выигранных призов на экране
/// </summary>
public class CollectedPrizesView : MonoBehaviour
{
    [SerializeField] 
    private TextMeshProUGUI _coinsLabel;
    [SerializeField] 
    private TextMeshProUGUI _gemsLabel;
    [SerializeField] 
    private TextMeshProUGUI _heartsLabel;
    [SerializeField] 
    private TextMeshProUGUI _relicsLabel;

    // Словарь, хранящий текст с количеством выигранного приза по ключу - Тип приза
    private Dictionary<PrizeType, TextMeshProUGUI> _prizeLabels;

    private void Awake()
    {
        _prizeLabels = new Dictionary<PrizeType, TextMeshProUGUI>
        {
            { PrizeType.Coin, _coinsLabel },
            { PrizeType.Gem, _gemsLabel },
            { PrizeType.Heart, _heartsLabel },
            { PrizeType.Relic, _relicsLabel }
        };
    }

    public void UpdatePrizesView(PrizeType type, int value)
    {
        if (_prizeLabels.TryGetValue(type, out var label))
        {
            label.text = $"x {value}";
        }
    }
}