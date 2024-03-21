using JetBrains.Annotations;
using UnityEngine;

/// <summary>
/// Класс стрелки рулетки, подбирающей выпавший приз
/// </summary>
public class PrizeSelector : MonoBehaviour
{
    private PolygonCollider2D _selectorCollider;

    private void Awake()
    {
        _selectorCollider = GetComponent<PolygonCollider2D>();
    }

    // Метод вызывается при остановке вращения рулетки
    [UsedImplicitly]
    public void SelectPrize()
    {
        // Создаем массив для хранения результатов рейкаста.
        var hits = new RaycastHit2D[10];
        // Стреляем лучом из стрелки
        var hitCount = _selectorCollider.Raycast(transform.position, hits);
        
        // Если столкновения были
        if (hitCount > 0)
        {
            // Получаем ближайший объект
            var hit = hits[0];

            if (hit.collider.gameObject.TryGetComponent(out Item prize))
            {
                // Берем приз
                prize.Pick();
            }
        }
    }
}
