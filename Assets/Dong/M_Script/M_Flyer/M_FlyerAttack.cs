using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_FlyerAttack : M_State
{
    M_Flyer flyer;
    public M_FlyerAttack(M_Base @base, M_StateMachine stateMachine, string aniboolname, M_Flyer flyer) : base(@base, stateMachine, aniboolname)
    {
        this.flyer = flyer;
    }

    public override void Enter()
    {
        base.Enter();
        flyer.currentAttackTimes= 0;
        flyer.attackTimes = Random.Range(2, 4);
    }

    public override void Exit()
    {
        base.Exit();

    }

    public override void Update()
    {
        base.Update();
        if (flyer.currentAttackTimes == flyer.attackTimes)
        {
            stateMachine.ChangeState(flyer.hide);
        }
    }
}
