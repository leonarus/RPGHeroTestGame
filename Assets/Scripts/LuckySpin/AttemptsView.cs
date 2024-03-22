using TMPro;
using UnityEngine;


public class AttemptsView : MonoBehaviour
{
    private static readonly int Ticket = Animator.StringToHash("ticket");

    [SerializeField] 
    private TextMeshProUGUI _countLabel;
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void UpdateAttempts(int value)
    {
        _countLabel.text = $"x {value}";
    }
    
    public void PlayTicketAnimation()
    {
        _animator.SetTrigger(Ticket);
    }
}