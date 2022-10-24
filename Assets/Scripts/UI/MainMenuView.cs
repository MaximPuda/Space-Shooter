using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenuView : MonoBehaviour
{
    [Header("Player Data")]
    [SerializeField] private TMP_Text _pointsLabel;

    [Header("Map")]
    [SerializeField] private Button[] _levelsPointers;

    [SerializeField] private Color _avalablePointerColor;
    [SerializeField] private string _avalablePointerText;

    [SerializeField] private Color _lockedPointerColor;
    [SerializeField] private string _lockedPointerText;

    [SerializeField] private Color _completePointerColor;
    [SerializeField] private string _completedPointerText;

    private void Start()
    {
        SetLevelsPointers();
        UpdateTotalPoints();
    }

    private void SetLevelsPointers()
    {
        for (int i = 0; i < _levelsPointers.Length; i++)
        {
            Button pointer = _levelsPointers[i];
            switch (GameManager.Instance.Levels[i].Status)
            {
                case LevelStatus.Available:
                    pointer.image.color = _avalablePointerColor;
                    pointer.GetComponentInChildren<TMP_Text>().text = _avalablePointerText;
                    pointer.interactable = true;
                    break;

                case LevelStatus.Locked:
                    pointer.image.color = _lockedPointerColor;
                    pointer.GetComponentInChildren<TMP_Text>().text = _lockedPointerText;
                    pointer.interactable = false;
                    break;

                case LevelStatus.Completed:
                    pointer.image.color = _completePointerColor;
                    pointer.GetComponentInChildren<TMP_Text>().text = _completedPointerText;
                    pointer.interactable = true;
                    break;

                default: break;
            }
        }
    }

    private void UpdateTotalPoints()
    {
        _pointsLabel.text = GameManager.Instance.TotalPoints.ToString() + ":POINTS";
    }
}
