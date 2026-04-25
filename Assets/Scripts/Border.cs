using UnityEngine;

public class Border : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (GameManager.Instance.State == GameState.Ended) return;
        
        if (collision.collider.TryGetComponent(out Enemy enemy))
        {
            GameManager.Instance.LoseLife();
            enemy.OnHit();
        }
    }
}
