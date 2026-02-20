using UnityEngine;

/// <summary>
/// Attach to the top-level block GameObject. Listens for hits from a BlockCollisionRelay
/// on a child (e.g. the deep Collision object) and handles health/destroy.
/// </summary>
public class BlockController : MonoBehaviour
{
    [SerializeField]
    int health = 1;

    BlockCollisionRelay _relay;

    void Start()
    {
        _relay = GetComponentInChildren<BlockCollisionRelay>();
        if (_relay != null)
            _relay.OnBlockHit += HandleHit;
    }

    void OnDestroy()
    {
        if (_relay != null)
            _relay.OnBlockHit -= HandleHit;
    }

    void HandleHit(Collision2D collision)
    {
        health--;
        if (health <= 0)
            Destroy(gameObject);
    }
}
