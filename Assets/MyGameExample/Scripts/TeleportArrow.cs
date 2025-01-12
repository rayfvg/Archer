using UnityEngine;

public class TeleportArrow : MonoBehaviour
{
    [SerializeField] private Transform positionOutput;

    public bool Horizontal = false;

    public AudioSource _tpSound;

    [System.Obsolete]
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Arrow>())
        {
            _tpSound.Play();
            other.transform.position = positionOutput.position;

            if (Horizontal)
                other.transform.rotation = Quaternion.EulerAngles(Vector3.right);
            if (!Horizontal)
            {

                other.transform.LookAt(Vector3.down * 200);
                other.GetComponent<Rigidbody>().velocity = Vector3.zero;
                other.GetComponent<Rigidbody>().AddForce(Vector3.down * 45, ForceMode.Impulse);


            }

        }
    }
}
