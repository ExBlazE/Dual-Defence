using System.Collections.Generic;
using UnityEngine;

public class Targets : MonoBehaviour
{
    [SerializeField] private Transform border;

    private readonly List<Enemy> redTargets = new();
    private readonly List<Enemy> blueTargets = new();

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
    { return GetClosest(redTargets); }

    public Enemy GetClosestBlue()
    { return GetClosest(blueTargets); }

    public void AddTarget(Enemy enemy)
    {
        if (enemy.type == TargetColor.Red)
        {
            if (!redTargets.Contains(enemy))
                redTargets.Add(enemy);
        }

        else if (enemy.type == TargetColor.Blue)
        {
            if (!blueTargets.Contains(enemy))
                blueTargets.Add(enemy);
        }
    }

    public void RemoveTarget(Enemy enemy)
    {
        if (enemy.type == TargetColor.Red)
        {
            redTargets.Remove(enemy);
        }

        else if (enemy.type == TargetColor.Blue)
        {
            blueTargets.Remove(enemy);
        }
    }
}
