using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;
/// <summary>
/// Класс, отвечающий за вращение рулетки
/// </summary>
public class Spin : MonoBehaviour
{
    public UnityEvent RotationStopped;

    private float _rotationTime = 2f; // Общее время вращения в секундах
    private float _minSpeed = 700f; // Минимальная скорость вращения
    private float _maxSpeed = 900f; // Максимальная скорость вращения

    private float _currentRotationTime;
    private float _currentSpeed;

    public void Rotate()
    {
        StopAllCoroutines();
        StartRotation();
    }

    private void StartRotation()
    {
        InitializeRotation();
        // Начинаем вращение
        StartCoroutine(RotateForTime());
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
            
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        
        RotationStopped.Invoke();
    }
}