using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_TickerExplosion : M_State
{
    M_Ticker ticker;
    public M_TickerExplosion(M_Base @base, M_StateMachine stateMachine, string aniboolname, M_Ticker ticker) : base(@base, stateMachine, aniboolname)
    {
        this.ticker = ticker;
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
    }
}
