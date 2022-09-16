using UnityEngine;

public class Run : State
{
    public Run(Animator animator) : base(animator)
    {
    }

    public override void Enter()
    {
        if (_animator != null)
        {
            _animator.StopPlayback();
            _animator.CrossFade("Run", 0.1f);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
