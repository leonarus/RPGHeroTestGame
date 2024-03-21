using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
/// <summary>
/// Класс, отвечающий за хранение и изменение количества попыток спинов
/// </summary>
public class AttemptsController : MonoBehaviour
{
    public Action<int> AttemptsCountChanged;
    public UnityEvent TicketsOver;
    public UnityEvent AttemptsOver;
    
    [SerializeField]
    private int _currentValue = 3;

    public void AddAttempt()
    {
        _currentValue++;
        AttemptsCountChanged?.Invoke(_currentValue);
    }

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
            TicketsOver.Invoke();
            StartCoroutine(InvokeAttemptsOver());
        }
    }
    
    IEnumerator InvokeAttemptsOver()
    {
        // Ждет 5 секунд перед вызовом события
        yield return new WaitForSeconds(4.2f);
        AttemptsOver?.Invoke();
    }
}