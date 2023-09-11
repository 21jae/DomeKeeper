using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class O_FlyerAttackHit : M_State
{
    O_FlyerAttack attack;
    public O_FlyerAttackHit(M_Base @base, M_StateMachine stateMachine, string aniboolname, O_FlyerAttack attack) : base(@base, stateMachine, aniboolname)
    {
        this.attack = attack;
    }

    public override void Enter()
    {
        base.Enter();
        attack.SetVelocity(attack.zero);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
    }

}
