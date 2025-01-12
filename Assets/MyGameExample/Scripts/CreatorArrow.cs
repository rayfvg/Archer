using System.Collections.Generic;
using UnityEngine;

public class CreatorArrow : MonoBehaviour
{
    [SerializeField] private Arrow _arrowPrefab;
    [SerializeField] private Transform _positionForArrow;

    public Arrow Arrow;

    private bool _readyToShoot = false;
    private AudioSource _shotSound;

    private AudioSource _ricoshet;
    private AudioSource _shotInWall;
    private AudioSource _damage;

    public void CreateArrow()
    {
        if (Arrow != null || _readyToShoot)
            return;

        Arrow = Instantiate(_arrowPrefab, _positionForArrow.position, _positionForArrow.rotation, _positionForArrow);
        Arrow.InitArrow(_ricoshet, _shotInWall, _damage);
    }

    public void ShotArrow()
    {
        if (_readyToShoot && Arrow != null)
        {
            Arrow.Shoot();
            _shotSound.Play();
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

    public void InitSound(AudioSource shotSound, AudioSource ricoshet, AudioSource shotInWall, AudioSource damage)
    {
        _shotSound = shotSound;
        _shotInWall = shotInWall;
        _ricoshet = ricoshet;
        _damage = damage;
    }
}
