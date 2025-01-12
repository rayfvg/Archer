using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishRules : MonoBehaviour
{
    public List<Enemy> enemies;

    public bool WinnerWhisLvl = false;

    private bool _work = true;

    private PlayerView _playerView;

    private GameObject _winnerLable;

    private ParticleSystem _winnerParticle;
    private AudioSource _victorySound;

    public int Lvlid;

    public void Initialize(PlayerView playerView, GameObject winnerLable, ParticleSystem winnerParticle, AudioSource victorySound)
    {
        _playerView = playerView;
        _winnerLable = winnerLable;
        _winnerParticle = winnerParticle;
        _victorySound = victorySound;
    }

    private void Update()
    {
        if (_work == false)
            return;

        if (WinnerWhisLvl)
        {
            StartCoroutine(DelayedWinners());
            return;
        }

        foreach (Enemy enemy in enemies)
        {
            if (enemy.IsLive == true)
                return;
        }

        WinnerWhisLvl = true;
    }

    private IEnumerator DelayedWinners()
    {
        _work = false; // Останавливаем работу, чтобы не вызывалась победа несколько раз
        yield return new WaitForSeconds(0.5f); // Задержка
        Winners();

    }

    public void Winners()
    {

        Debug.Log("Winner");
        PlayerPrefs.SetInt("lvl" + Lvlid, 1);
        WinnerWhisLvl = false;
        _playerView.WinnerAnim();

        Instantiate(_winnerParticle, transform.position, Quaternion.identity, transform);

        _victorySound.Play();
        _winnerLable.SetActive(true);
    }
}
