using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private SpawnArea spawnArea;
    [SerializeField] private ObjectPooler enemyPool;

    [Space]
    [SerializeField] private float spawnDelay = 0.5f;

    void Start()
    {
        StartCoroutine(EnemySpawner());
    }

    private IEnumerator EnemySpawner()
    {
        while(true)
        {
            GameObject spawnObj = null;
            while (spawnObj == null)
            {
                Vector3 spawnPos = spawnArea.GetRandomPoint();
                spawnObj = enemyPool.GetRandomFromPool(spawnPos, Quaternion.identity);

                if (spawnObj == null)
                    yield return null; // If pool is empty, wait for next frame
            }

            yield return new WaitForSeconds(spawnDelay);
        }
    }
}
