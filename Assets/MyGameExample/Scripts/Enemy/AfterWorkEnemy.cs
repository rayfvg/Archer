using UnityEngine;

public class AfterWorkEnemy : Enemy
{
    private Animator _animator;
    private Rigidbody _rigitbody;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rigitbody = GetComponent<Rigidbody>();
    }

    public override void TakeDamage()
    {
        _animator.enabled = false;
        Debug.Log("Попал в Говно");

        _rigitbody.AddRelativeTorque(-Vector3.back * 5000, ForceMode.Impulse);
        IsLive = false;
    }
}
