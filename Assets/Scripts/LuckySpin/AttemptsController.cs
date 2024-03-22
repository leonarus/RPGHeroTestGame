using System;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;


public class AttemptsController : MonoBehaviour
{
    public Action<int> AttemptsCountChanged;
    [UsedImplicitly]
    public UnityEvent AttemptsOver;
    
    [SerializeField]
    private int _currentValue = 3;

    public void DecreaseAttemptsCount()
    {
        if (_currentValue < 0)
        {
            return;
        }
        
        // Уменьшаем количество попыток и вызываем событие изменения количества
        _currentValue--;
        AttemptsCountChanged?.Invoke(_currentValue);

        // Если попытки закончились - вызываем событие окончания попыток
        if (_currentValue <= 0)
        {
            AttemptsOver?.Invoke();
        }
    }
}