using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Transform lifeBar;
    [SerializeField] private TextMeshProUGUI scoreText;

    void Start()
    {
        lifeBar.localScale = new Vector3(1f, lifeBar.localScale.y, lifeBar.localScale.z);
        scoreText.SetText("0");
    }

    public void SetLifeBar(float scaleX)
    {
        lifeBar.localScale = new Vector3(scaleX, lifeBar.localScale.y, lifeBar.localScale.z);
    }

    public void SetScore(int score)
    {
        scoreText.SetText(score.ToString());
    }
}
