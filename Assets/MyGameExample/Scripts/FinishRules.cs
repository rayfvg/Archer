using System.Collections.Generic;
using UnityEngine;

public class FinishRules : MonoBehaviour
{
    public List<Enemy> enemies;

    public bool WinnerWhisLvl = false;

    private bool _work = true;

    private PlayerView _playerView;

    private GameObject _winnerLable;

    public void Initialize(PlayerView playerView, GameObject winnerLable)
    {
        _playerView = playerView;
        _winnerLable = winnerLable;
    }

    private void Update()
    {
       if(_work == false)
            return;

        if (WinnerWhisLvl)
        {
            Winners();
        }
            
        foreach (Enemy enemy in enemies)
        {
            if (enemy.IsLive == true)
                return;
        }

        WinnerWhisLvl = true;
    }

    public void Winners()
    {
        Debug.Log("Winner");
        WinnerWhisLvl = false;
        _work = false;
        _playerView.WinnerAnim();
        _winnerLable.SetActive(true);
    }
}
