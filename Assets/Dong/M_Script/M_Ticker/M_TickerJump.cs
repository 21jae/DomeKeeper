using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_TickerJump : M_State
{
    M_Ticker ticker;
    public M_TickerJump(M_Base @base, M_StateMachine stateMachine, string aniboolname, M_Ticker ticker) : base(@base, stateMachine, aniboolname)
    {
        this.ticker = ticker;
    }

    public override void Enter()
    {
        base.Enter();
        ticker.SetVelocity(ticker.zero);
        ticker.rb.gravityScale = 1;
        ticker.Jump(ticker.jumpPower);
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
