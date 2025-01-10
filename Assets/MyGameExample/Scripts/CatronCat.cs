using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatronCat : Enemy
{
    private Animator _animator;
    private Rigidbody _body;

    [SerializeField] private float _rotationSpeed = 360f; // �������� �������� (������� � �������)
    [SerializeField] private float _rotationDuration = 1f;

    public bool Rotation = false;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _body = GetComponent<Rigidbody>();
    }

    public override void TakeDamage()
    {
        _animator.enabled = false;

        if (Rotation == true)
            StartCoroutine(RotateAroundSelf());

        Debug.Log("����� � ������� CatCartoon");
        IsLive = false;
    }

    private IEnumerator RotateAroundSelf()
    {
        float elapsedTime = 0f;
        float totalRotation = 0f;

        while (elapsedTime < _rotationDuration)
        {
            float rotationStep = _rotationSpeed * Time.deltaTime;
            totalRotation += rotationStep;
            transform.Rotate(0f, rotationStep, 0f);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // ������������ ������� �� ������, ���� �� �� ����� 360 ��������
        float correction = 360f - (totalRotation % 360f);
        transform.Rotate(0f, correction, 0f);
    }
}
