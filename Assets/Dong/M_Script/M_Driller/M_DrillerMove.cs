using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class M_DrillerMove : M_State
{
    M_Driller driller;
    public M_DrillerMove(M_Base @base, M_StateMachine stateMachine, string aniboolname, M_Driller driller) : base(@base, stateMachine, aniboolname)
    {
        this.driller = driller;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        driller.SetVelocity(driller.moveSpeed);
    }
}
