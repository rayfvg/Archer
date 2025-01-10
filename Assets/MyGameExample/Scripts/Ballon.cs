using UnityEngine;

public class Ballon : MonoBehaviour
{
    [SerializeField] private GameObject _particlePrefab;
    [SerializeField] private GameObject _door;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.GetComponent<Arrow>())
        {
            // ���������� �������� �� ��������
            _particlePrefab.transform.parent = null;

            // ���������� ��������
            _particlePrefab.SetActive(true);
            _door.SetActive(false);

            // ���������� �����
            Destroy(gameObject);

            // ���������� �������� ����� �������� ����� (��������, 5 ������)
            Destroy(_particlePrefab, 5f);
        }

    }
}
