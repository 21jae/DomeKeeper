using UnityEngine;

public class M_DiverBack : M_State
{
    M_Diver diver;
    Vector2 back;
    public M_DiverBack(M_Base @base, M_StateMachine stateMachine, string aniboolname, M_Diver diver) : base(@base, stateMachine, aniboolname)
    {
        this.diver = diver;
    }

    public override void Enter()
    {
        base.Enter();
        back = -diver.Getdir();
        back = back.normalized * 2;
        diver.SetVecVelocity(back.x, back.y);
        diver.isAttacking = -1;

        diver.gameObject.GetComponent<CircleCollider2D>().enabled = false;
        diver.gameObject.GetComponent<CapsuleCollider2D>().enabled = true;
        diver.gameObject.GetComponentInChildren<BoxCollider2D>().enabled = false;

        if(diver.x >0) diver.SoundBack();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (diver.stun) diver.SetVecVelocity(0, 0);
        else if (!diver.stun) diver.SetVecVelocity(back.x, back.y);

    }
}
