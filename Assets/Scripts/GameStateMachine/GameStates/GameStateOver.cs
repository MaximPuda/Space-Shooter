using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateOver : GameState
{
    public GameStateOver(GameStateMachine machine, string stateName) : base(machine, stateName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        GlobalEventManager.SendOnGameOver();
    }
}
