using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private SpawnArea spawnArea;
    [SerializeField] private Pools enemyPools;

    [Space]
    [SerializeField] private float spawnDelay = 0.5f;

    [Header("Split Management")]
    [SerializeField] private List<SplitData> splitConfigs;

    void OnEnable()
    {
        GameEvents.OnEnemyDeath += HandleSplit;
    }

    void OnDisable()
    {
        GameEvents.OnEnemyDeath -= HandleSplit;
    }

    void Start()
    {
        StartCoroutine(EnemySpawner());
    }

    private IEnumerator EnemySpawner()
    {
        while ((GameManager.Instance.State != GameState.Playing))
            yield return null;

        while (GameManager.Instance.State == GameState.Playing)
        {
            GameObject spawnObj = null;
            while (spawnObj == null)
            {
                Vector3 spawnPos = spawnArea.GetRandomPoint();
                spawnObj = enemyPools.GetRandomPool().GetFromPool(spawnPos);

                if (spawnObj == null)
                    yield return null; // If pool is empty, wait for next frame
            }

            yield return new WaitForSeconds(spawnDelay);
        }
    }

    private void HandleSplit(Faction faction, Shape shape, Vector3 splitPos)
    {
        if (shape == Shape.Circle) return;

        SplitData config = splitConfigs.Find(s => s.parentShape == shape);

        if (config == null || config.splitResults.Count == 0)
        {
            Debug.Log("No split config for shape '" + shape.ToString() + "' has been added");
            return;
        }

        foreach (Shape resultShape in config.splitResults)
        {
            ObjectPooler pool = enemyPools.GetPool(faction, resultShape);
            Vector3 spawnPos = splitPos + new Vector3(0, 0.5f, 0);
            GameObject newObj = pool.GetFromPool(spawnPos);
            Targets.Instance.AddTarget(newObj.GetComponent<Enemy>());

            Rigidbody2D newObjRb = newObj.GetComponent<Rigidbody2D>();
            Vector2 forceDir = new(Random.Range(-1f, 1f), 1f);
            float forcePower = Random.Range(4f, 6f);

            newObjRb.AddForce(forceDir.normalized * forcePower, ForceMode2D.Impulse);
        }
    }
}

[System.Serializable]
public class SplitData
{
    public Shape parentShape;
    public List<Shape> splitResults;
}
