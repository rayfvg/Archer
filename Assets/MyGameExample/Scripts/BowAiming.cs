using UnityEngine;

public class BowAiming : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private Transform _targetPoint;
    [SerializeField] private float minAngle = -45f; // ����������� ���� �������� (� ��������)
    [SerializeField] private float maxAngle = 45f;  // ������������ ���� �������� (� ��������)
    [SerializeField] private float minMouseDistance = 1f; // ����������� ���������� ����� ����� � ���������� ��� ��������

    private Camera _mainCamera; // ������, ������� ����� �������������� ��� ��������� ��������� ����

    public void InitCamera(Camera camera) => _mainCamera = camera;

    void Update()
    {
        // �������� ������� ������� ����
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Mathf.Abs(_mainCamera.transform.position.z - _player.position.z); // ������� ������
        Vector3 worldMousePosition = _mainCamera.ScreenToWorldPoint(mousePosition);

        // ��������� ����������� �� ����� �� ���� � ����
        Vector3 direction = worldMousePosition - _targetPoint.position;
        direction.z = 0; // �� �������� � 2D, ������� ������� ��������� �� ��� Z

        // ��������� ���������� ����� ����� � ������ �� ����
        if (direction.magnitude < minMouseDistance)
        {
            return; // ���� ���� ������� ������, ������� �� ������, ����� �������� ��������
        }

        // ��������� ����, �� ������� ����� ��������� ���
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // ������������ ���� � �������� ��������� ���������
        angle = Mathf.Clamp(angle, minAngle, maxAngle);

        // ������������ ���, ����� ����� �� ��� �������� �� ����
        _player.rotation = Quaternion.Euler(0f, 0f, angle);
    }
}

