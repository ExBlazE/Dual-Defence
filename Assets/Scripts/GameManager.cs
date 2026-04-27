using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int playerLife = 5;

    public GameState State { get; private set; }

    private int _life;
    private int _score;

    public static GameManager Instance;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);

        Application.targetFrameRate = 60;
    }

    void Start()
    {
        _life = playerLife;
        _score = 0;
        State = GameState.Playing;
    }

    public float GetLifeFraction()
    { return (float)_life / playerLife; }

    public void LoseLife()
    {
        _life--;
        if (_life <= 0)
        {
            _life = 0;
            State = GameState.Ended;
        }

        GameEvents.RaiseLifeChange(GetLifeFraction());
    }

    public void AddScore(int score)
    {
        _score += score;
        GameEvents.RaiseScoreChange(_score);
    }

    private void ResetLife()
    {
        _life = playerLife;
    }

    private void ResetScore()
    {
        _score = 0;
    }

    public void ResetGame()
    {
        ResetLife();
        ResetScore();
        State = GameState.Playing;
    }
}

public enum GameState
{
    Waiting,
    Playing,
    Paused,
    Ended
}
