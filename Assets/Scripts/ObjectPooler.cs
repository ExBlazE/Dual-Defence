using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public string poolName; // Only for naming in inspector

    [Space]
    [SerializeField] private GameObject objectToPool;
    [SerializeField] private int numberPerObject = 20;

    private readonly List<GameObject> pool = new();

    void Awake()
    {
        for (int i = 0; i < numberPerObject; i++)
        {
            GameObject obj = Instantiate(objectToPool, this.transform);
            pool.Add(obj);
            obj.SetActive(false);
        }
    }

    public GameObject GetFromPool(Vector3 pos = default, Quaternion rot = default, bool makeActive = true)
    {
        foreach (var obj in pool)
        {
            if (!obj.activeSelf)
            {
                obj.transform.SetPositionAndRotation(pos, rot);
                obj.SetActive(makeActive);
                return obj;
            }
        }
        return null;
    }

    public GameObject GetFromPool(bool makeActive = true)
    { return GetFromPool(default, default, makeActive); }
}
