using System.Collections.Generic;
using UnityEngine;

public class Targets : MonoBehaviour
{
    [SerializeField] private Transform border;

    private readonly List<Enemy> Red = new();
    private readonly List<Enemy> Blue = new();

    public static Targets Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private Enemy GetClosest(List<Enemy> targetList)
    {
        if (targetList.Count == 0)
            return null;

        Enemy closestTarget = null;
        float closestDistance = Mathf.Infinity;

        foreach(var target in targetList)
        {
            float distance = target.transform.position.y - border.position.y;
            if (distance < closestDistance)
            {
                closestTarget = target;
                closestDistance = distance;
            }
        }

        return closestTarget;
    }

    public Enemy GetClosestRed()
    { return GetClosest(Red); }

    public Enemy GetClosestBlue()
    { return GetClosest(Blue); }

    public void AddTarget(Enemy enemy)
    {
        if (enemy.type == TargetType.Red)
        {
            if (!Red.Contains(enemy))
                Red.Add(enemy);
        }

        else if (enemy.type == TargetType.Blue)
        {
            if (!Blue.Contains(enemy))
                Blue.Add(enemy);
        }
    }

    public void RemoveTarget(Enemy enemy)
    {
        if (enemy.type == TargetType.Red)
        {
            Red.Remove(enemy);
        }

        else if (enemy.type == TargetType.Blue)
        {
            Blue.Remove(enemy);
        }
    }
}
