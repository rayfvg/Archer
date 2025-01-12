using UnityEngine;

public class BreakBarrel : MonoBehaviour
{
    [SerializeField] private GameObject _ice;
    [SerializeField] private GameObject _expslosionParticle;

    public AudioSource ExplSound;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.GetComponent<Ice>())
        {
            ExplSound.Play();
            _expslosionParticle.SetActive(true);
            _ice.SetActive(false);
        }
    }
}
