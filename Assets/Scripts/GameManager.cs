using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int playerLives = 5;
    [SerializeField] private VFXHandler vfxHandler;

    public GameState GameState { get; private set; }
    private int _lives;

    public static GameManager Instance;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);
    }

    void Start()
    {
        _lives = playerLives;
        GameState = GameState.Playing;
    }

    public void LoseLife()
    {
        _lives--;
        if (_lives <= 0)
        {
            _lives = 0;
            GameState = GameState.Ended;
        }

        if (vfxHandler != null)
            vfxHandler.SetLifeBar((float)_lives / playerLives);
    }
}

public enum GameState
{
    Waiting,
    Playing,
    Paused,
    Ended
}
