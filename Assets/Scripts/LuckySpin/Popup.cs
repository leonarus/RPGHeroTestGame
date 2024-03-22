using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;


public class Popup : MonoBehaviour
{
    public UnityEvent AnimationFinished;
    
    [UsedImplicitly]
    public void OnAnimationCompleted()
    {
        AnimationFinished.Invoke();
    }

    [UsedImplicitly]
    public void HidePopup()
    {
        gameObject.SetActive(false);
    }
}