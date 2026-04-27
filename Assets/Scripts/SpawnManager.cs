using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private SpawnArea spawnArea;
    [SerializeField] private Pools enemyPools;

    [Header("Spawn Settings")]
    [SerializeField] private float minSpawnDelay = 0.5f;
    [SerializeField] private float maxSpawnDelay = 2f;
    [SerializeField] private float delayReduction = 0.1f;
    [SerializeField] private float reductionTime = 12f;
    [SerializeField] private float stageTime = 20f;

    [Header("Spawn Stage Settings")]
    [SerializeField] private List<SpawnStage> progression;

    [Header("Split Management")]
    [SerializeField] private List<SplitData> splitConfigs;

    private float currentSpawnDelay;
    private int currentStageIndex;

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
        ResetSpawn();
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

                Faction randomFaction = Random.value < 0.5f ? Faction.Red : Faction.Blue;
                Shape randomShape = GetRandomShape();
                ObjectPooler pool = enemyPools.GetPool(randomFaction, randomShape);

                spawnObj = pool.GetFromPool(spawnPos);

                if (spawnObj == null)
                    yield return null; // If pool is empty, wait for next frame
            }

            yield return new WaitForSeconds(currentSpawnDelay);
        }
    }

    private IEnumerator DifficultyManager()
    {
        while (GameManager.Instance.State != GameState.Playing)
            yield return null;

        float reductionTimer = 0f;
        float stageTimer = 0f;

        while (GameManager.Instance.State == GameState.Playing)
        {
            reductionTimer += Time.deltaTime;
            stageTimer += Time.deltaTime;

            if (reductionTimer >= reductionTime)
            {
                currentSpawnDelay = Mathf.Max(minSpawnDelay, currentSpawnDelay - delayReduction);
                reductionTimer = 0f;
            }
            if (stageTimer >= stageTime)
            {
                AdvanceStage();
                stageTimer = 0f;
            }

            if (CheckMaxDifficulty())
                yield break;

            yield return null;
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

    private void AdvanceStage()
    {
        if (currentStageIndex < progression.Count - 1)
            currentStageIndex++;
    }

    private Shape GetRandomShape()
    {
        float roll = Random.value;
        float cumulativeWeight = 0f;

        foreach (var entry in progression[currentStageIndex].weights)
        {
            cumulativeWeight += entry.weight;
            if (roll <= cumulativeWeight) return entry.shape;
        }

        return Shape.Circle;
    }

    private bool CheckMaxDifficulty()
    {
        return (currentSpawnDelay == minSpawnDelay) && (currentStageIndex == progression.Count - 1);
    }

    private void ResetSpawn()
    {
        currentSpawnDelay = maxSpawnDelay;
        currentStageIndex = 0;
        StartCoroutine(EnemySpawner());
        StartCoroutine(DifficultyManager());
    }
}

[System.Serializable]
public class SplitData
{
    public Shape parentShape;
    public List<Shape> splitResults;
}

[System.Serializable]
public class SpawnWeight
{
    public Shape shape;
    public float weight;
}

[System.Serializable]
public class SpawnStage
{
    public List<SpawnWeight> weights;
}
