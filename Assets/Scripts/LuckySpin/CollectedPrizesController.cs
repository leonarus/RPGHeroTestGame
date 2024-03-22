using System;
using System.Collections.Generic;
using UnityEngine;


public class CollectedPrizesController : MonoBehaviour
{
    public Action<PrizeType, int> PrizeCountChanged;

    public int Coins => _prizes[PrizeType.Coin];
    public int Gems => _prizes[PrizeType.Gem];

    // Словарь, хранящий количество выигранного приза по ключу - Тип приза
    private readonly Dictionary<PrizeType, int> _prizes = new()
    {
        { PrizeType.Coin, 0 },
        { PrizeType.Gem, 0 },
        { PrizeType.Heart, 0 },
        { PrizeType.Relic, 0 }
    };

    public void SetPrizeCount(PrizeType type, int value)
    {
        if (_prizes.ContainsKey(type))
        {
            _prizes[type] += value;
            PrizeCountChanged?.Invoke(type, _prizes[type]);
        }
    }
}