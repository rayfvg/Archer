using UnityEngine;

public class ButtonEnabletObj : MonoBehaviour
{
    [SerializeField] private GameObject _door;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.GetComponent<Box>())
        {
            _door.SetActive(false);
        }
    }
}
