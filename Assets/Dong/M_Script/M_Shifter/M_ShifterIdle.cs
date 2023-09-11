using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_ShifterIdle : M_State
{
    M_Shifter shifter;
    public M_ShifterIdle(M_Base @base, M_StateMachine stateMachine, string aniboolname, M_Shifter shifter) : base(@base, stateMachine, aniboolname)
    {
        this.shifter = shifter;
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
