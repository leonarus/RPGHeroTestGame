using UnityEngine;

public class ImageScaler : MonoBehaviour
{
    private Vector3 _initial;
    private void Awake()
    {
        _initial = transform.position;
    }

    public void ResetScale()
    {
        transform.position = _initial;
        transform.localScale = new Vector3(4.8f, 3.7f, 3.7f);
    }
}