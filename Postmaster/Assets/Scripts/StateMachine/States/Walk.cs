using UnityEngine;

public class Walk : State
{
    public Walk(Animator animator) : base(animator)
    {
    }

    public override void Enter()
    {
        if (_animator != null)
        {
            _animator.StopPlayback();
            _animator.CrossFade("Walk", 0.25f);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
