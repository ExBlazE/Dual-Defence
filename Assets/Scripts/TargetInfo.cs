using UnityEngine;

/// <summary>
/// Use this together with enemy ObjectPooler
/// </summary>
public class TargetInfo : MonoBehaviour
{
    public Faction Faction;
    public Shape Shape;
}

public enum Faction
{
    Red = 1,
    Blue = 2
}

public enum Shape
{
    Circle = 1,
    Triangle = 3,
    Square = 4,
    Pentagon = 5
}