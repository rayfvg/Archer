using UnityEngine;

public class PlayerView : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    public void StartPreShoot() => _animator.SetBool("PreShoot", true);
    public void StopPreShoot() => _animator.SetBool("PreShoot", false);

    public void WinnerAnim() => _animator.SetTrigger("Win");
}
