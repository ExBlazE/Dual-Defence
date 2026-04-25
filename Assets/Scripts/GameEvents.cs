using System;
using UnityEngine;

public static class GameEvents
{
    public static event Action<Faction, Vector3> OnEnemyHit;
    public static event Action<Faction> OnSelfHit;
    public static event Action<Faction, Shape, Vector3> OnEnemyDeath;

    public static void RaiseEnemyHit(Faction faction, Vector3 position)
    { OnEnemyHit?.Invoke(faction, position); }

    public static void RaiseSelfHit(Faction faction)
    { OnSelfHit?.Invoke(faction); }

    public static void RaiseEnemyDeath(Faction faction, Shape shape, Vector3 position)
    { OnEnemyDeath?.Invoke(faction, shape, position); }
}
