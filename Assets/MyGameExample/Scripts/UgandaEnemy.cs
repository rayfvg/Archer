using UnityEngine;

public class UgandaEnemy : Enemy
{
    private Animator _animator;
    private Rigidbody _body;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _body = GetComponent<Rigidbody>();
    }

    public override void TakeDamage()
    {
        _animator.enabled = false;
        Debug.Log("Попал в спрунки Oren");
        IsLive = false;
    }
}