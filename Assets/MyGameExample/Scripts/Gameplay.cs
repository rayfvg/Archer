using TMPro;
using UnityEngine;

public class Gameplay : MonoBehaviour
{
    [SerializeField] private PlayerView _playerView;
    [SerializeField] private CreatorArrow _createArrow;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _createArrow.DestroyArrow();
        }

        if (Input.GetMouseButton(0))
        {
            _playerView.StartPreShoot();
        }

        if(Input.GetMouseButtonUp(0))
        {
            _createArrow.ShotArrow();
            _playerView.StopPreShoot();
        }
    }
}
