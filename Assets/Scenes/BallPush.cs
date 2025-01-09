using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallPush : MonoBehaviour
{
    public float Forse;
    public Rigidbody body;

    public Transform _startposition;

    public void Push()
    {
        body.isKinematic = false;
        body.AddForce(transform.forward * Forse);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Push();
        }
        if (Input.GetMouseButtonDown(1))
        {
            Restart();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Vector3 relativeVelocity = collision.relativeVelocity;
        float angle = Vector3.Angle(relativeVelocity, -collision.contacts[0].normal);
        Debug.Log("Angle: " + angle);
    }

    public void Restart()
    {
        body.isKinematic = true;
        transform.position = _startposition.position;
    }
}
