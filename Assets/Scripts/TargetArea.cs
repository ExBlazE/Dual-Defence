using UnityEngine;

public class TargetArea : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Enemy enemy))
            Targets.Instance.AddTarget(enemy);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Enemy enemy))
            Targets.Instance.RemoveTarget(enemy);
    }
}
