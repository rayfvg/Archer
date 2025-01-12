using UnityEngine;

public class CatEating : MonoBehaviour
{
    [SerializeField] private GameObject _door;

    [SerializeField] private AudioSource _eatSound;
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.TryGetComponent(out Fruit fruit))
        {
            _door.SetActive(false);
            _eatSound.Play();
            Destroy(fruit.gameObject);
        }
    }
}
