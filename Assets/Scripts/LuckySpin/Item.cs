using System;
using UnityEngine;


public class Item : MonoBehaviour
{
    public Action<PrizeType, int> ItemCollected;
    [SerializeField] 
    private PrizeType _type;
    [SerializeField] 
    private int _value;
    [SerializeField] 
    private Popup _popup;

    // При подборе вызывает событие со своим типом и значением, а также показывает окошко с выигрышем 
    public void Pick()
    {
        ItemCollected?.Invoke(_type, _value);
        
        // Показываем окошко с выигрышем
        _popup.gameObject.SetActive(true);
    }
}