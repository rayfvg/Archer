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


    public int Lvlid;

    public void Initialize(PlayerView playerView, GameObject winnerLable, ParticleSystem winnerParticle)
    {
        _playerView = playerView;
        _winnerLable = winnerLable;
        _winnerParticle = winnerParticle;
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
        _work = false; // ������������� ������, ����� �� ���������� ������ ��������� ���
        yield return new WaitForSeconds(0.5f); // ��������
        Winners();
    }

    public void Winners()
    {
        Debug.Log("Winner");
        PlayerPrefs.SetInt("lvl" + Lvlid, 1);
        WinnerWhisLvl = false;
        _playerView.WinnerAnim();

        Instantiate(_winnerParticle, transform.position, Quaternion.identity, transform);

        _winnerLable.SetActive(true);
    }
}
