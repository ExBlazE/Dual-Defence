using System.Collections.Generic;
using UnityEngine;

public class Pools : MonoBehaviour
{
    [SerializeField] private List<ObjectPooler> allEnemyPools;

    private readonly Dictionary<PoolKey, ObjectPooler> poolDictionary = new();

    void Awake()
    {
        foreach (ObjectPooler pool in allEnemyPools)
        {
            TargetInfo info = pool.GetComponent<TargetInfo>();
            if (info != null)
            {
                PoolKey key = new(info.Faction, info.Shape);
                poolDictionary.Add(key, pool);
            }
        }
    }

    public ObjectPooler GetPool(Faction faction, Shape shape)
    {
        poolDictionary.TryGetValue(new PoolKey(faction, shape), out ObjectPooler pool);
        return pool;
    }

    public ObjectPooler GetRandomPool()
    {
        return allEnemyPools[Random.Range(0, allEnemyPools.Count)];
    }
}

public readonly struct PoolKey
{
    public readonly Faction faction;
    public readonly Shape shape;

    public PoolKey(Faction f, Shape s)
    {
        faction = f;
        shape = s;
    }
}
