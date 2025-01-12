using UnityEngine;

public class MemesSound : MonoBehaviour
{
    public AudioSource[] MemesSounds;

    public void GetRandomMem()
    {
        int random = Random.Range(0, MemesSounds.Length);
        MemesSounds[random].Play();
    }

   


    void OnApplicationFocus(bool hasFocus)
    {
        if (!hasFocus)
        {
            StopAllSouds();
        }
        else
        {
            PlayAllSounds();
        }
    }

    public void StopAllSouds()
    {
        AudioListener.pause = true;
    }

    public void PlayAllSounds()
    {
        AudioListener.pause = false;
    }
}
