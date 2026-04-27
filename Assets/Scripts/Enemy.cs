using UnityEngine;

public class Enemy : MonoBehaviour
{
    [field: SerializeField] public Faction Faction { get; private set; }
    [field: SerializeField] public Shape Shape { get; private set; }
    [field: SerializeField] public int Value { get; private set; }

    [Space]
    [Tooltip("In degrees per second")]
    [SerializeField] private float spinSpeed = 120f;
    [SerializeField] private float maxVelocity = 7.5f;

    private float maxVelocitySqr;
    private Rigidbody2D enemyRb;

    void Awake()
    {
        enemyRb = GetComponent<Rigidbody2D>();
        maxVelocitySqr = maxVelocity * maxVelocity;
    }

    void FixedUpdate()
    {
        if (enemyRb.linearVelocity.sqrMagnitude > maxVelocitySqr)
            enemyRb.linearVelocity = enemyRb.linearVelocity.normalized * maxVelocity;
    }

    void OnEnable()
    {
        enemyRb.angularVelocity = Random.value > 0.5f ? spinSpeed : -spinSpeed;
    }

    void OnDisable()
    {
        Targets.Instance.RemoveTarget(this);
        enemyRb.linearVelocity = Vector3.zero;
        enemyRb.angularVelocity = 0f;
    }

    public void OnHit()
    {
        gameObject.SetActive(false);
        GameEvents.RaiseEnemyDeath(Faction, Shape, transform.position);
    }

    public void Kill()
    { gameObject.SetActive(false); }
}
