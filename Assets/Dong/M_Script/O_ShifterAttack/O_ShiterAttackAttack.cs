using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class O_ShiterAttackAttack : M_State
{
    O_ShiterAttack attack;
    Vector2 dir;
    public O_ShiterAttackAttack(M_Base @base, M_StateMachine stateMachine, string aniboolname, O_ShiterAttack attack) : base(@base, stateMachine, aniboolname)
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
