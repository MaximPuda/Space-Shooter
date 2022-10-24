using UnityEngine.Events;

public class GlobalEventManager
{
    public static event UnityAction OnPlayerDie;
    public static event UnityAction<int> OnPlayerTakeDamge;
    public static event UnityAction<int> OnPlayerGetPoints;
    public static event UnityAction<int> OnEnemyKilled;
    public static event UnityAction<float> OnProgressChanged;
    public static event UnityAction OnLevelCompleted;
    public static event UnityAction OnGameOver;

    public static void SendOnPlayerDie() => OnPlayerDie?.Invoke();
    public static void SendOnPlayerTakeDamge(int currentHealth) => OnPlayerTakeDamge?.Invoke(currentHealth);
    public static void SendOnPlayerGetPoints(int currentPoints) => OnPlayerGetPoints?.Invoke(currentPoints);
    public static void SendOnEnemyKilled(int reward) => OnEnemyKilled?.Invoke(reward);
    public static void SendOnProgressChanged(float progress) => OnProgressChanged?.Invoke(progress);
    public static void SendOnLevelCompleted() => OnLevelCompleted?.Invoke();
    public static void SendOnGameOver() => OnGameOver?.Invoke();

}
