using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int playerLife = 5;
    [SerializeField] private UIManager ui;

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

    public void LoseLife()
    {
        _life--;
        if (_life <= 0)
        {
            _life = 0;
            State = GameState.Ended;
        }

        if (ui != null)
            ui.SetLifeBar((float)_life / playerLife);
    }

    public void AddScore(int score)
    {
        _score += score;
        ui.SetScore(_score);
    }
}

public enum GameState
{
    Waiting,
    Playing,
    Paused,
    Ended
}
