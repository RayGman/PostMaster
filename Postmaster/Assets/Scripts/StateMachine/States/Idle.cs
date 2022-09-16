using UnityEngine;

public class Idle : State
{
    public Idle(Animator animator) : base(animator)
    {     
    }

    public override void Enter()
    {
        if (_animator != null)
        {
            _animator.StopPlayback();
            _animator.CrossFade("Idle", 0.5f);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
