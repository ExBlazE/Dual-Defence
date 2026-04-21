using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [SerializeField] private string poolName; // Only for naming in inspector

    [Space]
    [SerializeField] private List<GameObject> objectsToPool;
    [SerializeField] private int numberPerObject = 20;

    private readonly List<GameObject> objectPool = new();

    void Awake()
    {
        foreach (var item in objectsToPool)
        {
            for(int i = 0; i < numberPerObject; i++)
            {
                GameObject obj = Instantiate(item, this.transform);
                objectPool.Add(obj);
                obj.SetActive(false);
            }
        }
    }

    public GameObject GetFromPool(Vector3 pos, Quaternion rot)
    {
        foreach (var obj in objectPool)
        {
            if (!obj.activeSelf)
            {
                obj.transform.SetPositionAndRotation(pos, rot);
                obj.SetActive(true);
                return obj;
            }
        }
        return null;
    }

    public GameObject GetRandomFromPool(Vector3 pos, Quaternion rot)
    {
        // Count how many are available
        int availableCount = 0;
        foreach (var obj in objectPool)
        {
            if (!obj.activeSelf) availableCount++;
        }

        // Pick a random "target" index among the available ones
        int randomTarget = Random.Range(0, availableCount);

        // Loop again and grab the Nth inactive object
        int currentIndex = 0;
        foreach (var obj in objectPool)
        {
            if (!obj.activeSelf)
            {
                if (currentIndex == randomTarget)
                {
                    obj.transform.SetPositionAndRotation(pos, rot);
                    obj.SetActive(true);
                    return obj;
                }
                currentIndex++;
            }
        }
        return null;
    }
}
