using UnityEngine;

public class Carry : State
{
    public Carry(Animator animator) : base(animator)
    {
    }

    public override void Enter()
    {
        if (_animator != null)
        {
            _animator.StopPlayback();
            _animator.CrossFade("Carry", 0.25f);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
