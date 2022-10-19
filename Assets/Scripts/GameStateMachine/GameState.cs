using UnityEngine;
public abstract class GameState
{
    protected GameStateMachine _machine;
    protected string _stateName;

    public GameState(GameStateMachine machine, string stateName)
    {
        _machine = machine;
        _stateName = stateName;
    }

    public virtual void Enter()
    {
        Debug.Log(_stateName);
    }

    public virtual void Exit()
    {

    }

    public virtual void LogicUpdate()
    {

    }
}
