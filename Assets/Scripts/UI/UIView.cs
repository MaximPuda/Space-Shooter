using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIView : MonoBehaviour
{
    [Header("Compontnts")]
    [SerializeField] private Image _health;
    [SerializeField] private Slider _progressBar;
    [SerializeField] private TMP_Text _pointsLabel;
    [SerializeField] private Animator _animator;
    [SerializeField] private TMP_Text _windowHeader;

    [Header("Game Over")]
    [SerializeField] private string _gameOverMessage;
    [SerializeField] private Color _gamOverTextColor;

    [Header("Level Completed")]
    [SerializeField] private string _completedMessage;
    [SerializeField] private Color _completedHeaderColor;

    private void OnEnable()
    {
        GlobalEventManager.OnPlayerTakeDamge += UpdateHealth;
        GlobalEventManager.OnPlayerGetPoints += UpdatePoints;
        GlobalEventManager.OnProgressChanged += UpdateProgress;
        GlobalEventManager.OnGameOver += ShowGameOverWindow;
        GlobalEventManager.OnLevelCompleted += ShowLevelCompletedWindow;
    }

    private void OnDisable()
    {
        GlobalEventManager.OnPlayerTakeDamge -= UpdateHealth;
        GlobalEventManager.OnPlayerGetPoints -= UpdatePoints;
        GlobalEventManager.OnProgressChanged -= UpdateProgress;
        GlobalEventManager.OnGameOver -= ShowGameOverWindow;
        GlobalEventManager.OnLevelCompleted -= ShowLevelCompletedWindow;
    }

    public void UpdateHealth(int currentHealth)
    {
        float healthValue = 1f / 3f * (float)currentHealth;
        _health.fillAmount = healthValue;
    }

    public void UpdatePoints(int points)
    {
        _pointsLabel.text = points.ToString();
    }

    public void ShowGameOverWindow()
    {
        _windowHeader.text = _gameOverMessage;
        _windowHeader.color = _gamOverTextColor;
        _animator.SetBool("GameOver", true);
    }

    public void ShowLevelCompletedWindow()
    {
        _windowHeader.text = _completedMessage;
        _windowHeader.color = _completedHeaderColor;
        _animator.SetBool("GameOver", true);
    }

    public void UpdateProgress(float progress)
    {
        _progressBar.value = progress;
    }
}
