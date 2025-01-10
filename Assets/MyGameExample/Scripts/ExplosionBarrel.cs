using UnityEngine;

public class ExplosionBarrel : MonoBehaviour
{
    [SerializeField] private float _explosionRadius = 5f;
    [SerializeField] private float _explosionForce = 500f;

    [SerializeField] private GameObject _expslosionParticle;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.GetComponent<Arrow>())
        {
            Explode();
        }
    }

    private void Explode()
    {
        // Получаем все объекты в радиусе взрыва
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, _explosionRadius);

        foreach (var hitCollider in hitColliders)
        {
            // Проверяем, есть ли у объекта компонент Box
            Box box = hitCollider.GetComponent<Box>();
            if (box != null)
            {
                Rigidbody rb = hitCollider.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.isKinematic = false;
                    _expslosionParticle.SetActive(true);
                    rb.AddExplosionForce(_explosionForce, transform.position, _explosionRadius);
                }
            }
        }

        // Уничтожаем бочку
        Destroy(gameObject);
    }
}
