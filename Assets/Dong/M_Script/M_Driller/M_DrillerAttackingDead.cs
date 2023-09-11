using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_DrillerAttackingDead : M_State
{
    M_Driller driller;
    public M_DrillerAttackingDead(M_Base @base, M_StateMachine stateMachine, string aniboolname, M_Driller driller) : base(@base, stateMachine, aniboolname)
    {
        this.driller = driller;
    }

    public override void Enter()
    {
        base.Enter();
        if (driller.deadCheck == 0) M_GameManager.instance.killedMonster++;
        driller.deadCheck++;
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
