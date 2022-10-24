using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamestatePause : GameState
{
    public GamestatePause(GameStateMachine machine, string stateName) : base(machine, stateName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        Time.timeScale = 0;
    }

    public override void Exit()
    {
        base.Exit();

        Time.timeScale = 1;
    }
}
