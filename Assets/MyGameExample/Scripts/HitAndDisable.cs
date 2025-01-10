using UnityEngine;

public class HitAndDisable : MonoBehaviour
{
    public GameObject EnabledObj;

    public GameObject[] Coins;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.GetComponent<Arrow>() != null)
        {
            EnabledObj.SetActive(false);

            foreach (var coin in Coins)
            {
                coin.GetComponent<Rigidbody>().isKinematic = false;
                coin.GetComponent<Rotator>().enabled = false;
            }
        }
            
    }
}
