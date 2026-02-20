using System;
using UnityEngine;

/// <summary>
/// Attach to the GameObject that has the Collider2D (e.g. deep child "Collision" object).
/// Fires an event when a collision occurs so a parent BlockController can handle damage.
/// </summary>
public class BlockCollisionRelay : MonoBehaviour
{
    public event Action<Collision2D> OnBlockHit;

    void OnCollisionEnter2D(Collision2D collision)
    {
        OnBlockHit?.Invoke(collision);
    }
}
