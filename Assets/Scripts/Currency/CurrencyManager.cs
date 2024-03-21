using System;
using UnityEngine;

namespace Currency
{
    public class CurrencyManager : MonoBehaviour
    {
        public Action<int, int> ScoreChangedEvent;
        [field: SerializeField]
        public int Value { get; private set; } = 50000;
        
        public void Initialize()
        {
            ScoreChangedEvent?.Invoke(Value, Value);
        }
        
        public void SetScore(int value)
        {
            var currentScore = Value;
            Value += value;
            ScoreChangedEvent?.Invoke(currentScore, Value);
        }

        public bool TryBuy(int price)
        {
            if (Value >= price)
            {
                Value -= price;
                ScoreChangedEvent?.Invoke(Value, Value);
                return true;
            }

            return false;
        }
    }
}