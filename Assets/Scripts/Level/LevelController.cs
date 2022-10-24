using UnityEngine;

public class LevelController : MonoBehaviour
{
    public static LevelController Instance;
    
    public Level CurrentLevel { get; private set; }
    public float Progress { get; private set; }

    private Transform _container;
    private float _currentTime = 0;
    private float _maxX;
    private float _maxY;
    private bool isRunning;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else Destroy(this.gameObject);
    }

    private void Start()
    {
        CurrentLevel = GameManager.Instance.CurrentLevel;
        _container = new GameObject(CurrentLevel.Name).transform;
        _maxX = GameManager.Instance.MaxX;
        _maxY = GameManager.Instance.MaxY;
        isRunning = true;
        Progress = 0;

        GameManager.Instance.Run();

        GlobalEventManager.OnGameOver += StopSpawning;
   }

    private void OnDisable()
    {
        GlobalEventManager.OnGameOver -= StopSpawning;
    }

    private void Update()
    {
        if (isRunning && CurrentLevel != null)
        {
            _currentTime += Time.deltaTime;
            if (_currentTime >= CurrentLevel.TimeBetweenSpawn)
            {
                _currentTime = 0;
                SpawnEnemy();
            }

            Progress += Time.deltaTime;
            SendProgress();
            if (Progress >= CurrentLevel.TimeToComplete)
            {
                LevelComplete();
                isRunning = false;
            }
        }
    }

    public void SetCurrentLevel(Level level)
    {
        if (level != null)
            CurrentLevel = level;
        else Debug.LogError("Level can't be Null!");
    }

    private void SpawnEnemy()
    {
        int index = Random.Range(0, CurrentLevel.Enemies.Length);
        float XPos = Random.Range(-_maxX, _maxX);

        Enemy newEnemy = Instantiate(CurrentLevel.Enemies[index], _container);
        newEnemy.transform.position = new Vector2(XPos, _maxY + 1f);
        newEnemy.Move(Vector2.down * CurrentLevel.EnemiesSpeed);
    }

    public void StopSpawning() => isRunning = false;

    public void ClearLevel()
    {
        foreach (Transform enemy in _container)
            Destroy(enemy.gameObject);
    }

    private void SendProgress()
    {
        float perscents = 1f / CurrentLevel.TimeToComplete * Progress;
        GlobalEventManager.SendOnProgressChanged(perscents);
    }

    private void LevelComplete()
    {
        GlobalEventManager.SendOnLevelCompleted();
        Debug.Log("Level completed!");
    }
}
