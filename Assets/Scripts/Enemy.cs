using UnityEngine;

public class Enemy : MonoBehaviour
{
    public TargetColor type;
    private Rigidbody2D enemyRb;

    void Awake()
    {
        enemyRb = GetComponent<Rigidbody2D>();
    }

    void OnDisable()
    {
        Targets.Instance.RemoveTarget(this);
        enemyRb.linearVelocity = Vector3.zero;
    }

    public void OnHit()
    {
        gameObject.SetActive(false);
    }
}

public enum TargetColor
{
    Default,
    Red,
    Blue
}
