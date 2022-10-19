public class GameStateMachine
{
    public GameState CurrentState { get; private set; }

    public void Initialize(GameState startState)
    {
        CurrentState = startState;
        CurrentState.Enter();
    }

    public void ChangeState(GameState state)
    {
        if(state != null)
        {
            CurrentState.Exit();
            CurrentState = state;
            CurrentState.Enter();
        }
    }
}
