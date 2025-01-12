using UnityEngine;

public class ButtonEnabletObj : MonoBehaviour
{
    [SerializeField] private GameObject _doorEnable;
    [SerializeField] private GameObject _doorDisable;

    [SerializeField] private AudioSource _clickSound;

    private bool _isWas = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.GetComponent<Box>())
        {
            if (_isWas == false)
            {
                _clickSound.Play();
                _isWas = true;
            }

            if (_doorDisable != null)
                _doorDisable.SetActive(false);

            if (_doorEnable != null)
                _doorEnable.SetActive(true);
        }
    }
}
