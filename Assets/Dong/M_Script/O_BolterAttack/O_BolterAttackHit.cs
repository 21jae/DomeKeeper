using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class O_BolterAttackHit : M_State
{
    O_BolterAttack attack;
    public O_BolterAttackHit(M_Base @base, M_StateMachine stateMachine, string aniboolname, O_BolterAttack attack) : base(@base, stateMachine, aniboolname)
    {
        this.attack = attack;
    }

    public override void Enter()
    {
        base.Enter();
        attack.SetVelocity(attack.zero);
        attack.SoundHit();
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
