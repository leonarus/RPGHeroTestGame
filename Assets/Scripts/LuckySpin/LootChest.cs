using System;
using JetBrains.Annotations;
using UnityEngine;

public class LootChest : MonoBehaviour
{
    public Action PrizesCollected;
    
    private static readonly int Put = Animator.StringToHash("put");
    private static readonly int Move = Animator.StringToHash("move");
    private static readonly int Open = Animator.StringToHash("open");
    private static readonly int Close = Animator.StringToHash("close");
    
    private Animator _animator;
    private bool _canMove;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void PlayPuttingAnimation()
    {
        _animator.SetTrigger(Put);
    }
    
    public void PlayMovingAnimation()
    {
        if (_canMove)
        {
            _animator.SetTrigger(Move);
        }
    }

    public void PlayOpeningAnimation()
    {
        _animator.SetTrigger(Open);
    }
    
    public void PlayClosingAnimation()
    {
        _animator.SetTrigger(Close);
        PrizesCollected?.Invoke();
    }

    [UsedImplicitly]
    public void ToggleMovement(bool canMove)
    {
        _canMove = canMove;
    }
}