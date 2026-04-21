using UnityEngine;

public class Controller : MonoBehaviour
{
    public void ShootRed()
    {
        Enemy target = Targets.Instance.GetClosestRed();
        if (target != null)
        {
            target.OnHit();
        }
    }

    public void ShootBlue()
    {
        Enemy target = Targets.Instance.GetClosestBlue();
        if (target != null)
        {
            target.OnHit();
        }
    }
}
