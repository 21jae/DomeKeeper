using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_FlyerAppear : M_State
{
    M_Flyer flyer;
    public M_FlyerAppear(M_Base @base, M_StateMachine stateMachine, string aniboolname, M_Flyer flyer) : base(@base, stateMachine, aniboolname)
    {
        this.flyer = flyer;
    }

    public override void Enter()
    {
        base.Enter();
        flyer.inactive.enabled = true;
    }

    public override void Exit()
    {
        base.Exit();
        flyer.inactive.enabled = false;
        flyer.active.enabled = true;
    }

    public override void Update()
    {
        base.Update();
    }
}
