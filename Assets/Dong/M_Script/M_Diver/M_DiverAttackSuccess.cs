using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_DiverAttackSuccess : M_State
{
    M_Diver diver;
    public M_DiverAttackSuccess(M_Base @base, M_StateMachine stateMachine, string aniboolname, M_Diver diver) : base(@base, stateMachine, aniboolname)
    {
        this.diver = diver;
    }

    public override void Enter()
    {
        base.Enter();
        diver.SetVelocity(diver.zero);
        diver.isAttacking = 0;
        diver.SoundHit();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        diver.gameObject.GetComponent<CircleCollider2D>().enabled = true;
        diver.gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
        diver.gameObject.GetComponentInChildren<BoxCollider2D>().enabled = false;
    }
}
