using UnityEngine;

public class Hold : State
{
    public Hold(Animator animator) : base(animator)
    {
    }

    public override void Enter()
    {
        if (_animator != null)
        {
            _animator.StopPlayback();
            _animator.CrossFade("Hold", 0.25f);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
