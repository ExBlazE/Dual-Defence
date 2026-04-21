using UnityEngine;

public class SpawnArea : MonoBehaviour
{
    private BoxCollider2D areaCollider;

    void Awake()
    {
        areaCollider = GetComponent<BoxCollider2D>();
    }

    public Vector3 GetRandomPoint()
    {
        Bounds bounds = areaCollider.bounds;

        float randomX = Random.Range(bounds.min.x, bounds.max.x);
        float randomY = Random.Range(bounds.min.y, bounds.max.y);

        return new Vector3(randomX, randomY, 0f);
    }
}
