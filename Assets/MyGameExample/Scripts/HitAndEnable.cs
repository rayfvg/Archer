using UnityEngine;

public class HitAndEnable : MonoBehaviour
{
    public GameObject[] EnabledObjs;

    public bool Enable = true;

    public AudioSource ClickAudio;

    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.collider.GetComponent<Arrow>() != null)
        {
            ClickAudio.Play();

            if (Enable)
            {
                foreach (var obj in EnabledObjs)
                {
                    obj.SetActive(true);
                }
            }
                

            if(Enable == false)
            {
                foreach (var obj in EnabledObjs)
                {
                    obj.SetActive(false);
                }
            }
                
        }
           
    }
}
