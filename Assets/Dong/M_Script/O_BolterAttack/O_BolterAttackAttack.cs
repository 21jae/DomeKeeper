using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class O_BolterAttackAttack : M_State
{
    O_BolterAttack attack;
    Vector2 dir;
    public O_BolterAttackAttack(M_Base @base, M_StateMachine stateMachine, string aniboolname, O_BolterAttack attack) : base(@base, stateMachine, aniboolname)
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
