using UnityEngine;

public class ButtonEnabletObj : MonoBehaviour
{
    [SerializeField] private GameObject _doorEnable;
    [SerializeField] private GameObject _doorDisable;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.GetComponent<Box>())
        {
            if(_doorDisable != null)
            _doorDisable.SetActive(false);

            if(_doorEnable != null)
                _doorEnable.SetActive(true);
        }
    }
}
