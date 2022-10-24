public class GameStateMainMenu : GameState
{
    public GameStateMainMenu(GameStateMachine machine, string stateName) : base(machine, stateName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        GameManager.Instance.LoadGame();
    }
}
