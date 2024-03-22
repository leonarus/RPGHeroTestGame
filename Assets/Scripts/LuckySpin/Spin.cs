using System.Collections;
using Managers;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;


public class Spin : MonoBehaviour
{
    public UnityEvent RotationStopped;

    [SerializeField] private AttemptsView _attemptsView;
    [SerializeField] private AudioManager _audioManager;
    [SerializeField] private float animationDuration = 1f;
    
    [SerializeField] private AudioSource _spinSound;
    [SerializeField] private float _minPitch = 0.35f;
    private float _rotationTime = 2f; // Общее время вращения в секундах
    private float _minSpeed = 700f; // Минимальная скорость вращения
    private float _maxSpeed = 900f; // Максимальная скорость вращения

    private float _currentRotationTime;
    private float _currentSpeed;

    public void Rotate()
    {
        StopAllCoroutines();
        StartCoroutine(WaitAndRotate());
    }

    private void StartRotation()
    {
        InitializeRotation();
        StartCoroutine(RotateForTime());
        _spinSound.Play();
    }
    
    private IEnumerator WaitAndRotate()
    {
        _attemptsView.PlayTicketAnimation(); 
        _audioManager.PlayInsertTicketSound();
        yield return new WaitForSeconds(animationDuration); 
        
        StartRotation();
    }

    private void InitializeRotation()
    {
        // Генерируем случайное время и скорость вращения
        _currentRotationTime = Random.Range(_rotationTime * 0.5f, _rotationTime);
        _currentSpeed = Random.Range(_minSpeed, _maxSpeed);
    }

    private IEnumerator RotateForTime()
    {
        var maxSpeed = _currentSpeed;
        // Вращаем вокруг оси в течение заданного времени
        var elapsedTime = 0f;
        while (elapsedTime < _currentRotationTime)
        {
            transform.Rotate(Vector3.back, _currentSpeed * Time.deltaTime);
            _currentSpeed = Mathf.Lerp(maxSpeed, 0,  elapsedTime / _currentRotationTime);
            SetSoundPitch();

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        
        RotationStopped.Invoke();
        _spinSound.Stop();
    }
    
    private void SetSoundPitch()
    {
        // Преобразуем скорость вращения колеса в высоту тона звука
        var pitch = Mathf.Max(_currentSpeed / _maxSpeed, _minPitch);
        // Применяем вычисленную высоту тона к источнику звука
        _spinSound.pitch = pitch;
    }
}