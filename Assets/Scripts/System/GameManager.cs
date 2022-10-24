using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    [SerializeField] private float _cameraBorderOffsetX = 0.3f;
    [SerializeField] private float _cameraBorderOffsetY = 0.6f;
    [SerializeField] private Level[] _levels;

    public Level CurrentLevel { get; private set; }
    public int TotalPoints { get; private set; }
    public int CurrentPoints { get; private set; }

    public Level[] Levels => _levels;
    public float MaxX { get; private set; }
    public float MaxY { get; private set; }

    private GameStateMachine _machine;

    private GameStateMainMenu _stateMainMenu;
    private GameStateRun _stateRun;
    private GameStateOver _stateOver;
    private GameStateCompleted _stateCompleted;
    private GamestatePause _statePause;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
            Initialize();
        }
        else Destroy(this.gameObject);

        var camBorder = Camera.main.ViewportToWorldPoint(Vector2.zero);
        MaxX = camBorder.x * -1 - _cameraBorderOffsetX;
        MaxY = camBorder.y * -1 - _cameraBorderOffsetY;
    }

    private void Start()
    {
        GlobalEventManager.OnPlayerDie += GameOver;
        GlobalEventManager.OnLevelCompleted += LevelCompleted;
        GlobalEventManager.OnPlayerGetPoints += GetCurrentPoints;
    }

    private void OnDisable()
    {
        GlobalEventManager.OnPlayerDie -= GameOver;
        GlobalEventManager.OnLevelCompleted -= LevelCompleted;
        GlobalEventManager.OnPlayerGetPoints -= GetCurrentPoints;
    }

    private void Update()
    {
        _machine.CurrentState.LogicUpdate();
    }

    private void Initialize()
    {
        _machine = new();

        _stateMainMenu = new(_machine, "MainMenu");
        _stateRun = new(_machine, "Run");
        _stateOver = new(_machine, "Over");
        _stateCompleted = new(_machine, "Completed");
        _statePause = new(_machine, "Pause");

        CurrentLevel = Levels[0];

        if (SceneManager.GetActiveScene().buildIndex == 0)
            _machine.Initialize(_stateMainMenu);
        else if (SceneManager.GetActiveScene().buildIndex == 1)
            _machine.Initialize(_stateRun);
    }

    public void LoadLevel(int levelIndex)
    {
        if (_levels != null && _levels.Length > 0)
        {
            if (levelIndex > -1 && levelIndex < _levels.Length)
            {
                CurrentLevel = _levels[levelIndex];
                SceneManager.LoadScene(1);
            }
            else Debug.Log("Index is invalid");
        }
        else Debug.Log("List of levels is empty!");
    }

    public void Run() => _machine.ChangeState(_stateRun);

    public void GameOver() => _machine.ChangeState(_stateOver);

    public void LevelCompleted() => _machine.ChangeState(_stateCompleted);

    public void Pause()
    {
        if (_machine.CurrentState == _stateRun)
            _machine.ChangeState(_statePause);
        else if (_machine.CurrentState == _statePause)
            _machine.ChangeState(_stateRun);
    }

    private void GetCurrentPoints(int points) => CurrentPoints = points;

    public void UpdateTotalPoints() => TotalPoints += CurrentPoints;

    public void Restart()
    {
        SceneManager.LoadScene(1);
        Run();
    }

    public void ExitToMainMenu()
    {
        SceneManager.LoadScene(0);
        _machine.ChangeState(_stateMainMenu);
    }

    public void LoadGame()
    {
        SaveData saveData = SaveSystem.Load();

        if (saveData != null)
        {
            TotalPoints = saveData.TotalPoints;
            for (int i = 0; i < Levels.Length; i++)
                Levels[i].Status = (LevelStatus)saveData.LevelsStatus[i];
        }
        else
        {
            TotalPoints = 0;
            Levels[0].Status = LevelStatus.Available;
            for (int i = 1; i < Levels.Length; i++)
                Levels[i].Status = LevelStatus.Locked; 
        }
    }

    public void SaveGame()
    {
        SaveData saveData = new(TotalPoints, Levels);
        SaveSystem.Save(saveData);
    }
}
