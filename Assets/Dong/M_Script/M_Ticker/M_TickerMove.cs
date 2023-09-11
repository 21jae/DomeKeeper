using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_TickerMove : M_State
{
    M_Ticker ticker;
    public M_TickerMove(M_Base @base, M_StateMachine stateMachine, string aniboolname, M_Ticker ticker) : base(@base, stateMachine, aniboolname)
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
        ticker.SetVelocity(ticker.moveSpeed);
        if (Vector2.Distance(ticker.domeCenter.transform.position, ticker.transform.position) <= 5)
        {
            stateMachine.ChangeState(ticker.jump);
        }
    }
}
