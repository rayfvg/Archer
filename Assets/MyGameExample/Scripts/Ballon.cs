using UnityEngine;

public class Ballon : MonoBehaviour
{
    [SerializeField] private GameObject _particlePrefab;
    [SerializeField] private GameObject _door;

    [SerializeField] private AudioSource _expsBallons;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.GetComponent<Arrow>())
        {
            _expsBallons.Play();
            Booom();
        }

    }

    private void Booom()
    {
        // Отвязываем партиклы от иерархии
        _particlePrefab.transform.parent = null;

        // Активируем партиклы
        _particlePrefab.SetActive(true);
        if (_door != null)
            _door.SetActive(false);

        // Уничтожаем шарик
        Destroy(gameObject);

        // Уничтожаем партиклы через заданное время (например, 5 секунд)
        Destroy(_particlePrefab, 5f);
    }


}
