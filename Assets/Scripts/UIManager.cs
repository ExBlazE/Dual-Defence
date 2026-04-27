using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Transform lifeBar;
    [SerializeField] private TextMeshProUGUI scoreText;

    void OnEnable()
    {
        GameEvents.OnLifeChange += SetLifeBar;
        GameEvents.OnScoreChange += SetScore;
    }

    void OnDisable()
    {
        GameEvents.OnLifeChange -= SetLifeBar;
        GameEvents.OnScoreChange -= SetScore;
    }

    void Start()
    {
        SetLifeBar(1f);
        SetScore(0);
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
