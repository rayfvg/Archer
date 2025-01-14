using UnityEngine;

public class MemesSound : MonoBehaviour
{
    public AudioSource[] MemesSounds;

    private int _currentIndex = 0; // ��������� �� ������� ����
    private AudioSource[] _shuffledSounds; // ������������ ������ ������

    private void Start()
    {
        ShuffleSounds();
    }

    public void GetRandomMem()
    {
        if (_shuffledSounds == null || _shuffledSounds.Length == 0)
            return;

        // ������������� ������� ����
        _shuffledSounds[_currentIndex].Play();

        // ��������� � ���������� �����
        _currentIndex = (_currentIndex + 1) % _shuffledSounds.Length;
    }

    private void ShuffleSounds()
    {
        // ������ ����� ������� � ������������
        _shuffledSounds = (AudioSource[])MemesSounds.Clone();
        for (int i = _shuffledSounds.Length - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            // ������ �������
            var temp = _shuffledSounds[i];
            _shuffledSounds[i] = _shuffledSounds[randomIndex];
            _shuffledSounds[randomIndex] = temp;
        }
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

