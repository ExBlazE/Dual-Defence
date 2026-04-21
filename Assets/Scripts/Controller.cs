using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField] private VFXHandler vfxHandler;

    public void ShootRed()
    {
        Enemy target = Targets.Instance.GetClosestRed();
        if (target != null)
        {
            target.OnHit();
            if (vfxHandler != null)
                vfxHandler.SpawnRedLaser(target.transform.position);
        }
        else
            GameManager.Instance.LoseLife();
    }

    public void ShootBlue()
    {
        Enemy target = Targets.Instance.GetClosestBlue();
        if (target != null)
        {
            target.OnHit();
            if (vfxHandler != null)
                vfxHandler.SpawnBlueLaser(target.transform.position);
        }
        else
            GameManager.Instance.LoseLife();
    }
}
