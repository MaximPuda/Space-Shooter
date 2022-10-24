public class GameStateCompleted : GameState
{
    public GameStateCompleted(GameStateMachine machine, string stateName) : base(machine, stateName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        GameManager.Instance.UpdateTotalPoints();

        var levels = GameManager.Instance.Levels;
        var currentLevel = GameManager.Instance.CurrentLevel;
        for (int i = 0; i < levels.Length; i++)
        {
            if (levels[i] == currentLevel && levels[i].Status != LevelStatus.Completed)
            {
                levels[i].Status = LevelStatus.Completed;
                if(i < levels.Length - 1)
                    levels[i + 1].Status = LevelStatus.Available;
            }
        }
    }

    public override void Exit()
    {
        base.Exit();

        GameManager.Instance.SaveGame();
    }
}
