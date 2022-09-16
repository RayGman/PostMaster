public class StateMachine
{
    public State CurrentState { get; set; }

    public void Init(State state)
    {
        CurrentState = state;
        CurrentState.Enter();
    }

    public void ChangeState(State newState)
    {
        CurrentState.Exit();
        Init(newState);
    }
}
