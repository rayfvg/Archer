using UnityEngine;

public class Frog : Enemy
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public override void TakeDamage()
    {
        _animator.enabled = false;
        Debug.Log("����� � �������");
        IsLive = false;
    }
}