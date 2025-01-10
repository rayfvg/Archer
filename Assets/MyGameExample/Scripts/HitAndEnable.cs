using UnityEngine;

public class HitAndEnable : MonoBehaviour
{
    public GameObject EnabledObj;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.GetComponent<Arrow>() != null)
            EnabledObj.SetActive(true);
    }
}
