using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class M_ShifterShift : M_State
{
    M_Shifter shifter;
    public M_ShifterShift(M_Base @base, M_StateMachine stateMachine, string aniboolname, M_Shifter shifter) : base(@base, stateMachine, aniboolname)
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
        shifter.shiftCount++;

        int x = (int)((Random.Range(1, 3) - 1.5) * 2);
       
        shifter.transform.position = new Vector2(Random.Range(2f, 15f) * x, Random.Range(-2f, 5f));
    }

    public override void Update()
    {
        base.Update();
    }
}
