using UnityEngine;

public class Controller : MonoBehaviour
{
    public void ShootRed()
    {
        if (GameManager.Instance.State != GameState.Playing) return;

        Enemy target = Targets.Instance.GetClosestRed();
        if (target != null)
        {
            target.OnHit();
            GameManager.Instance.AddScore(target.Value);
            GameEvents.RaiseEnemyHit(Faction.Red, target.transform.position);
        }
        else
        {
            GameManager.Instance.LoseLife();
            GameEvents.RaiseSelfHit(Faction.Red);
        }
    }

    public void ShootBlue()
    {
        if (GameManager.Instance.State != GameState.Playing) return;

        Enemy target = Targets.Instance.GetClosestBlue();
        if (target != null)
        {
            target.OnHit();
            GameManager.Instance.AddScore(target.Value);
            GameEvents.RaiseEnemyHit(Faction.Blue, target.transform.position);
        }
        else
        {
            GameManager.Instance.LoseLife();
            GameEvents.RaiseSelfHit(Faction.Blue);
        }
    }
}
