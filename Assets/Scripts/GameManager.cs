using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private GameStateMachine _machine;
    private GameStateRun _stateRun;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
            Initialize();
        }
        else Destroy(this);
    }

    private void Initialize()
    {
        _machine = new();
        _stateRun = new(_machine, "Run");
        _machine.Initialize(_stateRun);
    }

    void Update()
    {
        _machine.CurrentState.LogicUpdate();
    }
}
