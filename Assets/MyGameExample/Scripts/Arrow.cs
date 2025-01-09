using System.Collections;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public Transform DotForTarget;

    [SerializeField] private float _forse;
    [SerializeField] private float _forseBounse;
    [SerializeField] private Rigidbody _rigidbody;

    [SerializeField] private float arrowEmbed = 0.01f;

    [SerializeField] private float _lifeTime;

    private bool hasHitEnemy = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            ShootInWall(collision);
        }

        if(collision.collider.TryGetComponent(out Enemy enemy))
        {
            enemy.TakeDamage();

            hasHitEnemy = true; // Помечаем, что стрела попала во врага
            CancelInvoke("DestroyArrow");

            EmbedArrow(collision);
            GetComponent<Collider>().enabled = false;
        }
    }

    public void Shoot()
    {
        Invoke("DestroyArrow", 3f);

        _rigidbody.isKinematic = false;
        transform.parent = null;

        //transform.position = new Vector3(transform.position.x, transform.position.z, 0);
        _rigidbody.AddForce(transform.forward * _forse, ForceMode.Impulse);
    }

    public void EmbedArrow(Collision collision)
    {
        Debug.Log("проткнул");

        _rigidbody.velocity = Vector3.zero;
        _rigidbody.isKinematic = true;

        ContactPoint contact = collision.contacts[0];

        transform.position = contact.point + contact.normal * arrowEmbed;

        transform.rotation = Quaternion.LookRotation(-contact.normal);

        transform.SetParent(collision.transform);
    }

    public void ReflectArrow(Vector3 collisionNormal)
    {
        Vector3 newDirection = Vector3.Reflect(_rigidbody.velocity.normalized, collisionNormal);

        // Привести направление рикошета к ближайшей основной оси
        if (Mathf.Abs(newDirection.x) > Mathf.Abs(newDirection.y))
        {
            // Если движение больше по оси X
            newDirection = new Vector3(Mathf.Sign(newDirection.x), 0, 0);
        }
        else
        {
            // Если движение больше по оси Y
            newDirection = new Vector3(0, Mathf.Sign(newDirection.y), 0);
        }

        // Нормализовать направление (хотя уже приведено к единичным значениям, но для надежности)
        newDirection = newDirection.normalized;

        // Применить изменения к Rigidbody
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.AddForce(newDirection * _forseBounse, ForceMode.Impulse);

        // Повернуть объект, если направление не нулевое
        if (newDirection != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(new Vector3(newDirection.x, newDirection.y, 0));
        }

    }

    public void ShootInWall(Collision collision)
    {
        Debug.Log("Отскок");

        Vector3 relativeVelocity = collision.relativeVelocity;

        float angle = Vector3.Angle(relativeVelocity, -collision.contacts[0].normal);
        Debug.Log("Angle: " + angle);


        if (angle < 199 && angle > 166)
        {
            EmbedArrow(collision);
        }
        else
        {
            ReflectArrow(collision.contacts[0].normal);
        }
    }

    void DestroyArrow()
    {
        // Проверяем, была ли стрела отменена
        if (!hasHitEnemy)
        {
            Destroy(gameObject);
        }
    }
}