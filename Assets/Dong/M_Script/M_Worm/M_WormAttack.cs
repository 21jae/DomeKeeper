using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_WormAttack : M_State
{
    M_Worm worm;

    public M_WormAttack(M_Base @base, M_StateMachine stateMachine, string aniboolname, M_Worm worm) : base(@base, stateMachine, aniboolname)
    {
        this.worm = worm;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
        worm.attackCount++;
    }

    public override void Update()
    {
        base.Update();
    }
}
