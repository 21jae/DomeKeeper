using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_FlyerDead : M_State
{
    M_Flyer flyer;
    public M_FlyerDead(M_Base @base, M_StateMachine stateMachine, string aniboolname, M_Flyer flyer) : base(@base, stateMachine, aniboolname)
    {
        this.flyer = flyer;
    }

    public override void Enter()
    {
        base.Enter();
        flyer.active.enabled = false;
        flyer.inactive.enabled = false;
        if (flyer.deadCheck == 0) M_GameManager.instance.killedMonster++;
        flyer.deadCheck++;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        flyer.SetVelocity(flyer.zero);
    }

}
