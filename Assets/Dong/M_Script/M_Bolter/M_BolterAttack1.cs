using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_BolterAttack1 : M_State
{
    M_Bolter bolter;

    public M_BolterAttack1(M_Base @base, M_StateMachine stateMachine, string aniboolname, M_Bolter bolter) : base(@base, stateMachine, aniboolname)
    {
        this.bolter = bolter;
    }

    public override void Enter()
    {
        base.Enter();
        bolter.SetVelocity(bolter.zero);
        bolter.attackChange = Random.Range(1, 3);
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
