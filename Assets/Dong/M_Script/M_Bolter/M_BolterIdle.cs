using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_BolterIdle : M_State
{
    M_Bolter bolter;
    public M_BolterIdle(M_Base @base, M_StateMachine stateMachine, string aniboolname, M_Bolter bolter) : base(@base, stateMachine, aniboolname)
    {
        this.bolter = bolter;
    }

    public override void Enter()
    {
        base.Enter();
        bolter.attackCount = 0;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        bolter.SetVelocity(bolter.zero);

        bolter.attackCount += Time.deltaTime;

        if(bolter.attackCounter < bolter.attackCount)
        {
            stateMachine.ChangeState(bolter.attack1);
        }

        if (bolter.attacked)
        {
            stateMachine.ChangeState(bolter.hit);
        }
    }
}
