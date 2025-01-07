using System.Collections;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public Transform DotForTarget;

    [SerializeField] private float _forse;
    [SerializeField] private Rigidbody _rigidbody;

    [SerializeField] private float arrowEmbed = 0.01f;

    private bool _embedded = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (_embedded)
            return;

        Vector3 collisionNormal = collision.contacts.Length > 0
            ? collision.contacts[0].normal
            : Vector3.up; // Подстраховка

        Vector3 arrowDirection = _rigidbody.velocity.normalized;

        float angle = Vector3.Angle(arrowDirection, -collisionNormal);

        Debug.Log($"Collision Normal: {collisionNormal}, Arrow Direction: {arrowDirection}, Angle: {angle}");

        // Проверяем, можно ли вонзиться
        if (angle < 89 || angle > 91)
        {
            EmbedArrow(collision);
            Debug.Log("Вонзил");
        }
        else if (collision.gameObject.CompareTag("Wall"))
        {
            ReflectArrow(collisionNormal);
        }
    }

    public void Shoot()
    {
        _rigidbody.isKinematic = false;
        transform.parent = null;
        _rigidbody.AddForce(transform.forward * _forse * Time.deltaTime, ForceMode.Impulse);
    }

    private void EmbedArrow(Collision collision)
    {
        _embedded = true;

        // Отключаем физику стрелы
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.isKinematic = true;

        // Получаем информацию о столкновении
        ContactPoint contact = collision.contacts[0];

        // Устанавливаем стрелу в точке столкновения с небольшим смещением
        transform.position = contact.point + contact.normal * arrowEmbed;

        // Поворачиваем стрелу в направлении нормали поверхности
        transform.rotation = Quaternion.LookRotation(-contact.normal);

        // Закрепляем стрелу в объекте, сохраняя мировую позицию
        transform.SetParent(collision.transform, worldPositionStays: true);

        Debug.Log($"Arrow embedded at {transform.position} with rotation {transform.rotation}");
    }



    private void ReflectArrow(Vector3 collisionNormal)
    {
        Vector3 newDirection = Vector3.Reflect(_rigidbody.velocity.normalized, collisionNormal);

        _rigidbody.velocity = newDirection * _forse * Time.deltaTime;

        if (newDirection != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(newDirection);
        }
    }
}