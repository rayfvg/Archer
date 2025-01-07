using UnityEngine;

public class PlayerView : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    public void StartAnimation() => _animator.SetTrigger("Start");

    public void StartPreShoot() => _animator.SetBool("PreShoot", true);
    public void StopPreShoot() => _animator.SetBool("PreShoot", false);
}
