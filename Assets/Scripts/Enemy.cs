using UnityEngine;

public class Enemy : MonoBehaviour
{
    public TargetType type;
    private Rigidbody2D enemyRb;

    void Awake()
    {
        enemyRb = GetComponent<Rigidbody2D>();
    }

    void OnDisable()
    {
        enemyRb.linearVelocity = Vector3.zero;
    }

    public void OnHit()
    {
        Targets.Instance.RemoveTarget(this);
        gameObject.SetActive(false);
    }
}

public enum TargetType
{
    Default,
    Red,
    Blue
}
