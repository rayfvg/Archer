using System.Collections.Generic;
using UnityEngine;

public class CreatorArrow : MonoBehaviour
{
    [SerializeField] private Arrow _arrowPrefab;
    [SerializeField] private Transform _positionForArrow;

    public Arrow Arrow;

    private bool _readyToShoot = false;
    public void CreateArrow()
    {
        if (Arrow != null || _readyToShoot)
            return;

        Arrow = Instantiate(_arrowPrefab, _positionForArrow.position, _positionForArrow.rotation, _positionForArrow);
    }

    public void ShotArrow()
    {
        if (_readyToShoot && Arrow != null)
        {
            Arrow.Shoot();
            Arrow = null;
            _readyToShoot = false;
        }
    }

    public void DestroyArrow()
    {
        if (Arrow != null)
        {
            Destroy(Arrow.gameObject);
            Arrow = null;
            _readyToShoot = false;
        }
    }

    public void ReadyToShoot() => _readyToShoot = true;
}
