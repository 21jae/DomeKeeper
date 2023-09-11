using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_ShifterAttack : M_State
{
    M_Shifter shifter;
    public M_ShifterAttack(M_Base @base, M_StateMachine stateMachine, string aniboolname, M_Shifter shifter) : base(@base, stateMachine, aniboolname)
    {
        this.shifter = shifter;
    }

    public override void Enter()
    {
        base.Enter();
        shifter.shiftCount = 0;
    }

    public override void Exit()
    {
        base.Exit();
        shifter.shiftCounter = Random.Range(2, 4);
    }

    public override void Update()
    {
        base.Update();
    }
}
