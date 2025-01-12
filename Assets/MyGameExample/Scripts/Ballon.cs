using UnityEngine;

public class Ballon : MonoBehaviour
{
    [SerializeField] private GameObject _particlePrefab;
    [SerializeField] private GameObject _door;

    [SerializeField] private AudioSource _expsBallons;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.GetComponent<Arrow>())
        {
            _expsBallons.Play();
            Booom();
        }

    }

    private void Booom()
    {
        // ���������� �������� �� ��������
        _particlePrefab.transform.parent = null;

        // ���������� ��������
        _particlePrefab.SetActive(true);
        if (_door != null)
            _door.SetActive(false);

        // ���������� �����
        Destroy(gameObject);

        // ���������� �������� ����� �������� ����� (��������, 5 ������)
        Destroy(_particlePrefab, 5f);
    }


}
