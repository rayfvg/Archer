using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public bool IsLive = true;
    public abstract void TakeDamage();
}
