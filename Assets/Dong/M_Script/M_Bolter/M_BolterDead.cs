using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_BolterDead : M_State
{
    M_Bolter bolter;
    public M_BolterDead(M_Base @base, M_StateMachine stateMachine, string aniboolname,M_Bolter bolter) : base(@base, stateMachine, aniboolname)
    {
        this.bolter = bolter;
    }

    public override void Enter()
    {
        base.Enter();

        if (bolter.deadCheck == 0) M_GameManager.instance.killedMonster++;
        bolter.deadCheck++;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        bolter.SetVelocity(bolter.zero);
    }
}
