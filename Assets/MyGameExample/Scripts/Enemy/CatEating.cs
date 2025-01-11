using UnityEngine;

public class CatEating : MonoBehaviour
{
    [SerializeField] private GameObject _door;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.TryGetComponent(out Fruit fruit))
        {
            _door.SetActive(false);
            Destroy(fruit.gameObject);
        }
    }
}
