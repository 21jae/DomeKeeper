using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class O_FlyerAttackAttack : M_State
{
    O_FlyerAttack attack;
    Vector2 dir;
    public O_FlyerAttackAttack(M_Base @base, M_StateMachine stateMachine, string aniboolname, O_FlyerAttack attack) : base(@base, stateMachine, aniboolname)
    {
        this.attack = attack;
    }

    public override void Enter()
    {
        base.Enter(); 
        dir = attack.Getdir().normalized;
        dir = dir * attack.moveSpeed.x;
        attack.SetVelocity(dir);
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
