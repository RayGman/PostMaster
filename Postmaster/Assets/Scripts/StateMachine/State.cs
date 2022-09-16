using UnityEngine;

public abstract class State
{
    protected Animator _animator;

    public State()
    {
    }

    public State(Animator animator)
    {
        _animator = animator;
    }

    public virtual void Enter()
    {
        
    }

    public virtual void Exit()
    {
        if (_animator != null)
        {
            _animator.StopPlayback();
        }
    }
}

