using UnityEngine;

public class PlayerView : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private AudioSource _loadingArrow;
    private bool _isPreShooting; // ���� ��� ������������ ���������

    public void StartPreShoot()
    {
        if (!_isPreShooting) // ���� ����������� ������ ���� ���
        {
            _animator.SetBool("PreShoot", true);
            _loadingArrow.Play();
            _isPreShooting = true;
        }
    }

    public void StopPreShoot()
    {
        if (_isPreShooting) // ���� ��������������� ������ ���� �� ��� �������
        {
            _animator.SetBool("PreShoot", false);
            _loadingArrow.Stop();
            _isPreShooting = false;
        }
    }

    public void WinnerAnim() => _animator.SetTrigger("Win");

    public void InitSound(AudioSource loadingArrow) => _loadingArrow = loadingArrow;
}
