using UnityEngine;

public class BowAiming : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private Transform _targetPoint;
    [SerializeField] private float minAngle = -45f; // ћинимальный угол поворота (в градусах)
    [SerializeField] private float maxAngle = 45f;  // ћаксимальный угол поворота (в градусах)
    [SerializeField] private float minMouseDistance = 1f; // ћинимальное рассто€ние между мышью и персонажем дл€ поворота

    private Camera _mainCamera; //  амера, котора€ будет использоватьс€ дл€ получени€ положени€ мыши

    public void InitCamera(Camera camera) => _mainCamera = camera;

    void Update()
    {
        // ѕолучаем мировую позицию мыши
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Mathf.Abs(_mainCamera.transform.position.z - _player.position.z); // √лубина камеры
        Vector3 worldMousePosition = _mainCamera.ScreenToWorldPoint(mousePosition);

        // ¬ычисл€ем направление от точки на луке к мыши
        Vector3 direction = worldMousePosition - _targetPoint.position;
        direction.z = 0; // ћы работаем в 2D, поэтому убираем изменение по оси Z

        // ѕровер€ем рассто€ние между мышью и точкой на луке
        if (direction.magnitude < minMouseDistance)
        {
            return; // ≈сли мышь слишком близко, выходим из метода, чтобы избежать дрожани€
        }

        // ¬ычисл€ем угол, на который нужно повернуть лук
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // ќграничиваем угол в пределах заданного диапазона
        angle = Mathf.Clamp(angle, minAngle, maxAngle);

        // ѕоворачиваем лук, чтобы точка на нем смотрела на мышь
        _player.rotation = Quaternion.Euler(0f, 0f, angle);
    }
}

