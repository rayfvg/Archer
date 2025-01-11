using UnityEngine;

public class HitAndEnable : MonoBehaviour
{
    public GameObject EnabledObj;

    public bool Enable = true;

    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.collider.GetComponent<Arrow>() != null)
        {
            if (Enable)
                EnabledObj.SetActive(true);

            if(Enable == false)
                EnabledObj.SetActive(false);
        }
           
    }
}
